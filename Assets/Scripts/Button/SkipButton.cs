using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour, IClickable
{
    public void OnClick()
    {
        GameSession.Instance.OnPlayerInput(PlayerInputType.ClickSkip);
    }

}
