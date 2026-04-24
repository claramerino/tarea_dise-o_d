namespace Octopath_Traveler_Model;

public class ElementalAugmentation : PassiveSkill
{
    private const int Bonus = 50;
    public ElementalAugmentation() : base("Elemental Augmentation") { }

    public override Stats Apply(Stats stats)
    {
        return stats.With(elementalAttack: stats.ElementalAttack + Bonus);
    }
}

public class SummonStrength : PassiveSkill
{
    private const int Bonus = 50;
    public SummonStrength() : base("Summon Strength") { }

    public override Stats Apply(Stats stats)
    {
        return stats.With(physicalAttack: stats.PhysicalAttack + Bonus);
    }
}

public class HaleAndHearty : PassiveSkill
{
    private const int Bonus = 500;
    public HaleAndHearty() : base("Hale and Hearty") { }

    public override Stats Apply(Stats stats)
    {
        return stats.With(maxHp: stats.MaxHp + Bonus);
    }
}

public class Fleefoot : PassiveSkill
{
    private const int Bonus = 50;
    public Fleefoot() : base("Fleefoot") { }

    public override Stats Apply(Stats stats)
    {
        return stats.With(speed: stats.Speed + Bonus);
    }
}

public class InnerStrength : PassiveSkill
{
    private const int Bonus = 50;
    public InnerStrength() : base("Inner Strength") { }

    public override Stats Apply(Stats stats)
    {
        return stats.With(maxSp: stats.MaxSp + Bonus);
    }
}