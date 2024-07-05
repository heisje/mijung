using UnityEngine;
using System.Collections.Generic;

public class DiceSlots : MonoBehaviour
{
    private List<Transform> diceSlots = new List<Transform>();

    private void Awake()
    {

        foreach (Transform slot in transform)
        {
            diceSlots.Add(slot);
        }
    }

    public List<Transform> GetDiceSlots()
    {
        return diceSlots;
    }
}
