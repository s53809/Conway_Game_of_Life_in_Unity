using System;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] private SpriteRenderer colorOnOff;
    public Boolean CellState { get; private set; }

    public void SetCell(Boolean isOn)
    {
        CellState = isOn;
        if (isOn) colorOnOff.color = Color.white;
        else colorOnOff.color = Color.black;
    }
}
