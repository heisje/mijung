using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

[Serializable]
public class SkillData
{
    public SkillID ID;
    public string Ko;
    public string Description;
    public CombiType Combi;
    public int Unique;
    public int DefaultCooldown;
    public SkillTargetType Target;
    public List<KeyValuePair<FormulaType, string>> FormulaList;
}

public class SkillReaderManager : Singleton<SkillReaderManager>
{
    // 전체 스킬 데이터
    public Dictionary<SkillID, SkillData> SkillDataDict = new Dictionary<SkillID, SkillData>();

    public List<SkillData> SkillDataList = new List<SkillData>();

    private static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    private static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    protected override void Instantiation()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("SkillData - PublicSkills");
        if (csvFile == null)
        {
            Debug.LogError("CSV file not found at path: Resources/SkillData - PublicSkills");
            return;
        }

        var lines = Regex.Split(csvFile.text, LINE_SPLIT_RE);
        if (lines.Length <= 1) return;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            try
            {
                var values = Regex.Split(lines[i], SPLIT_RE);

                if (values.Length == 0 || values[0] != "TRUE") throw new Exception();

                if (!Enum.TryParse(typeof(SkillID), values[1], out var id) || !Enum.TryParse(typeof(CombiType), values[5], out var combi) ||
                   !Enum.TryParse(typeof(SkillTargetType), values[8], out var target))
                {
                    throw new Exception(); // 열거형 파싱 실패 시 다음 루프로 이동
                }

                int StartIndex = 10;
                int lastIndex = StartIndex + Convert.ToInt32(values[9]) * 2;
                List<KeyValuePair<FormulaType, string>> formulaList = new();
                while (StartIndex < lastIndex)
                {
                    if (Enum.TryParse(typeof(FormulaType), values[StartIndex], out var formulaType))
                    {
                        formulaList.Add(new KeyValuePair<FormulaType, string>((FormulaType)formulaType, values[StartIndex + 1]));
                    }
                    else
                    {
                        throw new Exception($"Error parsing FormulaType at index {StartIndex}: {values[StartIndex]}");
                    }
                    StartIndex += 2;
                }

                SkillData skillData = new SkillData
                {
                    ID = (SkillID)id,
                    Ko = values[2],
                    Description = values[3],
                    Combi = (CombiType)combi,
                    Unique = Convert.ToInt32(values[6]),
                    DefaultCooldown = Convert.ToInt32(values[7]),
                    Target = (SkillTargetType)target,
                    FormulaList = formulaList
                };
                SkillDataDict.Add(skillData.ID, skillData);
                SkillDataList.Add(skillData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing line {i}: {ex.Message}");
                continue;
            }
        }
    }

    public SkillData GetSkillData(SkillID id)
    {
        if (SkillDataDict.TryGetValue(id, out SkillData skillData))
        {
            return skillData;
        }
        else
        {
            Debug.LogWarning($"Skill ID {id} not found.");
            return null;
        }
    }
}
