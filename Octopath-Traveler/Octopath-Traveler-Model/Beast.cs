namespace Octopath_Traveler_Model;

public class Beast : Unit
{
    private int _currentShields;
    private readonly List<string> _weaknesses;
    private bool _isBreakingPoint;
    private int _breakingPointRoundsLeft;

    public Beast(string name, Stats stats, string skillName, int shields, List<string>? weaknesses = null)
        : base(name, stats)
    {
        SkillName = skillName;
        MaxShields = shields;
        _currentShields = shields;
        _weaknesses = weaknesses ?? new List<string>();
    }

    public string SkillName { get; }
    public int MaxShields { get; }
    public int CurrentShields => _currentShields;
    public override bool IsBreakingPoint => _isBreakingPoint;

    public bool IsWeakTo(string weapon)
    {
        return _weaknesses.Contains(weapon);
    }

    public void LoseShield(int amount = 1)
    {
        if (amount <= 0)
        {
            return;
        }

        _currentShields = Math.Max(0, _currentShields - amount);
    }

    public void ResetShields()
    {
        _currentShields = MaxShields;
    }

    public bool WillBeActiveNextRound => !_isBreakingPoint || _breakingPointRoundsLeft == 1;

    public void EnterBreakingPoint()
    {
        _isBreakingPoint = true;
        _breakingPointRoundsLeft = 2;
    }

    public void AdvanceBreakingPoint()
    {
        if (!_isBreakingPoint)
        {
            return;
        }

        _breakingPointRoundsLeft--;

        if (_breakingPointRoundsLeft <= 0)
        {
            _isBreakingPoint = false;
            ResetShields();
        }
    }
}