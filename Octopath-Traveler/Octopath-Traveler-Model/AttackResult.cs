namespace Octopath_Traveler_Model;

public class AttackResult
{
    public string AttackerName { get; }
    public string TargetName { get; }
    public int Damage { get; }
    public string AttackType { get; }
    public int TargetRemainingHp { get; }
    public bool IsWeakness { get; }
    public bool EnteredBreakingPoint { get; }

    public AttackResult(
        string attackerName,
        string targetName,
        int damage,
        string attackType,
        int targetRemainingHp,
        bool isWeakness = false,
        bool enteredBreakingPoint = false)
    {
        AttackerName = attackerName;
        TargetName = targetName;
        Damage = damage;
        AttackType = attackType;
        TargetRemainingHp = targetRemainingHp;
        IsWeakness = isWeakness;
        EnteredBreakingPoint = enteredBreakingPoint;
    }
}