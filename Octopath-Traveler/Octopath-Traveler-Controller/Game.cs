using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Octopath_Traveler;

public class Game
{
    private const string CharactersFilePath = "data/characters.json";
    private const string EnemiesFilePath = "data/enemies.json";

    private readonly View _view;
    private readonly string _teamsFolder;

    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
    }

    public void Play()
    {
        string selectedFilePath = SelectTeamsFilePath();
        var (travelerLines, beastNames) = LoadTeamsData(selectedFilePath);
        List<TravelerData> travelers = TeamLoader.ParseTravelers(travelerLines);

        if (!TeamsAreValid(travelers, beastNames))
        {
            ShowInvalidTeamsMessage();
            return;
        }

        PlayerTeam playerTeam = CreatePlayerTeam(travelers);
        BeastTeam beastTeam = CreateBeastTeam(beastNames);

        StartBattle(playerTeam, beastTeam);
    }

    private string SelectTeamsFilePath()
    {
        List<string> teamFiles = GetTeamFiles();
        ShowTeamFiles(teamFiles);
        string selectedFile = SelectTeamFile(teamFiles);

        return Path.Combine(_teamsFolder, selectedFile);
    }

    private (List<string> Travelers, List<string> Beasts) LoadTeamsData(string selectedFilePath)
    {
        return TeamLoader.LoadTeamsFromFile(selectedFilePath);
    }

    private bool TeamsAreValid(List<TravelerData> travelers, List<string> beastNames)
    {
        TeamValidator teamValidator = new TeamValidator();
        return teamValidator.IsValid(travelers, beastNames);
    }

    private void ShowInvalidTeamsMessage()
    {
        _view.WriteLine("Archivo de equipos no válido");
    }

    private List<string> GetTeamFiles()
    {
        return Directory
            .GetFiles(_teamsFolder)
            .Select(Path.GetFileName)
            .ToList();
    }

    private void ShowTeamFiles(List<string> teamFiles)
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");

        for (int index = 0; index < teamFiles.Count; index++)
        {
            _view.WriteLine($"{index}: {teamFiles[index]}");
        }
    }

    private string SelectTeamFile(List<string> teamFiles)
    {
        while (true)
        {
            string input = _view.ReadLine();

            if (!int.TryParse(input, out int option))
            {
                continue;
            }

            if (option >= 0 && option < teamFiles.Count)
            {
                return teamFiles[option];
            }
        }
    }

    private PlayerTeam CreatePlayerTeam(List<TravelerData> travelerDataList)
    {
        List<Traveler> travelers = travelerDataList
            .Select(CreateTraveler)
            .ToList();

        return new PlayerTeam(travelers);
    }

    private Traveler CreateTraveler(TravelerData travelerData)
    {
        return UnitLoader.LoadTravelerFromJson(
            travelerData.Name,
            CharactersFilePath,
            travelerData.ActiveSkills,
            travelerData.PassiveSkills);
    }

    private BeastTeam CreateBeastTeam(List<string> beastNames)
    {
        List<Beast> beasts = beastNames
            .Select(CreateBeast)
            .ToList();

        return new BeastTeam(beasts);
    }

    private Beast CreateBeast(string beastName)
    {
        return UnitLoader.LoadBeastFromJson(beastName, EnemiesFilePath);
    }

    private void StartBattle(PlayerTeam playerTeam, BeastTeam beastTeam)
    {
        TurnManager turnManager = new TurnManager();
        DamageCalculator damageCalculator = new DamageCalculator();
        GameState gameState = new GameState(playerTeam, beastTeam);
        Battle battle = new Battle(gameState, turnManager, damageCalculator);
        IBattleView battleView = new ConsoleBattleView(_view);
        BattleController battleController = new BattleController(battle, battleView);

        battleController.Start();
    }
}