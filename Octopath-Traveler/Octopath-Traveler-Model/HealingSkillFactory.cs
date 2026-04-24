using Octopath_Traveler_Model;
public class HealingSkillFactory
{
    public HealingSkill? Create(string name)
    {
        return name switch
        {
            "Heal Wounds" => new HealWounds(),
            "Heal More" => new HealMore(),
            "First Aid" => new FirstAid(),
            _ => null
        };
    }
}