using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour, IClickable
{
    public void OnClick()
    {
        GameSession.Instance.OnSkillActive(OnSkipSkill, GameSession.Instance.Player);
    }

    public bool OnSkipSkill<T>(DiceCalculateDto _, T __, Player ___, List<Enemy> ____) where T : Character
    {
        return true;
    }

}
