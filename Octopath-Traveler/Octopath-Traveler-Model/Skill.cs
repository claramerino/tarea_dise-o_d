namespace Octopath_Traveler_Model;

public abstract class Skill
{
    public string Name { get; }
    public int SpCost { get; }

    protected Skill(string name, int spCost)
    {
        Name = name;
        SpCost = spCost;
    }

    // La firma base de Skill no requiere Apply universal para todos los tipos
}