using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CarouselController : MonoBehaviour
{
    public RectTransform contentPanel; // 캐러셀 콘텐츠를 담는 패널
    public Button prevButton; // 이전 버튼
    public Button nextButton; // 다음 버튼
    public float transitionDuration = 0.5f; // 슬라이드 애니메이션 시간
    public int itemsPerPage = 1; // 한 페이지에 보여줄 아이템 수

    private int currentIndex = 0;
    private int totalItems;

    private void Start()
    {
        totalItems = contentPanel.childCount;
        prevButton.onClick.AddListener(ShowPrevious);
        nextButton.onClick.AddListener(ShowNext);
        UpdateButtons();
    }

    private void ShowPrevious()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            StartCoroutine(SlideToIndex(currentIndex));
        }
    }

    private void ShowNext()
    {
        if (currentIndex < totalItems - itemsPerPage)
        {
            currentIndex++;
            StartCoroutine(SlideToIndex(currentIndex));
        }
    }

    private IEnumerator SlideToIndex(int index)
    {
        Vector2 startPosition = contentPanel.anchoredPosition;
        Vector2 endPosition = new Vector2(-index * contentPanel.rect.width / itemsPerPage, startPosition.y);
        float elapsedTime = 0;

        while (elapsedTime < transitionDuration)
        {
            contentPanel.anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        contentPanel.anchoredPosition = endPosition;
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        prevButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < totalItems - itemsPerPage;
    }
}
