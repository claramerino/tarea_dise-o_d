namespace Octopath_Traveler_Model;

public class AttackBeastSkill : ConfiguredBeastSkill
{
    public AttackBeastSkill()
        : base(
            "Attack",
            new HighestCurrentHpTravelerSelector(),
            new PhysicalDamageEffect(1.3))
    {
    }
}


public class BefuddlingClaw : ConfiguredBeastSkill
{
    public BefuddlingClaw()
        : base(
            "Befuddling claw",
            new HighestElementalAttackTravelerSelector(),
            new PhysicalDamageEffect(1.4))
    {
    }
}

public class Stampede : ConfiguredBeastSkill
{
    public Stampede()
        : base(
            "Stampede",
            new AllTravelersSelector(),
            new PhysicalDamageEffect(1.7))
    {
    }
}

public class IceBlast : ConfiguredBeastSkill
{
    public IceBlast()
        : base(
            "Ice blast",
            new AllTravelersSelector(),
            new ElementalDamageEffect(1.3))
    {
    }
}

public class Stab : ConfiguredBeastSkill
{
    public Stab()
        : base(
            "Stab",
            new LowestPhysicalDefenseTravelerSelector(),
            new PhysicalDamageEffect(1.4))
    {
    }
}

public class Rampage : ConfiguredBeastSkill
{
    public Rampage()
        : base(
            "Rampage",
            new AllTravelersSelector(),
            new PhysicalDamageEffect(1.5))
    {
    }
}

public class MeteorStorm : ConfiguredBeastSkill
{
    public MeteorStorm()
        : base(
            "Meteor Storm",
            new HighestSpeedTravelerSelector(),
            new ElementalDamageEffect(1.3))
    {
    }
}

public class Freeze : ConfiguredBeastSkill
{
    public Freeze()
        : base(
            "Freeze",
            new HighestSpeedTravelerSelector(),
            new ElementalDamageEffect(1.3))
    {
    }
}

public class BeastLuminescence : ConfiguredBeastSkill
{
    public BeastLuminescence()
        : base(
            "Luminescence",
            new HighestSpeedTravelerSelector(),
            new ElementalDamageEffect(1.3))
    {
    }
}

public class Enshadow : ConfiguredBeastSkill
{
    public Enshadow()
        : base(
            "Enshadow",
            new HighestSpeedTravelerSelector(),
            new ElementalDamageEffect(1.3))
    {
    }
}

public class WindSlash : ConfiguredBeastSkill
{
    public WindSlash()
        : base(
            "Wind slash",
            new HighestSpeedTravelerSelector(),
            new ElementalDamageEffect(1.3))
    {
    }
}

public class Incinerate : ConfiguredBeastSkill
{
    public Incinerate()
        : base(
            "Incinerate",
            new AllTravelersSelector(),
            new ElementalDamageEffect(1.5))
    {
    }
}

public class BoarRush : ConfiguredBeastSkill
{
    public BoarRush()
        : base(
            "Boar Rush",
            new LowestPhysicalDefenseTravelerSelector(),
            new PhysicalDamageEffect(1.5))
    {
    }
}

public class Windshot : ConfiguredBeastSkill
{
    public Windshot()
        : base(
            "Windshot",
            new LowestElementalDefenseTravelerSelector(),
            new ElementalDamageEffect(1.3))
    {
    }
}

public class Firesand : ConfiguredBeastSkill
{
    public Firesand()
        : base(
            "Firesand",
            new LowestElementalDefenseTravelerSelector(),
            new ElementalDamageEffect(1.3))
    {
    }
}

public class Thundershot : ConfiguredBeastSkill
{
    public Thundershot()
        : base(
            "Thundershot",
            new LowestElementalDefenseTravelerSelector(),
            new ElementalDamageEffect(1.3))
    {
    }
}

public class Lightshot : ConfiguredBeastSkill
{
    public Lightshot()
        : base(
            "Lightshot",
            new LowestElementalDefenseTravelerSelector(),
            new ElementalDamageEffect(1.3))
    {
    }
}

public class Iceshot : ConfiguredBeastSkill
{
    public Iceshot()
        : base(
            "Iceshot",
            new LowestElementalDefenseTravelerSelector(),
            new ElementalDamageEffect(1.3))
    {
    }
}

public class Shadowshot : ConfiguredBeastSkill
{
    public Shadowshot()
        : base(
            "Shadowshot",
            new LowestElementalDefenseTravelerSelector(),
            new ElementalDamageEffect(1.3))
    {
    }
}

public class VortalClaw : ConfiguredBeastSkill
{
    public VortalClaw()
        : base(
            "Vortal Claw",
            new AllTravelersSelector(),
            new HalveCurrentHpEffect())
    {
    }
}

public class BlackGale : ConfiguredBeastSkill
{
    public BlackGale()
        : base(
            "Black Gale",
            new AllTravelersSelector(),
            new ElementalDamageEffect(1.6))
    {
    }
}

public class VorpalFang : ConfiguredBeastSkill
{
    public VorpalFang()
        : base(
            "Vorpal Fang",
            new LowestPhysicalDefenseTravelerSelector(),
            new PhysicalDamageEffect(1.5))
    {
    }
}

public class Galestorm : ConfiguredBeastSkill
{
    public Galestorm()
        : base(
            "Galestorm",
            new AllTravelersSelector(),
            new ElementalDamageEffect(1.7))
    {
    }
}