using UnityEngine;
using System.Collections.Generic;

public class DiceSlotManager : MonoBehaviour
{
    public static DiceSlotManager Instance;
    private List<Transform> diceSlotGroups = new List<Transform>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foreach (Transform group in transform)
        {
            diceSlotGroups.Add(group);
        }
    }

    public List<Transform> GetDiceSlotGroups()
    {
        return diceSlotGroups;
    }
}
