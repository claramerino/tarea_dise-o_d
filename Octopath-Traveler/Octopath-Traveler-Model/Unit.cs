namespace Octopath_Traveler_Model;

public abstract class Unit
{
    private int _currentHp;

    protected Unit(string name, Stats stats)
    {
        Name = name;
        Stats = stats;
        _currentHp = stats.MaxHp;
    }

    public string Name { get; }
    public Stats Stats { get; }

    public int Speed => Stats.Speed;
    public int PhysicalAttack => Stats.PhysicalAttack;
    public int PhysicalDefense => Stats.PhysicalDefense;
    public int ElementalAttack => Stats.ElementalAttack;
    public int ElementalDefense => Stats.ElementalDefense;

    public int CurrentHp => _currentHp;
    public bool IsDead => _currentHp <= 0;
    public virtual bool IsBreakingPoint => false;

    public void ReceiveDamage(int damage)
    {
        int validDamage = Math.Max(0, damage);
        _currentHp = Math.Max(0, _currentHp - validDamage);
    }

    public void RecoverHp(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _currentHp = Math.Min(Stats.MaxHp, _currentHp + amount);
    }

    protected void SetCurrentHp(int hp)
    {
        _currentHp = Math.Max(0, Math.Min(Stats.MaxHp, hp));
    }

    public void Heal(int amount)
    {
        if (IsDead)
        {
            return;
        }

        _currentHp = Math.Min(_currentHp + amount, Stats.MaxHp);
    }
    public void Revive(int hp)
    {
        if (!IsDead)
        {
            return;
        }

        _currentHp = Math.Min(hp, Stats.MaxHp);
    }
}