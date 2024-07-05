using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour, IClickable
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        GameSession.Instance.OnSkillActive(OnSkipSkill, GameSession.Instance.Player);
    }

    public bool OnSkipSkill<T>(DiceCalculateDto _, T __) where T : ICharacter
    {
        return true;
    }

}
