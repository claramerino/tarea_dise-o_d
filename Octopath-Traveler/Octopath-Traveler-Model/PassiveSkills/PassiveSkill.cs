namespace Octopath_Traveler_Model;

public abstract class PassiveSkill
{
    public string Name { get; }

    protected PassiveSkill(string name)
    {
        Name = name;
    }

    public abstract Stats Apply(Stats stats);
}