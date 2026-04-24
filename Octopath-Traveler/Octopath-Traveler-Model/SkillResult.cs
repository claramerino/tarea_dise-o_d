using Octopath_Traveler_Model;
public class SkillResult
{
    public string UserName { get; }
    public string SkillName { get; }
    public List<SkillImpact> Impacts { get; }

    public SkillResult(string userName, string skillName, List<SkillImpact> impacts)
    {
        UserName = userName;
        SkillName = skillName;
        Impacts = impacts;
    }
}