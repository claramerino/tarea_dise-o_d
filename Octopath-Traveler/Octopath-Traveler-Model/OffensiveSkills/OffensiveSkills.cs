
namespace Octopath_Traveler_Model;

public class Fireball : ConfiguredOffensiveSkill
{
    public Fireball() : base("Fireball", 8, false, new TypedElementalDamageEffect(1.5, "Fire"))
    {
    }
}

public class Icewind : ConfiguredOffensiveSkill
{
    public Icewind() : base("Icewind", 8, false, new TypedElementalDamageEffect(1.5, "Ice"))
    {
    }
}

public class LightningBolt : ConfiguredOffensiveSkill
{
    public LightningBolt() : base("Lightning Bolt", 8, false, new TypedElementalDamageEffect(1.5, "Lightning"))
    {
    }
}

public class HolyLight : ConfiguredOffensiveSkill
{
    public HolyLight() : base("Holy Light", 6, true, new TypedElementalDamageEffect(1.5, "Light"))
    {
    }
}

public class LuminescenceSkill : ConfiguredOffensiveSkill
{
    public LuminescenceSkill() : base("Luminescence", 9, false, new TypedElementalDamageEffect(1.5, "Light"))
    {
    }
}

public class Tradewinds : ConfiguredOffensiveSkill
{
    public Tradewinds() : base("Tradewinds", 7, true, new TypedElementalDamageEffect(1.5, "Wind"))
    {
    }
}

public class TradeTempest : ConfiguredOffensiveSkill
{
    public TradeTempest() : base("Trade Tempest", 10, false, new TypedElementalDamageEffect(1.5, "Wind"))
    {
    }
}

public class LevelSlash : ConfiguredOffensiveSkill
{
    public LevelSlash() : base("Level Slash", 9, false, new TypedPhysicalDamageEffect(1.5, "Sword"))
    {
    }
}

public class CrossStrike : ConfiguredOffensiveSkill
{
    public CrossStrike() : base("Cross Strike", 12, true, new TypedPhysicalDamageEffect(1.7, "Sword"))
    {
    }
}

public class MoonlightWaltz : ConfiguredOffensiveSkill
{
    public MoonlightWaltz() : base("Moonlight Waltz", 7, true, new TypedElementalDamageEffect(1.6, "Dark"))
    {
    }
}

public class NightOde : ConfiguredOffensiveSkill
{
    public NightOde() : base("Night Ode", 10, false, new TypedElementalDamageEffect(1.6, "Dark"))
    {
    }
}

public class Icicle : ConfiguredOffensiveSkill
{
    public Icicle() : base("Icicle", 7, true, new TypedElementalDamageEffect(1.5, "Ice"))
    {
    }
}

public class Amputation : ConfiguredOffensiveSkill
{
    public Amputation() : base("Amputation", 8, true, new TypedPhysicalDamageEffect(1.7, "Axe"))
    {
    }
}

public class Wildfire : ConfiguredOffensiveSkill
{
    public Wildfire() : base("Wildfire", 7, true, new TypedElementalDamageEffect(1.6, "Fire"))
    {
    }
}

public class TrueStrike : ConfiguredOffensiveSkill
{
    public TrueStrike() : base("True Strike", 10, true, new TypedPhysicalDamageEffect(2.0, "Bow"))
    {
    }
}

public class Thunderbird : ConfiguredOffensiveSkill
{
    public Thunderbird() : base("Thunderbird", 7, true, new TypedElementalDamageEffect(1.6, "Lightning"))
    {
    }
}

public class TigerRage : ConfiguredOffensiveSkill
{
    public TigerRage() : base("Tiger Rage", 35, false, new TypedPhysicalDamageEffect(1.9, "Axe"))
    {
    }
}

public class QilinsHorn : ConfiguredOffensiveSkill
{
    public QilinsHorn() : base("Qilin’s Horn", 35, true, new TypedPhysicalDamageEffect(2.1, "Spear"))
    {
    }
}

public class Yatagarasu : ConfiguredOffensiveSkill
{
    public Yatagarasu() : base("Yatagarasu", 35, false, new TypedPhysicalDamageEffect(1.9, "Dagger"))
    {
    }
}

public class FoxSpirit : ConfiguredOffensiveSkill
{
    public FoxSpirit() : base("Fox Spirit", 35, false, new TypedPhysicalDamageEffect(1.9, "Stave"))
    {
    }
}

public class PhoenixStorm : ConfiguredOffensiveSkill
{
    public PhoenixStorm() : base("Phoenix Storm", 35, true, new TypedPhysicalDamageEffect(2.1, "Bow"))
    {
    }
}

public class MercyStrike : OffensiveSkill
{
    private const int SpCostValue = 4;

    public MercyStrike() : base("Mercy Strike", SpCostValue)
    {
    }

    public override bool RequiresManualTarget()
    {
        return true;
    }

    public override List<Unit> GetTargets(
        Traveler user,
        GameState gameState,
        Unit? selectedTarget)
    {
        return selectedTarget == null
            ? new List<Unit>()
            : new List<Unit> { selectedTarget };
    }

    protected override SkillEffect GetEffect()
    {
        return new MercyStrikeEffect();
    }
}

public class LastStand : OffensiveSkill
{
    private const int SpCostValue = 16;

    public LastStand() : base("Last Stand", SpCostValue)
    {
    }

    public override bool RequiresManualTarget()
    {
        return false;
    }

    public override List<Unit> GetTargets(
        Traveler user,
        GameState gameState,
        Unit? selectedTarget)
    {
        return gameState.BeastTeam
            .GetAliveBeasts()
            .Cast<Unit>()
            .ToList();
    }

    protected override SkillEffect GetEffect()
    {
        return new LastStandEffect();
    }
}

public class ShootingStars : OffensiveSkill
{
    private const int SpCostValue = 35;
    private const double BaseModifier = 0.9;
    private static readonly string[] AttackTypes = { "Wind", "Light", "Dark" };

    public ShootingStars() : base("Shooting Stars", SpCostValue)
    {
    }

    public override bool RequiresManualTarget() => false;

    public override List<Unit> GetTargets(
        Traveler user,
        GameState gameState,
        Unit? selectedTarget)
    {
        return gameState.BeastTeam.GetAliveBeasts().Cast<Unit>().ToList();
    }

    protected override SkillEffect GetEffect()
    {
        return new ShootingStarsEffect();
    }
}

public class NightmareChimera : OffensiveSkill
{
    private const int SpCostValue = 35;
    private const double BaseModifier = 1.9;

    public NightmareChimera() : base("Nightmare Chimera", SpCostValue) { }

    public override bool RequiresManualTarget() => true;

    public override List<Unit> GetTargets(
        Traveler user,
        GameState gameState,
        Unit? selectedTarget)
    {
        return selectedTarget == null ? new List<Unit>() : new List<Unit> { selectedTarget };
    }

    protected override SkillEffect GetEffect()
    {
        return new NightmareChimeraEffect();
    }
}
