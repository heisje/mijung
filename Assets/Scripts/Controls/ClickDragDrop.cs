using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClickDragAndDrop : MonoBehaviour, IClickable
{
    public Vector3 StartPosition;
    protected bool isDragging; // 드래그 중인지 여부
    public bool IsDragAndDropPossible { get; set; } = true;

    public virtual void OnClick()
    {
        Debug.Log($"{transform.name}클릭됨");
    }

    // !!!!!!! 드래그 중에는 isDragging을 추가해야함 !!!!!!!
    public virtual void OnDragStart()
    {
        if (!IsDragAndDropPossible) return;
        StartPosition = transform.position;
        isDragging = true;
    }

    // !!!!!!! 드래그 중 드래그를 못할 시 다시 되돌아가는 로직 !!!!!!!
    public virtual void SetIsDragAndDropPossible(bool ChangeValue)
    {
        if (isDragging)
        {
            isDragging = false;
            transform.position = StartPosition;
        }
        IsDragAndDropPossible = ChangeValue;
    }


    public virtual void OnDrag(Vector3 position)
    {
        if (!IsDragAndDropPossible) return;

        Vector3 newPosition = position;
        newPosition.z = -30; // z축 고정
        transform.position = newPosition;
    }

    // !!!!!!!!!!!!!!isDragging여부를 바꿔줘야함!!!!!!!!
    public virtual void OnDrop(RaycastHit[] hits)
    {
        isDragging = false;
    }

    // 람다를 쓰기 위함
    public delegate Vector3 PositionSelector(RaycastHit hit);

    // 해당하는 컴포넌트를 찾고, 위치를 반환
    public Vector3 GetComponentAndFindVector<T1>(RaycastHit hit, PositionSelector selectPosition, Vector3 closest) where T1 : Component
    {
        // 위에서 다른 객체를 찾았으면 바로 리턴
        if (closest != Vector3.zero)
        {
            return closest;
        }

        T1 slotComponent = hit.transform.GetComponent<T1>();

        if (slotComponent != null)
        {
            closest = selectPosition(hit);
        }
        return closest;
    }

    // 레이로 해당 컴포넌트만 찾으면 반환, 상위 컴포넌트 null처리 필수!!!!!!!!!!!!!
    public T1 GetHitComponent<T1>(RaycastHit hit) where T1 : Component
    {
        T1 slotComponent = hit.transform.GetComponent<T1>();

        return slotComponent;
    }
}