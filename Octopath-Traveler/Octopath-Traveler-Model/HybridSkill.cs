namespace Octopath_Traveler_Model;

public abstract class HybridSkill : Skill
{
    protected HybridSkill(string name, int spCost)
        : base(name, spCost)
    {
    }

    protected abstract List<SkillImpact> ApplyEffects(
        Traveler user,
        GameState gameState,
        Unit? selectedTarget);

    public SkillResult Apply(
        Traveler user,
        GameState gameState,
        Unit? selectedTarget)
    {
        List<SkillImpact> impacts = ApplyEffects(user, gameState, selectedTarget);
        user.SpendSp(SpCost);
        return new SkillResult(user.Name, Name, impacts);
    }
}