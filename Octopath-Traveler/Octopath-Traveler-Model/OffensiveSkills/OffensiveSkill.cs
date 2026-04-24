namespace Octopath_Traveler_Model;

public abstract class OffensiveSkill : Skill
{
    protected OffensiveSkill(string name, int spCost) : base(name, spCost) { }

    public virtual SkillResult Apply(
        Traveler user,
        GameState gameState,
        DamageCalculator damageCalculator,
        List<Unit> targets,
        string? selectedWeapon = null,
        int bpUsed = 0)
    {
        List<SkillImpact> impacts = GetEffect().Apply(user, targets, damageCalculator, selectedWeapon, bpUsed);
        user.SpendSp(SpCost);
        return new SkillResult(user.Name, Name, impacts);
    }

    public abstract bool RequiresManualTarget();

    public abstract List<Unit> GetTargets(
        Traveler user,
        GameState gameState,
        Unit? selectedTarget);

    protected abstract SkillEffect GetEffect();
}

public class ConfiguredOffensiveSkill : OffensiveSkill
{
    private readonly bool _requiresManualTarget;
    private readonly SkillEffect _effect;

    public ConfiguredOffensiveSkill(
        string name,
        int spCost,
        bool requiresManualTarget,
        SkillEffect effect)
        : base(name, spCost)
    {
        _requiresManualTarget = requiresManualTarget;
        _effect = effect;
    }

    public override bool RequiresManualTarget()
    {
        return _requiresManualTarget;
    }

    public override List<Unit> GetTargets(
        Traveler user,
        GameState gameState,
        Unit? selectedTarget)
    {
        if (_requiresManualTarget)
        {
            return selectedTarget == null
                ? new List<Unit>()
                : new List<Unit> { selectedTarget };
        }

        return gameState.BeastTeam
            .GetAliveBeasts()
            .Cast<Unit>()
            .ToList();
    }

    protected override SkillEffect GetEffect()
    {
        return _effect;
    }
}