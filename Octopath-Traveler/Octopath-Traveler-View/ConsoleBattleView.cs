using System.Collections.Generic;
using Octopath_Traveler_View;

namespace Octopath_Traveler;

public class ConsoleBattleView : IBattleView
{
    private readonly View _view;

    public ConsoleBattleView(View view)
    {
        _view = view;
    }

    public void ShowRoundStart(int roundNumber)
    {
        WriteSeparator();
        _view.WriteLine($"INICIA RONDA {roundNumber}");
    }

    public void ShowBattleState(List<string> playerLines, List<string> enemyLines)
    {
        WriteSeparator();
        _view.WriteLine("Equipo del jugador");

        foreach (string line in playerLines)
        {
            _view.WriteLine(line);
        }

        _view.WriteLine("Equipo del enemigo");

        foreach (string line in enemyLines)
        {
            _view.WriteLine(line);
        }
    }

    public void ShowRoundTurns(List<string> remainingTurns, List<string> nextRoundTurns)
    {
        WriteSeparator();
        _view.WriteLine("Turnos de la ronda");
        for (int i = 0; i < remainingTurns.Count; i++)
        {
            _view.WriteLine($"{i + 1}.{remainingTurns[i]}");
        }

        WriteSeparator();
        _view.WriteLine("Turnos de la siguiente ronda");
        for (int i = 0; i < nextRoundTurns.Count; i++)
        {
            _view.WriteLine($"{i + 1}.{nextRoundTurns[i]}");
        }
    }

    public string AskTravelerAction(string travelerName)
    {
        WriteSeparator();
        _view.WriteLine($"Turno de {travelerName}");
        _view.WriteLine("1: Ataque básico");
        _view.WriteLine("2: Usar habilidad");
        _view.WriteLine("3: Defender");
        _view.WriteLine("4: Huir");

        return _view.ReadLine();
    }

    public string AskWeaponOption(List<string> weapons)
    {
        WriteSeparator();
        _view.WriteLine("Seleccione un arma");

        for (int i = 0; i < weapons.Count; i++)
        {
            _view.WriteLine($"{i + 1}: {weapons[i]}");
        }

        _view.WriteLine($"{weapons.Count + 1}: Cancelar");
        return _view.ReadLine();
    }

    public void ShowSkillResult(string userName, string skillName, List<string> impactLines, List<string> hpLines)
    {
        WriteSeparator();
        _view.WriteLine($"{userName} usa {skillName}");

        foreach (string line in impactLines)
        {
            _view.WriteLine(line);
        }

        foreach (string line in hpLines)
        {
            _view.WriteLine(line);
        }
    }

    public string AskTargetOption(string travelerName, List<string> targetDescriptions)
    {
        WriteSeparator();
        _view.WriteLine($"Seleccione un objetivo para {travelerName}");

        for (int i = 0; i < targetDescriptions.Count; i++)
        {
            _view.WriteLine($"{i + 1}: {targetDescriptions[i]}");
        }

        _view.WriteLine($"{targetDescriptions.Count + 1}: Cancelar");
        return _view.ReadLine();
    }

    public string AskBpOption()
    {
        WriteSeparator();
        _view.WriteLine("Seleccione cuantos BP utilizar");
        return _view.ReadLine();
    }

    public string AskSkillOption(string travelerName, List<string> skills)
    {
        WriteSeparator();
        _view.WriteLine($"Seleccione una habilidad para {travelerName}");

        for (int i = 0; i < skills.Count; i++)
        {
            _view.WriteLine($"{i + 1}: {skills[i]}");
        }

        _view.WriteLine($"{skills.Count + 1}: Cancelar");
        return _view.ReadLine();
    }

    public void ShowTravelerAttackResult(string attackerName, string targetName, int damage, string attackType, int targetRemainingHp, bool isWeakness, bool enteredBreakingPoint)
    {
        WriteSeparator();
        _view.WriteLine($"{attackerName} ataca");
        string weaknessText = isWeakness ? " con debilidad" : "";
        _view.WriteLine($"{targetName} recibe {damage} de daño de tipo {attackType}{weaknessText}");
        if (enteredBreakingPoint)
        {
            _view.WriteLine($"{targetName} entra en Breaking Point");
        }
        _view.WriteLine($"{targetName} termina con HP:{targetRemainingHp}");
    }
    

    public void ShowBeastSkillResult(string beastName, string skillName, List<string> impactLines, List<string> hpLines)
    {
        WriteSeparator();
        _view.WriteLine($"{beastName} usa {skillName}");

        foreach (string line in impactLines)
        {
            _view.WriteLine(line);
        }

        foreach (string line in hpLines)
        {
            _view.WriteLine(line);
        }
    }

    public void ShowEscapeMessage()
    {
        WriteSeparator();
        _view.WriteLine("El equipo de viajeros ha huido!");
    }

    public void ShowWinner(string winner)
    {
        WriteSeparator();

        if (winner == "player")
        {
            _view.WriteLine("Gana equipo del jugador");
        }
        else if (winner == "enemy")
        {
            _view.WriteLine("Gana equipo del enemigo");
        }
    }

    private void WriteSeparator()
    {
        _view.WriteLine("----------------------------------------");
    }
}