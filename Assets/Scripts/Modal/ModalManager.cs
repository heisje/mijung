using UnityEngine;
public class ModalManager : Singleton<ModalManager>
{
    public GameObject modalPanel;

    void Start()
    {
        if (modalPanel != null)
        {
            modalPanel.SetActive(false); // 시작할 때 모달 UI를 비활성화합니다.
        }
    }

    public void ShowModal()
    {
        if (modalPanel != null)
        {
            modalPanel.SetActive(true);
        }
    }

    public void HideModal()
    {
        if (modalPanel != null)
        {
            modalPanel.SetActive(false);
        }
    }

    public void ToggleModal()
    {
        if (modalPanel != null)
        {
            bool isActive = modalPanel.activeSelf;
            modalPanel.SetActive(!isActive);
        }
    }
}