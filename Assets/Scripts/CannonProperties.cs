using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProperties : MonoBehaviour
{
    [SerializeField] private int _cannonMaxCapacity = 6, _cannonUsedCapacity = 0;

    [SerializeField] private List<GameObject> _loadedCannonBalls = new List<GameObject>{}, _selectedTargets = new List<GameObject>{};

    [HideInInspector]public delegate void OnCheckAimingDelegate(int buttonState);
    [HideInInspector]public static OnCheckAimingDelegate OnCheck;

    private void OnEnable()
    {
        CannonManager.OnGameStateChange += CheckGameState;
    }

    private void OnDisable()
    {
        CannonManager.OnGameStateChange -= CheckGameState;
    }

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
            OnCheck?.Invoke(4);
        else
            OnCheck?.Invoke(0);
    }
    
    public bool DesignateTarget(GameObject target)
    {
        if( _selectedTargets.Count < _loadedCannonBalls.Count)
        {
            _selectedTargets.Add(target);
            Debug.Log("Tile designated target! Name: " + target.name);
            CheckFiringPhaseCriteria();
            return true;
        }
        return false;
    }

    public void CheckFiringPhaseCriteria()
    {
        if(_selectedTargets.Count == _loadedCannonBalls.Count)
            OnCheck?.Invoke(5);
        else
            OnCheck?.Invoke(1);
    }

    public bool DelistTarget(int[] targetTile)
    {
        Debug.Log("Tile to be delisted!");
        if(_selectedTargets.Count > 0)
        {
            int[] tempIntArray = new int[2];
            for(int i = 0; i < _selectedTargets.Count ; i++)
            {
                tempIntArray = _selectedTargets[i].GetComponent<TileProperties>().GetTileCoordinates();
                if(tempIntArray[0] == targetTile[0] && tempIntArray[1] == targetTile[1])
                {    
                    Debug.Log(_selectedTargets[i].name + " to be delisted!");
                    _selectedTargets.RemoveAt(i);
                    CheckFiringPhaseCriteria();
                    return true;
                }
            }
        }
        return false;
    }

    void DeselectAllTargets()
    {
        // foreach(GameObject target in _selectedTargets)
        for(int i = _selectedTargets.Count - 1; i >= 0 ; i--)
        {
            _selectedTargets[i].GetComponent<TileProperties>().DeselectAndHideReticle();
            _selectedTargets.RemoveAt(i);
        }
    }

    void CheckGameState(int gameState)
    {
    
        switch(gameState)
        {
            case 3:
            {
                Debug.Log("gameState is 3! Deselecting all targets and emptying cannon");
                DeselectAllTargets();
                break;
            }
            default:
            {
                // Debug.Log("gameState is " + gameState + ". CannonBallLoadingManager doesn't do anything");
                break;
            }
        }
    }   //End of CheckGameState
}
