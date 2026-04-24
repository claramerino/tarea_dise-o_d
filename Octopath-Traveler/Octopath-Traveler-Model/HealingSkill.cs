namespace Octopath_Traveler_Model;

public abstract class HealingSkill : Skill
{
    protected HealingSkill(string name, int spCost) : base(name, spCost) { }

    // Nuevo método para compatibilidad con Skill
    public SkillResult Apply(
        Traveler user,
        GameState gameState,
        Unit? selectedTarget)
    {
        // Por defecto, no usa BP (0)
        return Apply(user, gameState, selectedTarget, 0);
    }

    // Método extendido para skills que usan BP
    public virtual SkillResult Apply(
        Traveler user,
        GameState gameState,
        Unit? selectedTarget,
        int bpUsed)
    {
        List<Unit> targets = GetTargets(gameState, selectedTarget);
        List<SkillImpact> impacts = new();

        double modifier = CalculateFinalModifier(bpUsed);

        foreach (Unit target in targets)
        {
            int healing = (int)Math.Floor(user.Stats.ElementalAttack * modifier);
            target.Heal(healing);
            impacts.Add(new SkillImpact(
                target.Name,
                healing,
                target.CurrentHp,
                "curación"));
        }

        user.SpendSp(SpCost);
        return new SkillResult(user.Name, Name, impacts);
    }

    protected abstract double BaseModifier { get; }
    protected abstract double GetBpScaling(int bpUsed);

    private double CalculateFinalModifier(int bpUsed)
    {
        return BaseModifier + GetBpScaling(bpUsed);
    }

    protected abstract List<Unit> GetTargets(
        GameState gameState,
        Unit? selectedTarget);

    public abstract bool RequiresManualTarget();
}