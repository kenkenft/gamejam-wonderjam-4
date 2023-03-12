using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseButtonClick : MonoBehaviour
{
    public int TargetState;
    
    void OnMouseDown()
    {
        Debug.Log("Phase Button clicked! TargetState: " + TargetState);
    }
}
