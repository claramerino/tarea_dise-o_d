using System.Collections.Generic;
using System.Linq;
using System;

namespace Octopath_Traveler_Model;

public class Battle
{
    private readonly GameState _gameState;
    private readonly TurnManager _turnManager;
    private readonly DamageCalculator _damageCalculator;

    public Battle(
        GameState gameState,
        TurnManager turnManager,
        DamageCalculator damageCalculator)
    {
        _gameState = gameState;
        _turnManager = turnManager;
        _damageCalculator = damageCalculator;
    }

    public GameState GameState => _gameState;

    public void StartRound()
    {
        if (_gameState.RoundNumber > 1)
        {
            foreach (Traveler traveler in _gameState.PlayerTeam.GetAliveTravelers())
            {
                traveler.GainBp(1);
            }
        }

        List<Unit> roundOrder = _turnManager.BuildRoundOrder(
            _gameState.PlayerTeam,
            _gameState.BeastTeam);

        _gameState.SetRoundOrder(roundOrder);
    }

    public List<Unit> GetRemainingTurns()
    {
        return _turnManager.BuildRemainingTurnOrder(
            _gameState.CurrentRoundOrder,
            _gameState.CurrentTurnIndex);
    }

    public List<Unit> GetNextRoundPreview()
    {
        return _turnManager.BuildNextRoundPreview(
            _gameState.PlayerTeam,
            _gameState.BeastTeam);
    }

    public Unit? GetCurrentUnit()
    {
        if (_gameState.CurrentTurnIndex >= _gameState.CurrentRoundOrder.Count)
        {
            return null;
        }

        return _gameState.CurrentRoundOrder[_gameState.CurrentTurnIndex];
    }

    public void AdvanceTurn()
    {
        _gameState.AdvanceTurn();
    }

    public void AdvanceRound()
    {
        _gameState.AdvanceRound();

        foreach (Traveler traveler in _gameState.PlayerTeam.Travelers)
        {
            traveler.ClearDefend();
        }

        foreach (Beast beast in _gameState.BeastTeam.Beasts)
        {
            beast.AdvanceBreakingPoint();
        }
    }

    public void Escape()
    {
        _gameState.RegisterEscape();
    }

    public void ResolveDefend(Traveler traveler)
    {
        traveler.Defend();
    }

    public AttackResult ResolveBasicAttack(Traveler attacker, Beast target, string weapon, int bpUsed)
    {
        int validBp = Math.Max(0, Math.Min(bpUsed, Math.Min(3, attacker.CurrentBp)));
        attacker.SpendBp(validBp);

        bool isWeakness = target.IsWeakTo(weapon);
        bool wasAlreadyInBreakingPoint = target.IsBreakingPoint;

        int damage = _damageCalculator.CalculateBasicPhysicalDamageWithModifiers(
            attacker, target, isWeakness, wasAlreadyInBreakingPoint);
        target.ReceiveDamage(damage);

        bool enteredBreakingPoint = false;
        if (isWeakness && !wasAlreadyInBreakingPoint)
        {
            target.LoseShield();
            if (target.CurrentShields == 0)
            {
                target.EnterBreakingPoint();
                enteredBreakingPoint = true;
            }
        }

        return new AttackResult(
            attacker.Name,
            target.Name,
            damage,
            weapon,
            target.CurrentHp,
            isWeakness,
            enteredBreakingPoint);
    }

    public BeastSkillResult ResolveBeastTurn(Beast beast)
    {
        BeastSkillFactory factory = new BeastSkillFactory();
        BeastSkill? skill = factory.Create(beast.SkillName);

        if (skill == null)
        {
            throw new InvalidOperationException(
                $"Habilidad de bestia desconocida: {beast.SkillName}");
        }
        return skill.Apply(beast, _gameState, _damageCalculator);
    }


    public SkillResult ResolveOffensiveSkill(
        Traveler user,
        OffensiveSkill skill,
        Unit? selectedTarget,
        int bpUsed,
        string? selectedWeapon = null)
    {
        user.SpendBp(bpUsed);
        var targets = skill.GetTargets(user, _gameState, selectedTarget);
        return skill.Apply(user, _gameState, _damageCalculator, targets, selectedWeapon, bpUsed);
    }

}