using UnityEngine;

public class SelectButton : ChangeTMP, IClickable
{
    public int Index;
    public SelectButtonGroup Mother;

    public void Initialization(int i, string text, SelectButtonGroup selectButtonGroup)
    {
        Index = i;
        ChangeText(text);
        Mother = selectButtonGroup;
    }

    public void OnClick()
    {
        Mother.SendClickEvent(Index);
    }
}
