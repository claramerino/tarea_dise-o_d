namespace Octopath_Traveler_Model;

public abstract class Team<TUnit> where TUnit : Unit
{
    private readonly List<TUnit> _units;

    protected Team(List<TUnit> units)
    {
        _units = units ?? throw new ArgumentNullException(nameof(units));
    }

    protected IReadOnlyList<TUnit> Units => _units;

    public int Count => _units.Count;

    public bool IsDefeated()
    {
        return _units.All(unit => unit.IsDead);
    }

    protected List<TUnit> GetAliveUnits()
    {
        return _units.Where(unit => !unit.IsDead).ToList();
    }

    protected TUnit GetUnitAt(int index)
    {
        return _units[index];
    }
}