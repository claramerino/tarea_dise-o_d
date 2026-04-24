using Octopath_Traveler_Model;

public class DamageCalculator
{
    private const double BasicAttackModifier = 1.3;

    public int CalculateBasicPhysicalDamage(Unit attacker, Unit defender)
    {
        return CalculatePhysicalDamage(attacker, defender, BasicAttackModifier);
    }

    public int CalculatePhysicalDamage(Unit attacker, Unit defender, double modifier)
    {
        double rawDamage = attacker.PhysicalAttack * modifier - defender.PhysicalDefense;
        int damage = (int)Math.Floor(rawDamage);

        return Math.Max(0, damage);
    }

    public int CalculateElementalDamage(Unit attacker, Unit defender, double modifier)
    {
        double rawDamage = attacker.ElementalAttack * modifier - defender.ElementalDefense;
        int damage = (int)Math.Floor(rawDamage);

        return Math.Max(0, damage);
    }

    public int CalculateElementalDamage(Unit attacker, Unit defender, double modifier, DamageType type)
    {
        double rawDamage = attacker.ElementalAttack * modifier - defender.ElementalDefense;

        int damage = (int)Math.Floor(rawDamage);

        return Math.Max(0, damage);
    }

    public double CalculateRawPhysicalDamage(Unit attacker, Unit defender, double modifier)
    {
        return attacker.PhysicalAttack * modifier - defender.PhysicalDefense;
    }

    public int CalculateBasicPhysicalDamageWithModifiers(
        Unit attacker,
        Unit defender,
        bool isWeakness,
        bool isBreakingPoint)
    {
        int baseDamage = CalculateBasicPhysicalDamage(attacker, defender);
        double multiplier = 1.0 + (isWeakness ? 0.5 : 0) + (isBreakingPoint ? 0.5 : 0);
        return (int)Math.Floor(baseDamage * multiplier);
    }
}