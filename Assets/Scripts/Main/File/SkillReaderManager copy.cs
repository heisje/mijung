using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

/// <summary>
/// 스킬 컬럼
/// </summary>
public enum E_S
{
    ID = 1,
    Name,
    Description,
    Group,
    Combi,
    Unique,
    Cooldown,
    MouseTarget,
    EffectTarget,
    Effect,
    Formula
}

[Serializable]
public class SkillData2
{
    public ESkillID ID;
    public string Ko;
    public string Description;
    public ECombi Combi;
    public int Unique;
    public int DefaultCooldown;
    public SkillTargetType Target;
    public List<Formula<EFormula>> FormulaList;
    public EChanger Changer;
    public string ChangerValue;
    public List<Formula<EFormula>> ChangerFormulaList;
    public CharacterType Character;
}

public class SkillReaderManager2 : Singleton<SkillReaderManager>
{
    public Dictionary<ESkillID, SkillData> SkillDataDict = new();
    public List<SkillData> SkillDataList = new();

    private static readonly string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    private static readonly string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    private readonly string[] fileNames = { "SkillData - PublicSkills", "SkillData - SwordsManSkills" };

    protected override void Instantiation()
    {
        foreach (var fileName in fileNames)
        {
            CharacterType character = fileName.Contains("PublicSkills") ? CharacterType.Public : CharacterType.Swordsman;
            CSVToSkill(fileName, character);
        }
    }

    public void CSVToSkill(string fileName, CharacterType character)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);
        if (csvFile == null)
        {
            Debug.LogError($"CSV file not found at path: Resources/{fileName}");
            return;
        }

        var lines = Regex.Split(csvFile.text, LINE_SPLIT_RE);
        if (lines.Length <= 1) return;

        var header = Regex.Split(lines[0], SPLIT_RE);
        string headerText = string.Join(", ", header);
        Debug.Log("Header: " + headerText);

        for (var i = 1; i < lines.Length; i++)
        {
            try
            {
                var values = Regex.Split(lines[i], SPLIT_RE);

                if (values.Length == 0 || values[0] != "TRUE") continue;


                SkillData skillData = new()
                {
                    ID = ParseEnum<ESkillID>(values[1], $"SkillID: {values[1]}"),
                    Ko = values[2],
                    Description = values[3],
                    Combi = ParseEnum<ECombi>(values[5], $"CombiType: {values[5]}"),
                    Unique = Convert.ToInt32(values[6]),
                    DefaultCooldown = Convert.ToInt32(values[7]),
                    Target = ParseEnum<SkillTargetType>(values[8], $"SkillTargetType: {values[8]}"),
                    FormulaList = ParseFormulaList(9, values),
                    Changer = ParseEnum<EChanger>(values[15], $"ChangerType: {values[15]}"),
                    ChangerValue = values[16],
                    ChangerFormulaList = ParseFormulaList(17, values),
                    Character = character
                };

                SkillDataDict.Add(skillData.ID, skillData);
                SkillDataList.Add(skillData);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"CSV Parse Error {fileName} Line {i} : {ex.Message}\n{ex.StackTrace}");
                continue;
            }
        }
    }

    private T ParseEnum<T>(string value, string errorMessage) where T : Enum
    {
        if (Enum.TryParse(typeof(T), value, out var result))
        {
            return (T)result;
        }
        throw new Exception($"Failed to parse {errorMessage}");
    }

    private List<Formula<EFormula>> ParseFormulaList(int startIndex, string[] values)
    {
        var formulaList = new List<Formula<EFormula>>();
        for (int j = 0; j < 3; j++)
        {
            var typeIndex = startIndex + j * 2;
            var valueIndex = typeIndex + 1;

            if (Enum.TryParse(typeof(EFormula), values[typeIndex], out var formulaType) &&
                (EFormula)formulaType != EFormula.None)
            {
                formulaList.Add(new Formula<EFormula>((EFormula)formulaType, values[valueIndex]));
            }
        }
        return formulaList;
    }

    public SkillData GetSkillData(ESkillID id)
    {
        if (SkillDataDict.TryGetValue(id, out SkillData skillData))
        {
            return skillData;
        }
        Debug.LogWarning($"Skill ID {id} not found.");
        return null;
    }
}
