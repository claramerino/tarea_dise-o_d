namespace Octopath_Traveler_Model;

public class SkillImpact
{
    public string TargetName { get; }
    public int Damage { get; }
    public int TargetRemainingHp { get; }
    public string DamageDescription { get; }

    public SkillImpact(
        string targetName,
        int damage,
        int targetRemainingHp,
        string damageDescription)
    {
        TargetName = targetName;
        Damage = damage;
        TargetRemainingHp = targetRemainingHp;
        DamageDescription = damageDescription;
    }
}

public class BeastSkillResult
{
    public string UserName { get; }
    public string SkillName { get; }
    public IReadOnlyList<SkillImpact> Impacts { get; }

    public BeastSkillResult(
        string userName,
        string skillName,
        List<SkillImpact> impacts)
    {
        UserName = userName;
        SkillName = skillName;
        Impacts = impacts;
    }
}