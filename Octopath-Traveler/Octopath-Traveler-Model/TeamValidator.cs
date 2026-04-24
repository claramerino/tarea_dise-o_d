namespace Octopath_Traveler_Model;

using System.Collections.Generic;
using System.Linq;

public class TeamValidator
{
    private const string CharactersFilePath = "data/characters.json";
    private const string EnemiesFilePath = "data/enemies.json";
    private const string SkillsFilePath = "data/skills.json";
    private const string PassiveSkillsFilePath = "data/passive_skills.json";

    private const int MinimumTravelers = 1;
    private const int MaximumTravelers = 4;
    private const int MinimumBeasts = 1;
    private const int MaximumBeasts = 5;
    private const int MaximumActiveSkills = 8;
    private const int MaximumPassiveSkills = 4;

    private readonly HashSet<string> _validTravelerNames;
    private readonly HashSet<string> _validBeastNames;
    private readonly HashSet<string> _validActiveSkillNames;
    private readonly HashSet<string> _validPassiveSkillNames;

    public TeamValidator()
    {
        _validTravelerNames = new HashSet<string>(UnitLoader.LoadNamesFromJson(CharactersFilePath));
        _validBeastNames = new HashSet<string>(UnitLoader.LoadNamesFromJson(EnemiesFilePath));
        _validActiveSkillNames = new HashSet<string>(UnitLoader.LoadNamesFromJson(SkillsFilePath));
        _validPassiveSkillNames = new HashSet<string>(UnitLoader.LoadNamesFromJson(PassiveSkillsFilePath));
    }

    public bool IsValid(List<TravelerData> travelers, List<string> beasts)
    {
        if (!TeamDataExists(travelers, beasts))
            return false;
        if (!TravelerCountIsValid(travelers))
            return false;
        if (!BeastCountIsValid(beasts))
            return false;
        if (!TravelerNamesAreUnique(travelers))
            return false;
        if (!BeastNamesAreUnique(beasts))
            return false;
        if (!AllTravelersExist(travelers))
            return false;
        if (!AllBeastsExist(beasts))
            return false;
        if (!AllTravelerSkillsAreValid(travelers))
            return false;

        return true;
    }

    private bool TeamDataExists(List<TravelerData> travelers, List<string> beasts)
    {
        return travelers != null && beasts != null;
    }

    private bool TravelerCountIsValid(List<TravelerData> travelers)
    {
        int travelerCount = travelers.Count;
        return travelerCount >= MinimumTravelers && travelerCount <= MaximumTravelers;
    }

    private bool BeastCountIsValid(List<string> beasts)
    {
        int beastCount = beasts.Count;
        return beastCount >= MinimumBeasts && beastCount <= MaximumBeasts;
    }

    private bool TravelerNamesAreUnique(List<TravelerData> travelers)
    {
        return NamesAreUnique(travelers.Select(traveler => traveler.Name));
    }

    private bool BeastNamesAreUnique(List<string> beasts)
    {
        return NamesAreUnique(beasts);
    }

    private bool NamesAreUnique(IEnumerable<string> names)
    {
        HashSet<string> uniqueNames = new();

        foreach (string name in names)
        {
            if (!uniqueNames.Add(name))
            {
                return false;
            }
        }

        return true;
    }

    private bool AllTravelersExist(List<TravelerData> travelers)
    {
        foreach (TravelerData traveler in travelers)
        {
            if (!_validTravelerNames.Contains(traveler.Name))
            {
                return false;
            }
        }

        return true;
    }

    private bool AllBeastsExist(List<string> beasts)
    {
        foreach (string beastName in beasts)
        {
            if (!_validBeastNames.Contains(beastName))
            {
                return false;
            }
        }

        return true;
    }

    private bool AllTravelerSkillsAreValid(List<TravelerData> travelers)
    {
        foreach (TravelerData traveler in travelers)
        {
            if (!TravelerSkillsAreValid(traveler.ActiveSkills, traveler.PassiveSkills))
            {
                return false;
            }
        }

        return true;
    }

    private bool TravelerSkillsAreValid(List<string> activeSkills, List<string> passiveSkills)
    {
        if (!SkillCountIsValid(activeSkills, MaximumActiveSkills))
            return false;
        if (!SkillCountIsValid(passiveSkills, MaximumPassiveSkills))
            return false;
        if (!SkillsAreUnique(activeSkills))
            return false;
        if (!SkillsAreUnique(passiveSkills))
            return false;
        if (!AllSkillsExist(activeSkills, _validActiveSkillNames))
            return false;

        return AllSkillsExist(passiveSkills, _validPassiveSkillNames);
    }

    private bool SkillCountIsValid(List<string> skills, int maximumSkillCount)
    {
        return skills.Count <= maximumSkillCount;
    }

    private bool SkillsAreUnique(List<string> skills)
    {
        return NamesAreUnique(skills);
    }

    private bool AllSkillsExist(List<string> skills, HashSet<string> validSkillNames)
    {
        foreach (string skill in skills)
        {
            if (!validSkillNames.Contains(skill))
            {
                return false;
            }
        }

        return true;
    }
}