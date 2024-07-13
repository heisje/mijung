using System;
using UnityEngine;

public class SkillCard : ClickDragAndDrop
{
    public Skill Skill;
    public SkillID SkillID;
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
            string isPossible = Skill.IsPossible ? " O" : " X";
            UpdateText(Skill.Name + isPossible);
        }
        else
        {
            IsDragAndDropPossible = false;
            string isPossible = Skill.IsPossible ? " O" : " X";
            UpdateText(Skill.Name + isPossible);
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
                Debug.Log(hit.transform.name);
                if (Skill.SkillTarget == SkillTargetType.EnemySingle)
                {
                    Enemy targetEnemy = GetHitComponent<Enemy>(hit);

                    // 사용이 가능한 지점
                    if (targetEnemy != null)
                    {
                        ChangeTMP.ChangeText("사용됨");
                        GameSession.Instance.OnSkillActive(Skill.OnSkill, targetEnemy);
                    }
                }
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