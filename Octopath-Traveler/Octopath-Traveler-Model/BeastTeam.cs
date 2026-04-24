namespace Octopath_Traveler_Model;

public class BeastTeam : Team<Beast>
{
    public BeastTeam(List<Beast> beasts) : base(beasts)
    {
    }

    public IReadOnlyList<Beast> Beasts => Units;

    public List<Beast> GetAliveBeasts()
    {
        return GetAliveUnits();
    }

    public Beast GetBeastAt(int index)
    {
        return GetUnitAt(index);
    }
}