using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProperties : MonoBehaviour
{
    [SerializeField] private int _cannonMaxCapacity = 6, _cannonUsedCapacity = 0;

    [SerializeField] private List<GameObject> _loadedCannonBalls = new List<GameObject>{}, _selectedTargets = new List<GameObject>{};

    [HideInInspector]public delegate void OnCheckAimingDelegate(bool state);
    [HideInInspector]public static OnCheckAimingDelegate OnCheck;

    public void LoadInToCannon(GameObject cannonBall)
    {
        int cannonBallCapacity = cannonBall.GetComponent<CannonBallDisplay>().CapacitySize;
        if(_cannonUsedCapacity + cannonBallCapacity <= _cannonMaxCapacity)
        {    
            _loadedCannonBalls.Add(cannonBall);
            _cannonUsedCapacity += cannonBallCapacity;
        }
        else
            Debug.Log("Capacity would be exceeded! Cannot load!");

        CheckAimingPhaseCriteria();
    }

    public void UnloadFromCannon()
    {
        // Removes most recently loaded cannonball and assoicated capacity usage
        int lastIndex = _loadedCannonBalls.Count-1;
        _cannonUsedCapacity =- _loadedCannonBalls[lastIndex].GetComponent<CannonBallDisplay>().CapacitySize;
        _loadedCannonBalls.RemoveAt(lastIndex);  
    }

    public void CheckAimingPhaseCriteria()
    {
        if(_cannonUsedCapacity > 0)
            OnCheck?.Invoke(true);
        else
            OnCheck?.Invoke(false);
    }
    
    public void DesignateTarget(GameObject target)
    {
        _selectedTargets.Add(target);
        Debug.Log("Tile Added! Name: " + target.name);
    }

    public void DelistTarget(int[] tileCoordinates)
    {

    }
}
