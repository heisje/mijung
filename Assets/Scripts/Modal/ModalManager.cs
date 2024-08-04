using System.Collections.Generic;
using UnityEngine;
public class ModalManager : Singleton<ModalManager>
{
    // 모달을 누르면
    // 1. 주사위, 스킬, 유물변경이 뜬다.
    // 2. 추가/삭제를 누른다. 
    // 3. 추가하고싶은, 삭제하고싶은 을 누른다.

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
            bool afterActive = !modalPanel.activeSelf;
            modalPanel.SetActive(afterActive);
        }
    }
}