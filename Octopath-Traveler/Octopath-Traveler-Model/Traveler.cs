namespace Octopath_Traveler_Model;

public class Traveler : Unit
{
    private const int InitialBp = 1;
    private const int MaxBp = 5;

    private int _currentSp;
    private int _currentBp;

    public Traveler(
        string name,
        Stats stats,
        List<string> weapons,
        List<string> activeSkills,
        List<string> passiveSkills)
        : base(name, ApplyPassiveSkills(stats, passiveSkills))
    {
        Weapons = weapons ?? new List<string>();
        ActiveSkills = activeSkills ?? new List<string>();
        PassiveSkills = passiveSkills ?? new List<string>();
        _currentSp = Stats.MaxSp;
        _currentBp = InitialBp;
    }

    public int CurrentSp => _currentSp;
    public int MaxSp => Stats.MaxSp;
    public int CurrentBp => _currentBp;

    public IReadOnlyList<string> Weapons { get; }
    public IReadOnlyList<string> ActiveSkills { get; }
    public IReadOnlyList<string> PassiveSkills { get; }

    public bool IsDefending { get; private set; }

    public bool HasSpFor(int spCost)
    {
        return _currentSp >= spCost;
    }

    public void SpendSp(int spCost)
    {
        if (spCost < 0)
        {
            return;
        }

        _currentSp = Math.Max(0, _currentSp - spCost);
    }

    public void GainBp(int amount)
    {
        if (amount < 0)
        {
            return;
        }

        _currentBp += amount;

        if (_currentBp > MaxBp)
        {
            _currentBp = MaxBp;
        }
    }

    public void SpendBp(int amount)
    {
        if (amount < 0)
        {
            return;
        }

        _currentBp = Math.Max(0, _currentBp - amount);
    }

    public void Defend()
    {
        IsDefending = true;
    }

    public void ClearDefend()
    {
        IsDefending = false;
    }

    private static Stats ApplyPassiveSkills(Stats baseStats, List<string>? passiveSkillNames)
    {
        if (passiveSkillNames == null || passiveSkillNames.Count == 0)
        {
            return baseStats;
        }

        PassiveSkillFactory factory = new PassiveSkillFactory();
        Stats modifiedStats = baseStats;

        foreach (string passiveSkillName in passiveSkillNames)
        {
            PassiveSkill? passiveSkill = factory.Create(passiveSkillName);

            if (passiveSkill != null)
            {
                modifiedStats = passiveSkill.Apply(modifiedStats);
            }
        }

        return modifiedStats;
    }
}