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
    public List<Formula<FormulaType>> FormulaList;
    public ChangerType Changer;
    public string ChangerValue;
    public List<Formula<FormulaType>> ChangerFormulaList;
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
                   !Enum.TryParse(typeof(SkillTargetType), values[8], out var target) || !Enum.TryParse(typeof(ChangerType), values[15], out var changer))
                {
                    throw new Exception(); // 열거형 파싱 실패 시 다음 루프로 이동
                }

                // 포뮬라 파싱
                List<Formula<FormulaType>> formulaList = FormulaPhaser(9, values);


                // Changer 포뮬라 파싱
                List<Formula<FormulaType>> changerFormulaList = FormulaPhaser(12, values);

                SkillData skillData = new SkillData
                {
                    ID = (SkillID)id,
                    Ko = values[2],
                    Description = values[3],
                    Combi = (CombiType)combi,
                    Unique = Convert.ToInt32(values[6]),
                    DefaultCooldown = Convert.ToInt32(values[7]),
                    Target = (SkillTargetType)target,
                    FormulaList = formulaList,
                    Changer = (ChangerType)changer,
                    ChangerValue = values[16],
                    ChangerFormulaList = changerFormulaList
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

    public List<Formula<FormulaType>> FormulaPhaser(int startIndex, string[] values)
    {
        List<Formula<FormulaType>> formulaList = new();
        for (int j = 0; j < 3; j++)
        {
            if (Enum.TryParse(typeof(FormulaType), values[startIndex + j * 2], out var formulaType))
            {
                if ((FormulaType)formulaType == FormulaType.None) continue;
                formulaList.Add(new Formula<FormulaType>((FormulaType)formulaType, values[startIndex + j * 2 + 1]));
            }
            else
            {
                throw new Exception($"Error parsing FormulaType at index {startIndex + j * 2}: {values[startIndex + j * 2]}");
            }
        }
        return formulaList;
    }
}
