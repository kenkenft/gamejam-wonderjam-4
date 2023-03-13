using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseButtonClick : MonoBehaviour
{
    public int TargetState;

    [HideInInspector]public delegate void OnButtonClick(int value);
    [HideInInspector]public static OnButtonClick SetGameState;
    
    void OnMouseDown()
    {
        SetGameState?.Invoke(TargetState);
        Debug.Log("Button clicked: " + gameObject.name);
    }
}
