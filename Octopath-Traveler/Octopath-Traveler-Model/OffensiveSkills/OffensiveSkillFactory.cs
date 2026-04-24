namespace Octopath_Traveler_Model;

public class OffensiveSkillFactory
{
    public OffensiveSkill? Create(string skillName)
    {
        return skillName switch
        {
            "Fireball" => new Fireball(),
            "Icewind" => new Icewind(),
            "Lightning Bolt" => new LightningBolt(),
            "Holy Light" => new HolyLight(),
            "Luminescence" => new LuminescenceSkill(),
            "Tradewinds" => new Tradewinds(),
            "Trade Tempest" => new TradeTempest(),
            "Level Slash" => new LevelSlash(),
            "Cross Strike" => new CrossStrike(),
            "Moonlight Waltz" => new MoonlightWaltz(),
            "Night Ode" => new NightOde(),
            "Icicle" => new Icicle(),
            "Amputation" => new Amputation(),
            "Wildfire" => new Wildfire(),
            "True Strike" => new TrueStrike(),
            "Thunderbird" => new Thunderbird(),
            "Tiger Rage" => new TigerRage(),
            "Qilin’s Horn" => new QilinsHorn(),
            "Yatagarasu" => new Yatagarasu(),
            "Fox Spirit" => new FoxSpirit(),
            "Phoenix Storm" => new PhoenixStorm(),
            "Mercy Strike" => new MercyStrike(),
            "Last Stand" => new LastStand(),
            "Shooting Stars" => new ShootingStars(),
            "Nightmare Chimera" => new NightmareChimera(),
            _ => null
        };
    }
}