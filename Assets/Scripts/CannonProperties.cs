using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProperties : MonoBehaviour
{
    [SerializeField] private int _cannonMaxCapacity = 6, _cannonUsedCapacity = 0;

    private List<GameObject> loadedCannonBalls = new List<GameObject>{};

    [HideInInspector]public delegate void OnCheckAimingDelegate(bool state);
    [HideInInspector]public static OnCheckAimingDelegate OnCheck;

    public void LoadInToCannon(GameObject cannonBall)
    {
        int cannonBallCapacity = cannonBall.GetComponent<CannonBallDisplay>().CapacitySize;
        if(_cannonUsedCapacity + cannonBallCapacity <= _cannonMaxCapacity)
        {    
            loadedCannonBalls.Add(cannonBall);
            _cannonUsedCapacity += cannonBallCapacity;
        }
        else
            Debug.Log("Capacity would be exceeded! Cannot load!");

        CheckAimingPhaseCriteria();
    }

    public void UnloadFromCannon()
    {
        // Removes most recently loaded cannonball and assoicated capacity usage
        int lastIndex = loadedCannonBalls.Count-1;
        _cannonUsedCapacity =- loadedCannonBalls[lastIndex].GetComponent<CannonBallDisplay>().CapacitySize;
        loadedCannonBalls.RemoveAt(lastIndex);  
    }

    public void CheckAimingPhaseCriteria()
    {
        if(_cannonUsedCapacity > 0)
        {
            Debug.Log("Ammo detected! Enabling Aiming Button");
            // ToDo Set up function that modify UI button state
            OnCheck?.Invoke(true);
        }
        else
        {
            Debug.Log("No Ammo detected! Disabling Aiming Button");
            // ToDo Set up function that modify UI button state
            OnCheck?.Invoke(false);
        }

    }
    
}
