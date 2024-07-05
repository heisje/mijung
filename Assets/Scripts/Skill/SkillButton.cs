using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillButton : MonoBehaviour, IClickable
{
    public TextMeshProUGUI textMeshPro;

    void Awake()
    {
        // 하위 오브젝트에서 TextMeshProUGUI 컴포넌트를 찾습니다.
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component not found in children.");
        }
    }

    public void ChangeText(string newText)
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = newText;
        }
    }

    public void OnClick()
    {

    }
}
