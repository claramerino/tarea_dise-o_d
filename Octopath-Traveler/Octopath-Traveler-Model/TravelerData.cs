namespace Octopath_Traveler_Model;

public class TravelerData
{
    public string Name { get; }
    public List<string> ActiveSkills { get; }
    public List<string> PassiveSkills { get; }

    public TravelerData(string name, List<string> activeSkills, List<string> passiveSkills)
    {
        Name = name;
        ActiveSkills = activeSkills;
        PassiveSkills = passiveSkills;
    }
}