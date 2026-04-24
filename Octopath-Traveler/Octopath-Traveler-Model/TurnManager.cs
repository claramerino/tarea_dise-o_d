using Octopath_Traveler_Model;
using System.Collections.Generic;
using System.Linq;

public class TurnManager
{
    public List<Unit> BuildRoundOrder(PlayerTeam playerTeam, BeastTeam beastTeam)
    {
        List<Unit> units = GetAliveUnits(playerTeam, beastTeam);

        return units
            .OrderByDescending(unit => unit.Speed)
            .ThenBy(unit => GetUnitPriority(unit))
            .ThenBy(unit => GetBoardPosition(unit, playerTeam, beastTeam))
            .ToList();
    }

    public List<Unit> BuildRemainingTurnOrder(List<Unit> currentRoundOrder, int currentTurnIndex)
    {
        return currentRoundOrder
            .Skip(currentTurnIndex)
            .Where(unit => !unit.IsDead && !unit.IsBreakingPoint)
            .ToList();
    }

    public List<Unit> BuildNextRoundPreview(PlayerTeam playerTeam, BeastTeam beastTeam)
    {
        List<Unit> units = new();
        units.AddRange(playerTeam.GetAliveTravelers());
        units.AddRange(beastTeam.GetAliveBeasts().Where(b => b.WillBeActiveNextRound));

        return units
            .OrderByDescending(unit => unit.Speed)
            .ThenBy(unit => GetUnitPriority(unit))
            .ThenBy(unit => GetBoardPosition(unit, playerTeam, beastTeam))
            .ToList();
    }

    private List<Unit> GetAliveUnits(PlayerTeam playerTeam, BeastTeam beastTeam)
    {
        List<Unit> units = new();

        units.AddRange(playerTeam.GetAliveTravelers());
        units.AddRange(beastTeam.GetAliveBeasts().Where(b => !b.IsBreakingPoint));

        return units;
    }

    private UnitPriority GetUnitPriority(Unit unit)
    {
        return unit is Traveler ? UnitPriority.Traveler : UnitPriority.Beast;
    }

    private int GetBoardPosition(Unit unit, PlayerTeam playerTeam, BeastTeam beastTeam)
    {
        if (unit is Traveler traveler)
        {
            return GetUnitPosition(traveler, playerTeam.Travelers);
        }

        Beast beast = (Beast)unit;
        return GetUnitPosition(beast, beastTeam.Beasts);
    }

    private int GetUnitPosition<TUnit>(TUnit unit, IReadOnlyList<TUnit> units)
    {
        for (int index = 0; index < units.Count; index++)
        {
            if (ReferenceEquals(units[index], unit))
            {
                return index;
            }
        }

        return int.MaxValue;
    }
}