using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;

[Serializable]
public class SkillData
{
    public E_Sk_Id ID; // 1
    public string Ko;   // 2
    public string En;   // 3
    public string Ko_Description;   // 4
    public string En_Description;   // 5
    public ECharacter Group;        // 6
    public ECombi Combi;            // 7
    public int Unique;              // 8
    public int Cooldown;            // 9
    public E_Sk_Focus MouseFocus;   // 10
    public List<string> Formulas;   // 11
}

/// <summary>
/// 스킬만 읽어내고 GData에 데이터를 넘김
/// </summary>
public class SkillReaderManager : Singleton<SkillReaderManager>
{
    private static readonly string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    private static readonly string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    protected override void Init()
    {
        TextAsset CSV = Resources.Load<TextAsset>("Data - Skills");
        var rows = Regex.Split(CSV.text, LINE_SPLIT_RE);
        if (rows.Length <= 1) return;
        CSVToSkill(rows);
    }
    public void CSVToSkill(string[] rows)
    {
        for (var i = 1; i < rows.Length; i++)
        {
            try
            {
                //랜더	ID	한국이름	영어이름	설명	영어설명	6_그룹	조합법	희귀	쿨다운	마우스대상	11_매개변수1	2	3	4
                //0    1   2         3        4      5         6      7      8        9       10   11          
                var values = Regex.Split(rows[i], SPLIT_RE);

                if (values.Length == 0 || values[0] != "TRUE") continue;

                // 각 문자열의 앞뒤 공백을 제거
                for (int j = 0; j < values.Length; j++)
                {
                    values[j] = values[j].Trim();
                }

                SkillData skillData = new()
                {
                    ID = ParseEnum<E_Sk_Id>(values, 1),
                    Ko = values[2],
                    En = values[3],
                    Ko_Description = values[4],
                    En_Description = values[5],
                    Group = ParseEnum<ECharacter>(values, 6),
                    Combi = ParseEnum<ECombi>(values, 7),
                    Unique = Convert.ToInt32(values[8]),
                    Cooldown = Convert.ToInt32(values[9]),
                    MouseFocus = ParseEnum<E_Sk_Focus>(values, 10),
                    Formulas = ParseFormulaList(values, 11),
                };

                G_Skill.Ins.Skills.Add(skillData.ID, skillData);
                G_Skill.Ins.SkillsInspector.Add(skillData);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Skills CSV Parse Error Line {i} : {ex.Message}\n{ex.StackTrace}");
                continue;
            }
        }
    }

    private T ParseEnum<T>(string[] values, int i) where T : Enum
    {
        if (Enum.TryParse(typeof(T), values[i], out var result))
        {
            return (T)result;
        }
        throw new Exception($"Failed to parse {i} Column");
    }

    private List<string> ParseFormulaList(string[] values, int startIndex)
    {
        var result = new List<string>();
        // ! 주의
        while (startIndex < values.Length && !string.IsNullOrWhiteSpace(values[startIndex]))
        {
            result.Add(values[startIndex]);
            startIndex++;
        }

        return result;
    }
}
