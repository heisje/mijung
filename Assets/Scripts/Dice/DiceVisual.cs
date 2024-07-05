using System;
using System.Linq;
using UnityEngine;
using TMPro;

public class DiceVisual : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }


    // 색상을 변경하는 메서드
    public void SetColor(Color color)
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color = color;
        }
    }
}