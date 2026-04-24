using Octopath_Traveler_Model;
public class HealWounds : HealingSkill
{
    private const int SpCostValue = 6;

    protected override double BaseModifier => 1.5;

    public HealWounds() : base("Heal Wounds", SpCostValue)
    {
    }

    protected override double GetBpScaling(int bpUsed)
    {
        return 0.5 * bpUsed;
    }

    protected override List<Unit> GetTargets(
        GameState gameState,
        Unit? selectedTarget)
    {
        return gameState.PlayerTeam
            .Travelers
            .Cast<Unit>()
            .ToList();
    }

    public override bool RequiresManualTarget()
    {
        return false;
    }
}

public class HealMore : HealingSkill
{
    private const int SpCostValue = 25;

    protected override double BaseModifier => 2.0;

    public HealMore() : base("Heal More", SpCostValue)
    {
    }

    protected override double GetBpScaling(int bpUsed)
    {
        return BaseModifier * 0.3 * bpUsed;
    }

    protected override List<Unit> GetTargets(
        GameState gameState,
        Unit? selectedTarget)
    {
        return gameState.PlayerTeam
            .Travelers
            .Cast<Unit>()
            .ToList();
    }

    public override bool RequiresManualTarget()
    {
        return false;
    }
}

public class FirstAid : HealingSkill
{
    private const int SpCostValue = 4;

    protected override double BaseModifier => 1.5;

    public FirstAid() : base("First Aid", SpCostValue)
    {
    }

    protected override double GetBpScaling(int bpUsed)
    {
        return BaseModifier * 0.4 * bpUsed;
    }

    protected override List<Unit> GetTargets(
        GameState gameState,
        Unit? selectedTarget)
    {
        return selectedTarget == null
            ? new List<Unit>()
            : new List<Unit> { selectedTarget };
    }

    public override bool RequiresManualTarget()
    {
        return true;
    }
}