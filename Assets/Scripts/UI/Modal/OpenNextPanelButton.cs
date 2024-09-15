using UnityEngine;

public class OpenNextPanelButton : MonoBehaviour, IClickable
{
    public GameObject NextPanel;
    public void OnClick()
    {
        NextPanel.SetActive(true);
    }
}