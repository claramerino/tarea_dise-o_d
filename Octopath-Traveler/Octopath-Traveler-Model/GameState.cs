namespace Octopath_Traveler_Model;

public class GameState
{
    public PlayerTeam PlayerTeam { get; }
    public BeastTeam BeastTeam { get; }

    public int RoundNumber { get; private set; }
    public bool PlayerHasEscaped { get; private set; }
    public List<Unit> CurrentRoundOrder { get; private set; }
    public int CurrentTurnIndex { get; private set; }

    public GameState(PlayerTeam playerTeam, BeastTeam beastTeam)
    {
        PlayerTeam = playerTeam;
        BeastTeam = beastTeam;
        RoundNumber = 1;
        PlayerHasEscaped = false;
        CurrentRoundOrder = new List<Unit>();
        CurrentTurnIndex = 0;
    }

    public void SetRoundOrder(List<Unit> roundOrder)
    {
        CurrentRoundOrder = roundOrder;
        CurrentTurnIndex = 0;
    }

    public void AdvanceTurn()
    {
        CurrentTurnIndex++;
    }

    public void AdvanceRound()
    {
        RoundNumber++;
        CurrentTurnIndex = 0;
        CurrentRoundOrder = new List<Unit>();
    }

    public void RegisterEscape()
    {
        PlayerHasEscaped = true;
    }

    public bool IsBattleOver()
    {
        return PlayerHasEscaped || PlayerTeam.IsDefeated() || BeastTeam.IsDefeated();
    }

    public string? GetWinner()
    {
        if (BeastTeam.IsDefeated())
        {
            return "player";
        }

        if (PlayerTeam.IsDefeated() || PlayerHasEscaped)
        {
            return "enemy";
        }

        return null;
    }
}