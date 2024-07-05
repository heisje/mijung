using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

[Serializable]
public class SkillData
{
    public SkillID ID;
    public string Name;
    public string DamageFormula;
    public SkillTargetType SkillTargetType;
    public int DefaultCooldown;
}

public class SkillReaderManager : MonoBehaviour
{
    public static SkillReaderManager Instance;

    private Dictionary<SkillID, SkillData> SkillDataDict;

    private static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    private static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSkillData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadSkillData()
    {
        SkillDataDict = new Dictionary<SkillID, SkillData>();
        TextAsset csvFile = Resources.Load<TextAsset>("skills");
        if (csvFile == null)
        {
            Debug.LogError("CSV file not found at path: Resources/skills");
            return;
        }

        var lines = Regex.Split(csvFile.text, LINE_SPLIT_RE);
        if (lines.Length <= 1) return;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            int idValue = int.Parse(values[0]);
            SkillID id = (SkillID)Enum.ToObject(typeof(SkillID), idValue);
            string name = values[1];
            string damageFormula = values[2];
            string skillTargetTypeValue = values[3];
            SkillTargetType skillTargetType = (SkillTargetType)Enum.Parse(typeof(SkillTargetType), skillTargetTypeValue);
            int defaultCooldown = int.Parse(values[4]);

            SkillData skillData = new SkillData
            {
                ID = id,
                Name = name,
                DamageFormula = damageFormula,
                SkillTargetType = skillTargetType,
                DefaultCooldown = defaultCooldown
            };

            SkillDataDict.Add(id, skillData);
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
