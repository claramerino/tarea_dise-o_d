namespace Octopath_Traveler_Model;

public class PassiveSkillFactory
{
    public PassiveSkill? Create(string passiveSkillName)
    {
        return passiveSkillName switch
        {
            "Elemental Augmentation" => new ElementalAugmentation(),
            "Summon Strength" => new SummonStrength(),
            "Hale and Hearty" => new HaleAndHearty(),
            "Fleefoot" => new Fleefoot(),
            "Inner Strength" => new InnerStrength(),
        };
    }
}