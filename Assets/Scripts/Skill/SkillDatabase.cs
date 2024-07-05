using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    public List<Skill> SkillDataList;   // 진짜 DB
    private Dictionary<SkillID, Skill> SkillMap;    // ID로 객체를 잇기 위한 용

    void Start()
    {
        // ScriptableObject 데이터를 이용하여 매핑
        SkillMap = new Dictionary<SkillID, Skill>();

        foreach (var SkillData in SkillDataList)
        {
            SkillMap[SkillData.ID] = SkillData;
        }
    }
}
