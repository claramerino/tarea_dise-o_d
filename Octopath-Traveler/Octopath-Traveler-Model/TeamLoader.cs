using System.Collections.Generic;
using System.Linq;
using System.IO;
using Octopath_Traveler_Model;

public class TeamLoader
{
    private const string EnemyTeamHeader = "Enemy Team";

    public static (List<string> Travelers, List<string> Beasts) LoadTeamsFromFile(string filePath)
    {
        List<string> lines = File.ReadAllLines(filePath).ToList();
        int enemyTeamStartIndex = FindEnemyTeamStartIndex(lines, EnemyTeamHeader);

        List<string> travelers = ExtractTravelerLines(lines, enemyTeamStartIndex);
        List<string> beasts = ExtractBeastLines(lines, enemyTeamStartIndex);

        return (travelers, beasts);
    }

    public static List<TravelerData> ParseTravelers(List<string> travelerLines)
    {
        return travelerLines
            .Select(ParseTravelerData)
            .ToList();
    }

    public static TravelerData ParseTravelerData(string travelerDataString)
    {
        var (name, activeSkills, passiveSkills) = ParseTravelerString(travelerDataString);
        return new TravelerData(name, activeSkills, passiveSkills);
    }

    public static (string Name, List<string> ActiveSkills, List<string> PassiveSkills) ParseTravelerString(string travelerDataString)
    {
        List<string> activeSkills = ExtractSkills(travelerDataString, "(");
        List<string> passiveSkills = ExtractSkills(travelerDataString, "[");

        string name = ExtractTravelerName(travelerDataString);

        return (name, activeSkills, passiveSkills);
    }

    private static int FindEnemyTeamStartIndex(List<string> lines, string enemyTeamHeader)
    {
        return lines.FindIndex(line => line == enemyTeamHeader);
    }

    private static List<string> ExtractTravelerLines(List<string> lines, int enemyTeamStartIndex)
    {
        List<string> travelers = new();

        for (int i = 1; i < enemyTeamStartIndex; i++)
        {
            travelers.Add(lines[i]);
        }

        return travelers;
    }

    private static List<string> ExtractBeastLines(List<string> lines, int enemyTeamStartIndex)
    {
        List<string> beasts = new();

        for (int i = enemyTeamStartIndex + 1; i < lines.Count; i++)
        {
            beasts.Add(lines[i]);
        }

        return beasts;
    }

    private static string ExtractTravelerName(string travelerDataString)
    {
        string nameWithoutActiveSkills = travelerDataString.Split('(')[0];
        string nameWithoutPassiveSkills = nameWithoutActiveSkills.Split('[')[0];

        return nameWithoutPassiveSkills.Trim();
    }

    private static List<string> ExtractSkills(string travelerDataString, string section)
    {
        if (!ContainsSection(travelerDataString, section))
        {
            return new List<string>();
        }

        string skillsText = GetSectionContent(travelerDataString, section);
        return ParseSkillList(skillsText);
    }

    private static bool ContainsSection(string travelerDataString, string section)
    {
        return travelerDataString.Contains(section);
    }

    private static string GetSectionContent(string travelerDataString, string section)
    {
        return travelerDataString
            .Split(section)[1]
            .Split(GetSectionClosingDelimiter(section))[0];
    }

    private static string GetSectionClosingDelimiter(string section)
    {
        return section == "(" ? ")" : "]";
    }

    public static List<string> ParseSkillList(string skillsText)
    {
        return skillsText
            .Split(',')
            .Select(skill => skill.Trim())
            .Where(skill => !string.IsNullOrWhiteSpace(skill))
            .ToList();
    }
}