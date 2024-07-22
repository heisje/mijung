using System.Collections.Generic;
using UnityEngine;

public class SelectButtonGroup : MonoBehaviour
{
    SelectManager Mother;
    List<SelectButton> SelectButtons = new List<SelectButton>();
    public void Initialize(KeyValuePair<int, string>[] data, GameObject prefab, SelectManager mother)
    {
        Mother = mother;
        for (var i = 0; i < data.Length; i++)
        {
            GameObject SelectButtonObject = Instantiate(prefab, transform);

            // Vector3 newPosition = SelectButtonObject.transform.position;
            // newPosition.x += 400 * i; // x 포지션 수정
            // SelectButtonObject.transform.localPosition = newPosition;

            SelectButton SelectButton = SelectButtonObject.AddComponent<SelectButton>();
            SelectButton.Initialization(data[i].Key, data[i].Value, this);

            SelectButtons.Add(SelectButton);
        }
    }
    public void SendClickEvent(int i)
    {
        Mother.OnClickButton(i);

        foreach (SelectButton garbage in SelectButtons)
        {
            Destroy(garbage.gameObject);
        }
        Destroy(this.gameObject);
    }
}