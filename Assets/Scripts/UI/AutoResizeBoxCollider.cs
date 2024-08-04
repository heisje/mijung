using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AutoResizeBoxCollider : MonoBehaviour
{
    private BoxCollider boxCollider;
    private RectTransform rectTransform;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (boxCollider != null && rectTransform != null)
        {
            // RectTransform의 크기에 따라 BoxCollider 크기 조정
            Vector3 newSize = new Vector3(rectTransform.rect.width, rectTransform.rect.height, 1);
            boxCollider.size = newSize;
        }
    }
}
