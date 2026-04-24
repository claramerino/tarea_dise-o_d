namespace Octopath_Traveler_Model;

public class BeastSkillFactory
{
    public BeastSkill? Create(string skillName)
    {
        return skillName switch
        {
            "Attack" => new AttackBeastSkill(),
            "Befuddling claw" => new BefuddlingClaw(),
            "Stampede" => new Stampede(),
            "Ice blast" => new IceBlast(),
            "Stab" => new Stab(),
            "Rampage" => new Rampage(),
            "Meteor Storm" => new MeteorStorm(),
            "Freeze" => new Freeze(),
            "Luminescence" => new BeastLuminescence(),
            "Enshadow" => new Enshadow(),
            "Wind slash" => new WindSlash(),
            "Incinerate" => new Incinerate(),
            "Boar Rush" => new BoarRush(),
            "Windshot" => new Windshot(),
            "Firesand" => new Firesand(),
            "Thundershot" => new Thundershot(),
            "Lightshot" => new Lightshot(),
            "Iceshot" => new Iceshot(),
            "Shadowshot" => new Shadowshot(),
            "Vortal Claw" => new VortalClaw(),
            "Black Gale" => new BlackGale(),
            "Vorpal Fang" => new VorpalFang(),
            "Galestorm" => new Galestorm(),
            _ => null
        };
    }
}