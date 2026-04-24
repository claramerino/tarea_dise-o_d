namespace Octopath_Traveler_Model;

public abstract class TurnOrderSkill : Skill
{
    protected TurnOrderSkill(string name, int spCost)
        : base(name, spCost)
    {
    }

    protected abstract void ModifyTurnOrder(
        Traveler user,
        GameState gameState,
        Unit? target);

    public SkillResult Apply(
        Traveler user,
        GameState gameState,
        Unit? selectedTarget)
    {
        ModifyTurnOrder(user, gameState, selectedTarget);
        user.SpendSp(SpCost);
        return new SkillResult(
            user.Name,
            Name,
            new List<SkillImpact>()); // o mensaje especial
    }
}