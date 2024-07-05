using System;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public enum DiceState
{
    ToBeRolled,      // 돌려야됨
    Keeped,      // 저장됨
    Destroyed    // 파기됨
}

public class Dice : ClickDragAndDrop
{
    private static int nextId = 1; // 식별자를 위한 정적 변수
    public int Id { get; private set; } // 식별자

    public int[] Values; // 주사위의 값들
    public DiceState State; // 주사위 상태
    public int Value; // 주사위 값

    // Awake시 참조
    public DiceVisual DiceVisual; // 시각적인 요소를 가진 객체 참조
    public TextMeshProUGUI UI; // 텍스트 참조

    private void Awake()
    {
        // DiceVisual > Canvas > Text(TMP)
        Transform diceVisualTransform = transform.Find("DiceVisual");
        if (diceVisualTransform != null)
        {
            DiceVisual = diceVisualTransform.GetComponent<DiceVisual>();
            Transform canvasTransform = diceVisualTransform.Find("Canvas");
            if (canvasTransform != null)
            {
                UI = canvasTransform.Find("ValueText").GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogWarning("canvasTransform을 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("diceVisualTransform 찾을 수 없습니다.");
        }
    }

    // 생성자
    public Dice(int numberOfSides)
    {
        Values = Enumerable.Range(1, numberOfSides + 1).ToArray();
        Id = nextId++;
    }

    // 돌리기
    public void Roll()
    {
        int randomIndex = UnityEngine.Random.Range(0, Values.Length);
        Value = Values[randomIndex];
        UI.text = Value.ToString();
    }

    // 주사위 저장
    public void UpdateStat(DiceState toState)
    {
        State = toState;
        UpdateVisual();
    }

    // 주사위 변환
    public void Change(int index, int toValue)
    {
        Values[index] = toValue;
    }

    public override void OnClick()
    {
        if (State == DiceState.Keeped)
        {
            UpdateStat(DiceState.ToBeRolled);
        }
        else if (State == DiceState.ToBeRolled)
        {
            UpdateStat(DiceState.Keeped);
        }
    }

    // 상태 변경 시 DiceVisual 업데이트
    private void UpdateVisual()
    {
        if (State == DiceState.Keeped)
        {
            DiceVisual.SetColor(Color.green);
        }
        if (State == DiceState.ToBeRolled)
        {
            DiceVisual.SetColor(Color.yellow);
        }
    }


    public override void OnDrop(RaycastHit[] hits)
    {
        Vector3 closest = Vector3.zero;
        foreach (RaycastHit hit in hits)
        {
            closest = GetComponentAndFindVector<DiceSlot>(hit, h => h.transform.position, closest);
            closest = GetComponentAndFindVector<DiceRollBoard>(hit, h => h.point, closest);
        }

        if (closest != Vector3.zero)
        {
            Vector3 newPosition = closest;
            newPosition.z = 0;
            transform.position = newPosition;
        }
        else
        {
            transform.position = StartPosition;
        }
    }

}
