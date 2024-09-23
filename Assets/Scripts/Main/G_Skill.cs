using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 로딩시 저장되는 게임 전체 데이터 모음
// TODO: 아무래도 최적화를 해야겠지?
public class G_Skill : Singleton<G_Skill>
{
    [SerializeField]
    public List<SkillData> SkillsInspector = new();

    public Dictionary<E_Sk_Id, SkillData> Skills = new();

    public SkillData GetSkillData(E_Sk_Id id)
    {
        if (Skills.TryGetValue(id, out SkillData skillData))
        {
            return skillData;
        }
        throw new FileLoadException($"No GetSkillData Target:{id}");
    }

    public Skill GenerateSkillIns(E_Sk_Id id)
    {
        if (Skills.TryGetValue(id, out SkillData skillData))
        {
            return CreateInstance(skillData.ID.ToString(), skillData);
        }
        throw new FileLoadException($"No New SkillData Target:{id}");
    }



    public static Skill CreateInstance(string className, SkillData skillData)
    {
        // 현재 어셈블리에서 클래스 타입 찾기
        Type type = Type.GetType(className) ?? throw new ArgumentException($"Class '{className}' not found.");

        // 해당 타입이 Skill을 상속받는지 확인
        if (!typeof(Skill).IsAssignableFrom(type))
        {
            throw new InvalidOperationException($"Class '{className}' does not inherit from Skill.");
        }

        // 동적으로 생성자 호출하여 인스턴스 생성
        return (Skill)Activator.CreateInstance(type, skillData);
    }
}