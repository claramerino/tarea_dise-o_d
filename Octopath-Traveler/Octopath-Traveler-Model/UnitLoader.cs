using System.Text.Json;

namespace Octopath_Traveler_Model;

public static class UnitLoader
{
    public static List<string> LoadNamesFromJson(string jsonPath)
    {
        JsonElement unitsData = ReadUnitsData(jsonPath);

        return unitsData
            .EnumerateArray()
            .Select(GetUnitName)
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .ToList();
    }

    public static Traveler LoadTravelerFromJson(
        string travelerName,
        string charactersJsonPath,
        List<string> activeSkills,
        List<string> passiveSkills)
    {
        JsonElement travelerData = FindUnitDataByName(charactersJsonPath, travelerName);
        Stats stats = CreateTravelerStats(travelerData);
        List<string> weapons = ExtractWeapons(travelerData);

        return new Traveler(travelerName, stats, weapons, activeSkills, passiveSkills);
    }

    public static Beast LoadBeastFromJson(string beastName, string enemiesJsonPath)
    {
        JsonElement beastData = FindUnitDataByName(enemiesJsonPath, beastName);
        JsonElement statsData = beastData.GetProperty("Stats");
        Stats stats = CreateStats(statsData, 0);
        string skillName = beastData.GetProperty("Skill").GetString() ?? "";
        int shields = beastData.GetProperty("Shields").GetInt32();
        List<string> weaknesses = ExtractWeaknesses(beastData);

        return new Beast(beastName, stats, skillName, shields, weaknesses);
    }

    private static List<string> ExtractWeaknesses(JsonElement beastData)
    {
        if (!beastData.TryGetProperty("Weaknesses", out JsonElement weaknessesData))
        {
            return new List<string>();
        }

        return weaknessesData
            .EnumerateArray()
            .Select(w => w.GetString() ?? "")
            .Where(w => !string.IsNullOrWhiteSpace(w))
            .ToList();
    }

    private static JsonElement FindUnitDataByName(string jsonPath, string unitName)
    {
        JsonElement unitsData = ReadUnitsData(jsonPath);

        return unitsData
            .EnumerateArray()
            .First(unitData => GetUnitName(unitData) == unitName);
    }

    private static Stats CreateStats(JsonElement statsData, int maxSp)
    {
        return new Stats(
            maxHp: statsData.GetProperty("HP").GetInt32(),
            maxSp: maxSp,
            physicalAttack: statsData.GetProperty("PhysAtk").GetInt32(),
            physicalDefense: statsData.GetProperty("PhysDef").GetInt32(),
            elementalAttack: statsData.GetProperty("ElemAtk").GetInt32(),
            elementalDefense: statsData.GetProperty("ElemDef").GetInt32(),
            speed: statsData.GetProperty("Speed").GetInt32()
        );
    }

    private static JsonElement ReadUnitsData(string jsonPath)
    {
        string json = File.ReadAllText(jsonPath);
        return JsonSerializer.Deserialize<JsonElement>(json);
    }

    private static string GetUnitName(JsonElement unitData)
    {
        return unitData.GetProperty("Name").GetString() ?? "";
    }

    private static Stats CreateTravelerStats(JsonElement travelerData)
    {
        JsonElement statsData = travelerData.GetProperty("Stats");
        int maxSp = statsData.GetProperty("SP").GetInt32();

        return CreateStats(statsData, maxSp);
    }

    private static List<string> ExtractWeapons(JsonElement travelerData)
    {
        return travelerData
            .GetProperty("Weapons")
            .EnumerateArray()
            .Select(GetWeaponName)
            .Where(weaponName => !string.IsNullOrWhiteSpace(weaponName))
            .ToList();
    }

    private static string GetWeaponName(JsonElement weaponData)
    {
        return weaponData.GetString() ?? "";
    }
}