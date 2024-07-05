using UnityEngine;
using UnityEngine.InputSystem;

public class Clicker : MonoBehaviour
{
    Camera m_Camera;
    ClickDragAndDrop draggedObject;
    IClickable clickableObject;
    bool isDragging = false;
    float clickStartTime;
    const float clickThreshold = 0.2f; // 클릭과 드래그를 구분하는 시간 (초)

    const float fixedZPosition = -30f; // 고정된 z축 위치

    float maxRayCastDistance = 1000f; // RayCast최대 거리 설정
    void Awake()
    {
        m_Camera = Camera.main;
    }

    void Update()
    {
        Mouse mouse = Mouse.current;

        // 객체 선택
        if (mouse.leftButton.wasPressedThisFrame)
        {
            OnMouseDown();
        }

        // 눌렀을 때 시간을 재며 분기
        if (mouse.leftButton.isPressed)
        {
            if (draggedObject != null)
            {
                OnDrag();
            }
            else if (Time.time - clickStartTime > clickThreshold)
            {
                // 클릭 시간이 기준을 넘었을 때 드래그 시작
                if (clickableObject != null)
                {
                    ClickDragAndDrop dragAndDropAble = clickableObject as ClickDragAndDrop;
                    if (dragAndDropAble != null && dragAndDropAble.IsDragAndDropPossible)
                    {
                        draggedObject = dragAndDropAble;
                        isDragging = true;
                        draggedObject.OnDragStart(); // 드래그 시작 시 호출
                    }
                }
            }
        }

        if (isDragging && !draggedObject.IsDragAndDropPossible)
        {
            OnMouseUp();
        }

        if (mouse.leftButton.wasReleasedThisFrame)
        {
            OnMouseUp();
        }
    }

    void OnMouseDown()
    {
        clickStartTime = Time.time;
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = m_Camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            clickableObject = hit.collider.GetComponentInParent<IClickable>();
        }
    }

    void OnDrag()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = Mathf.Abs(m_Camera.transform.position.z - fixedZPosition); // z축 고정
        Vector3 worldPosition = m_Camera.ScreenToWorldPoint(mousePosition);
        draggedObject.OnDrag(worldPosition);
    }

    void OnMouseUp()
    {
        if (!isDragging && clickableObject != null)     // 오직 선택
        {
            clickableObject.OnClick();
        }
        else if (isDragging && draggedObject != null)   // 드래그 중
        {
            var hits = FindRayCastAll();
            draggedObject.OnDrop(hits);
        }

        // 초기화
        draggedObject = null;
        clickableObject = null;
        isDragging = false;
    }

    RaycastHit[] FindRayCastAll()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, maxRayCastDistance);
        return hits;
    }
}
