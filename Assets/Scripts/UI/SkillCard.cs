using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillCard : ClickDragAndDrop
{
    public Skill Skill;
    public E_Sk_Id SkillID;
    protected ChangeTMP ChangeTMP;

    public override void OnDragStart()
    {
        if (IsDragAndDropPossible)
        {
            StartPosition = transform.position;
            isDragging = true;
        }
    }

    private void Update()
    {
        if (Skill.IsPossible)
        {
            IsDragAndDropPossible = true;
            string isPossible = $"    {(Skill.IsPossible ? "<color=#0000FF><size=150%>O</size></color>" : "<color=#FF0000>X</color>")}, {(Skill.IsChanged ? "<color=#FF0000>바뀜</color>" : "")}";
            UpdateText(Skill.ID + isPossible + "\n" + Skill.Description);
        }
        else
        {
            IsDragAndDropPossible = false;
            string isPossible = $"    {(Skill.IsPossible ? "<color=#0000FF><size=150%>O</size></color>" : "<color=#FF0000>X</color>")}, {(Skill.IsChanged ? "<color=#FF0000>바뀜</color>" : "")}";
            UpdateText(Skill.ID + isPossible + "\n" + Skill.Description);
        }

        // 오브젝트가 존재하는지 확인하고, Button 컴포넌트를 가져옵니다.
        Button myButton = GetComponentInChildren<Button>();

        if (myButton != null)
        {
            // 현재 버튼의 ColorBlock을 가져옵니다.
            ColorBlock colorBlock = myButton.colors;

            // Normal Color를 원하는 색상으로 변경합니다. 예시로 파란색을 사용했습니다.
            colorBlock.normalColor = Skill.IsGlobalPossible ? Color.green : Color.white;

            // 변경된 ColorBlock을 버튼에 다시 할당합니다.
            myButton.colors = colorBlock;
        }
        else
        {
            Debug.LogError("Button 컴포넌트를 찾을 수 없습니다.");
        }
    }

    // IsDragAndDropPossible이 되어있는 상태어야만, DiceDTO가 제대로 참조됨.
    public override void OnDrop(RaycastHit[] hits)
    {
        if (IsDragAndDropPossible)
        {
            Vector3 closest = Vector3.zero;
            foreach (RaycastHit hit in hits)
            {
                // if (Skill.SkillTarget == SkillTargetType.EnemySingle)
                // {
                Enemy targetEnemy = GetHitComponent<Enemy>(hit);

                // 사용이 가능한 지점
                if (targetEnemy != null)
                {
                    ChangeTMP.ChangeText("사용됨");
                    var playerAction = new PlayerAction(Skill.OnSkill, GameSession.Ins.DiceDTO, new Sk_Context(targetEnemy));
                    GameSession.Ins.OnSkillSave(playerAction);
                }
                // }
            }
        }
        transform.position = StartPosition;
        isDragging = false;
    }

    public void UpdateText(string text)
    {
        if (ChangeTMP == null)
        {
            ChangeTMP = transform.GetComponentInChildren<ChangeTMP>();
        }
        ChangeTMP.ChangeText(text);
    }
}