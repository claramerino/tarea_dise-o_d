namespace Octopath_Traveler_Model;

public class SkillFactory
{
    public Skill Create(string name)
    {
        return name switch
        {
            "Heal Wounds" => new HealWounds(),
            "Heal More" => new HealMore(),
            "First Aid" => new FirstAid(),
            _ => throw new Exception("Skill no encontrada")
        };
    }
}