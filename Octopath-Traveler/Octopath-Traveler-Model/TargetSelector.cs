namespace Octopath_Traveler_Model;

public abstract class TargetSelector
{
    public abstract List<Unit> SelectTargets(Unit user, GameState gameState);
}

public class HighestCurrentHpTravelerSelector : TargetSelector
{
    public override List<Unit> SelectTargets(Unit user, GameState gameState)
    {
        Traveler target = gameState.PlayerTeam.GetAliveTravelers()
            .OrderByDescending(traveler => traveler.CurrentHp)
            .First();

        return new List<Unit> { target };
    }
}

public class HighestElementalAttackTravelerSelector : TargetSelector
{
    public override List<Unit> SelectTargets(Unit user, GameState gameState)
    {
        Traveler target = gameState.PlayerTeam.GetAliveTravelers()
            .OrderByDescending(traveler => traveler.ElementalAttack)
            .First();

        return new List<Unit> { target };
    }
}

public class LowestPhysicalDefenseTravelerSelector : TargetSelector
{
    public override List<Unit> SelectTargets(Unit user, GameState gameState)
    {
        Traveler target = gameState.PlayerTeam.GetAliveTravelers()
            .OrderBy(traveler => traveler.PhysicalDefense)
            .First();

        return new List<Unit> { target };
    }
}

public class HighestSpeedTravelerSelector : TargetSelector
{
    public override List<Unit> SelectTargets(Unit user, GameState gameState)
    {
        Traveler target = gameState.PlayerTeam.GetAliveTravelers()
            .OrderByDescending(traveler => traveler.Speed)
            .First();

        return new List<Unit> { target };
    }
}

public class LowestElementalDefenseTravelerSelector : TargetSelector
{
    public override List<Unit> SelectTargets(Unit user, GameState gameState)
    {
        Traveler target = gameState.PlayerTeam.GetAliveTravelers()
            .OrderBy(traveler => traveler.ElementalDefense)
            .First();

        return new List<Unit> { target };
    }
}

public class AllTravelersSelector : TargetSelector
{
    public override List<Unit> SelectTargets(Unit user, GameState gameState)
    {
        return gameState.PlayerTeam
            .GetAliveTravelers()
            .Cast<Unit>()
            .ToList();
    }
}