namespace Octopath_Traveler_Model;

public class PlayerTeam : Team<Traveler>
{
    public PlayerTeam(List<Traveler> travelers) : base(travelers)
    {
    }

    public IReadOnlyList<Traveler> Travelers => Units;

    public List<Traveler> GetAliveTravelers()
    {
        return GetAliveUnits();
    }

    public Traveler GetTraveler(int index)
    {
        return GetUnitAt(index);
    }
}