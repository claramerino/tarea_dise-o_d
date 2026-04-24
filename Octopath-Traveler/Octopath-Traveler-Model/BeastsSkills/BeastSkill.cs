namespace Octopath_Traveler_Model;

public abstract class BeastSkill
{
    public string Name { get; }

    protected BeastSkill(string name)
    {
        Name = name;
    }

    public BeastSkillResult Apply(
        Beast user,
        GameState gameState,
        DamageCalculator damageCalculator)
    {
        List<Unit> targets = GetTargetSelector().SelectTargets(user, gameState);
        List<SkillImpact> impacts = GetEffect().Apply(user, targets, damageCalculator);

        return new BeastSkillResult(user.Name, Name, impacts);
    }

    protected abstract TargetSelector GetTargetSelector();
    protected abstract SkillEffect GetEffect();
}

public class ConfiguredBeastSkill : BeastSkill
{
    private readonly TargetSelector _targetSelector;
    private readonly SkillEffect _effect;

    public ConfiguredBeastSkill(
        string name,
        TargetSelector targetSelector,
        SkillEffect effect)
        : base(name)
    {
        _targetSelector = targetSelector;
        _effect = effect;
    }

    protected override TargetSelector GetTargetSelector()
    {
        return _targetSelector;
    }

    protected override SkillEffect GetEffect()
    {
        return _effect;
    }
}