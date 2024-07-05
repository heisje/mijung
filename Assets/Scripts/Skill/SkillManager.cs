using System.Collections.Generic;
using UnityEngine;
public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public SkillCard SkillCardPrefab; // SkillButton 프리팹을 드래그하여 할당 (static)

    public void UpdateSkills(DiceCalculateDto DiceDTO, Player player)
    {
        foreach (var skill in player.HaveSkillList)
        {
            skill.IsPossible = false;
            if (skill.Cooldown == 0 && skill.OnCheck(DiceDTO))
            {
                skill.IsPossible = true;
                // SkillCard도 사용가능하게 바꾸는 것은 SkillCard Update에 지정되어 있음
            };
            skill.UpdateDescription(DiceDTO, player);
        }
    }

    public void ChangeIsPossibleSkill(Player player, bool state)
    {
        foreach (var skill in player.HaveSkillList)
        {
            skill.IsPossible = state;
        }
    }

    public void CreatePlayerSkillCard(Player player)
    {
        Vector3 startPosition = Vector3.zero; // CalculateRightCenterPosition(); // 시작 위치를 계산합니다.

        foreach (var skill in player.HaveSkillList)
        {
            // SkillButton 프리팹을 인스턴스화
            SkillCard skillCardInstance = Instantiate(SkillCardPrefab, transform);
            skillCardInstance.Skill = skill;
            string isPossible = skill.IsPossible ? " O" : " X";
            skillCardInstance.UpdateText(skill.Name + isPossible);

            // 인스턴스의 속성을 설정
            skillCardInstance.transform.localPosition = startPosition;

            // 위치 업데이트
            startPosition.y -= 30; // 로컬 좌표 기준으로 y축 위치를 변경
        }
    }

    private static Vector3 CalculateRightCenterPosition()
    {
        // 메인 카메라의 오른쪽 중앙 위치를 계산합니다.
        Camera mainCamera = Camera.main;
        Vector3 rightCenterViewport = new Vector3(1, 0.5f, mainCamera.nearClipPlane);
        Vector3 rightCenterWorldPosition = mainCamera.ViewportToWorldPoint(rightCenterViewport);

        // 스크린 스페이스로 변환하여 UI 위치를 설정합니다.
        Vector3 rightCenterScreenPosition = mainCamera.WorldToScreenPoint(rightCenterWorldPosition);
        rightCenterScreenPosition.z = 0; // UI에서는 z축이 필요 없습니다.

        return rightCenterScreenPosition;
    }
}