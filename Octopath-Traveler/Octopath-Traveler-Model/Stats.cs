namespace Octopath_Traveler_Model;

public class Stats
{
    public int MaxHp { get; }
    public int MaxSp { get; }
    public int PhysicalAttack { get; }
    public int PhysicalDefense { get; }
    public int ElementalAttack { get; }
    public int ElementalDefense { get; }
    public int Speed { get; }

    public Stats(
        int maxHp,
        int maxSp,
        int physicalAttack,
        int physicalDefense,
        int elementalAttack,
        int elementalDefense,
        int speed)
    {
        MaxHp = maxHp;
        MaxSp = maxSp;
        PhysicalAttack = physicalAttack;
        PhysicalDefense = physicalDefense;
        ElementalAttack = elementalAttack;
        ElementalDefense = elementalDefense;
        Speed = speed;
    }

    public Stats With(
        int? maxHp = null,
        int? maxSp = null,
        int? physicalAttack = null,
        int? physicalDefense = null,
        int? elementalAttack = null,
        int? elementalDefense = null,
        int? speed = null)
    {
        return new Stats(
            maxHp ?? MaxHp,
            maxSp ?? MaxSp,
            physicalAttack ?? PhysicalAttack,
            physicalDefense ?? PhysicalDefense,
            elementalAttack ?? ElementalAttack,
            elementalDefense ?? ElementalDefense,
            speed ?? Speed);
    }
}
