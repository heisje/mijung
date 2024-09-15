using UnityEngine;

class ModalCloseButton : MonoBehaviour, IClickable
{
    public GameObject DefaultPanel;

    public void OnClick()
    {
        HideModal();
    }

    public void HideModal()
    {
        if (DefaultPanel != null)
        {
            DefaultPanel.SetActive(false);
        }
    }
}