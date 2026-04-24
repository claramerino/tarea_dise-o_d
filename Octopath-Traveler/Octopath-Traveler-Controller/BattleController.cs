using System.Collections.Generic;
using System.Linq;
using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public class BattleController
{
    private readonly Battle _battle;
    private readonly IBattleView _view;

    public BattleController(Battle battle, IBattleView view)
    {
        _battle = battle;
        _view = view;
    }

    public void Start()
    {
        while (!_battle.GameState.IsBattleOver())
        {
            PlayRound();

            if (!_battle.GameState.IsBattleOver())
            {
                _battle.AdvanceRound();
            }
        }

        ShowWinner();
    }

    private void PlayRound()
    {
        _view.ShowRoundStart(_battle.GameState.RoundNumber);
        _battle.StartRound();

        while (!_battle.GameState.IsBattleOver())
        {
            Unit? currentUnit = _battle.GetCurrentUnit();

            if (currentUnit == null)
            {
                return;
            }

            if (currentUnit.IsDead)
            {
                _battle.AdvanceTurn();
                continue;
            }

            _view.ShowBattleState(BuildPlayerTeamLines(), BuildEnemyTeamLines());

            if (!_battle.GameState.IsBattleOver())
            {
                _view.ShowRoundTurns(BuildRemainingTurnNames(), BuildNextRoundTurnNames());
            }

            if (currentUnit is Traveler traveler)
            {
                PlayTravelerTurn(traveler);
            }
            else if (currentUnit is Beast beast)
            {
                PlayBeastTurn(beast);
            }

            if (_battle.GameState.IsBattleOver())
            {
                return;
            }

            _battle.AdvanceTurn();
        }
    }

    private void PlayTravelerTurn(Traveler traveler)
    {
        bool turnResolved = false;

        while (!turnResolved && !_battle.GameState.IsBattleOver())
        {
            string input = _view.AskTravelerAction(traveler.Name);

            switch (input)
            {
                case "1":
                    turnResolved = ResolveBasicAttack(traveler);
                    break;

                case "2":
                    turnResolved = HandleSkillMenu(traveler);
                    break;

                case "3":
                    _battle.ResolveDefend(traveler);
                    turnResolved = true;
                    break;

                case "4":
                    _battle.Escape();
                    _view.ShowEscapeMessage();
                    turnResolved = true;
                    break;
            }
        }
    }

    private bool HandleSkillMenu(Traveler traveler)
    {
        while (true)
        {
            List<string> skills = traveler.ActiveSkills.ToList();
            string input = _view.AskSkillOption(traveler.Name, skills);

            int? selectedIndex = SelectOption(skills.Count, input);

            if (selectedIndex == null)
            {
                continue;
            }

            if (selectedIndex == -1)
            {
                return false;
            }

            string selectedSkillName = skills[selectedIndex.Value];
            return ResolveSkill(traveler, selectedSkillName);
        }
    }

    private bool ResolveBasicAttack(Traveler traveler)
    {
        string? weapon = SelectWeapon(traveler);
        if (weapon == null)
        {
            return false;
        }

        Beast? target = SelectBeastTarget(traveler);
        if (target == null)
        {
            return false;
        }

        int bpUsed = SelectBp(traveler);

        AttackResult result = _battle.ResolveBasicAttack(traveler, target, weapon, bpUsed);

        _view.ShowTravelerAttackResult(
            result.AttackerName,
            result.TargetName,
            result.Damage,
            result.AttackType,
            result.TargetRemainingHp,
            result.IsWeakness,
            result.EnteredBreakingPoint);

        return true;
    }

    private bool ResolveSkill(Traveler traveler, string skillName)
    {
        OffensiveSkillFactory factory = new OffensiveSkillFactory();
        OffensiveSkill? skill = factory.Create(skillName);

        if (skill == null)
        {
            return false;
        }

        if (!traveler.HasSpFor(skill.SpCost))
        {
            return false;
        }

        Unit? selectedTarget = null;
        string? selectedWeapon = null;

        // Si es Nightmare Chimera, pedir arma en orden clásico
        if (skill is Octopath_Traveler_Model.NightmareChimera)
        {
            List<string> allWeapons = new() { "Sword", "Spear", "Dagger", "Axe", "Bow", "Stave" };
            while (true)
            {
                string input = _view.AskWeaponOption(allWeapons);
                int? selectedIndex = SelectOption(allWeapons.Count, input);
                if (selectedIndex == null)
                    continue;
                if (selectedIndex == -1)
                    return false;
                selectedWeapon = allWeapons[selectedIndex.Value];
                break;
            }
        }

        if (skill.RequiresManualTarget())
        {
            selectedTarget = SelectSkillTarget(traveler);

            if (selectedTarget == null)
            {
                return false;
            }
        }

        int bpUsed = SelectBp(traveler);

            SkillResult result =
            _battle.ResolveOffensiveSkill(traveler, skill, selectedTarget, bpUsed, selectedWeapon);

            ShowSkillResult(result);

        return true;
    }

    // Método ShowOffensiveSkillResult eliminado. Usar ShowSkillResult con SkillResult.

    private void ShowSkillResult(SkillResult result)
    {
        List<string> impactLines = new();
        List<string> hpLines = new();

        foreach (SkillImpact impact in result.Impacts)
        {
            impactLines.Add($"{impact.TargetName} recibe {impact.Damage} de {impact.DamageDescription}");
        }

        var lastHpByTarget = new Dictionary<string, SkillImpact>();
        foreach (SkillImpact impact in result.Impacts)
        {
            lastHpByTarget[impact.TargetName] = impact;
        }
        var orderedTargets = new List<string>();
        foreach (var impact in result.Impacts)
        {
            if (!orderedTargets.Contains(impact.TargetName))
                orderedTargets.Add(impact.TargetName);
        }
        foreach (var target in orderedTargets)
        {
            var impact = lastHpByTarget[target];
            hpLines.Add($"{impact.TargetName} termina con HP:{impact.TargetRemainingHp}");
        }

        _view.ShowSkillResult(
            result.UserName,
            result.SkillName,
            impactLines,
            hpLines);
    }

    private Unit? SelectSkillTarget(Traveler traveler)
    {
        while (true)
        {
            List<Beast> aliveBeasts = _battle.GameState.BeastTeam.GetAliveBeasts();
            List<string> targetDescriptions = BuildTargetDescriptions(aliveBeasts);

            string input = _view.AskTargetOption(traveler.Name, targetDescriptions);

            int? selectedIndex = SelectOption(aliveBeasts.Count, input);

            if (selectedIndex == null)
            {
                continue;
            }

            if (selectedIndex == -1)
            {
                return null;
            }

            return aliveBeasts[selectedIndex.Value];
        }
    }
  
    private void PlayBeastTurn(Beast beast)
    {
        BeastSkillResult result = _battle.ResolveBeastTurn(beast);

        List<string> impactLines = new();
        List<string> hpLines = new();

        foreach (SkillImpact impact in result.Impacts)
        {
            impactLines.Add(
                $"{impact.TargetName} recibe {impact.Damage} de {impact.DamageDescription}");
        }

        foreach (SkillImpact impact in result.Impacts)
        {
            hpLines.Add(
                $"{impact.TargetName} termina con HP:{impact.TargetRemainingHp}");
        }

        _view.ShowBeastSkillResult(
            result.UserName,
            result.SkillName,
            impactLines,
            hpLines);
    }

    private int SelectBp(Traveler traveler)
    {
        int maxBpUsable = Math.Min(3, traveler.CurrentBp);

        if (maxBpUsable <= 0)
        {
            return 0;
        }

        while (true)
        {
            string input = _view.AskBpOption();

            if (!int.TryParse(input, out int bp))
            {
                continue;
            }

            if (bp >= 0 && bp <= maxBpUsable)
            {
                return bp;
            }
        }
    }
    
    private int? SelectOption(int optionCount, string input)
    {
        if (!int.TryParse(input, out int option))
        {
            return null;
        }

        if (option == optionCount + 1)
        {
            return -1;
        }

        if (option >= 1 && option <= optionCount)
        {
            return option - 1;
        }

        return null;
    }

    private string? SelectWeapon(Traveler traveler)
    {
        while (true)
        {
            List<string> weapons = traveler.Weapons.ToList();
            string input = _view.AskWeaponOption(weapons);

            int? selectedIndex = SelectOption(weapons.Count, input);

            if (selectedIndex == null)
            {
                continue;
            }

            if (selectedIndex == -1)
            {
                return null;
            }

            return weapons[selectedIndex.Value];
        }
    }

    private Beast? SelectBeastTarget(Traveler traveler)
    {
        while (true)
        {
            List<Beast> aliveBeasts = _battle.GameState.BeastTeam.GetAliveBeasts();
            List<string> targetDescriptions = BuildTargetDescriptions(aliveBeasts);

            string input = _view.AskTargetOption(traveler.Name, targetDescriptions);

            int? selectedIndex = SelectOption(aliveBeasts.Count, input);

            if (selectedIndex == null)
            {
                continue;
            }

            if (selectedIndex == -1)
            {
                return null;
            }

            return aliveBeasts[selectedIndex.Value];
        }
    }

    private List<string> BuildPlayerTeamLines()
    {
        List<string> lines = new();

        for (int i = 0; i < _battle.GameState.PlayerTeam.Travelers.Count; i++)
        {
            Traveler traveler = _battle.GameState.PlayerTeam.Travelers[i];
            char letter = (char)('A' + i);

            lines.Add(
                $"{letter}-{traveler.Name} - HP:{traveler.CurrentHp}/{traveler.Stats.MaxHp} SP:{traveler.CurrentSp}/{traveler.MaxSp} BP:{traveler.CurrentBp}");
        }

        return lines;
    }

    private List<string> BuildEnemyTeamLines()
    {
        List<string> lines = new();

        for (int i = 0; i < _battle.GameState.BeastTeam.Beasts.Count; i++)
        {
            Beast beast = _battle.GameState.BeastTeam.Beasts[i];
            char letter = (char)('A' + i);

            lines.Add(
                $"{letter}-{beast.Name} - HP:{beast.CurrentHp}/{beast.Stats.MaxHp} Shields:{beast.CurrentShields}");
        }

        return lines;
    }

    private List<string> BuildRemainingTurnNames()
    {
        List<Unit> remainingTurns = _battle.GetRemainingTurns();
        List<string> names = new();

        foreach (Unit unit in remainingTurns)
        {
            names.Add(unit.Name);
        }

        return names;
    }

    private List<string> BuildNextRoundTurnNames()
    {
        List<Unit> nextRoundTurns = _battle.GetNextRoundPreview();
        List<string> names = new();

        foreach (Unit unit in nextRoundTurns)
        {
            names.Add(unit.Name);
        }

        return names;
    }

    private List<string> BuildTargetDescriptions(List<Beast> beasts)
    {
        List<string> descriptions = new();

        foreach (Beast beast in beasts)
        {
            descriptions.Add(
                $"{beast.Name} - HP:{beast.CurrentHp}/{beast.Stats.MaxHp} Shields:{beast.CurrentShields}");
        }

        return descriptions;
    }

    private void ShowWinner()
    {
        string? winner = _battle.GameState.GetWinner();

        if (winner != null)
        {
            _view.ShowWinner(winner);
        }
    }
}