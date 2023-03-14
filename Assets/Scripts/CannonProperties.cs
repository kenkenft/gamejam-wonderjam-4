using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProperties : MonoBehaviour
{
    [SerializeField] private int _cannonMaxCapacity = 6, _cannonUsedCapacity = 0;

    [SerializeField] private List<GameObject> _loadedCannonBalls = new List<GameObject>{}, _selectedTargets = new List<GameObject>{};

    [SerializeField] private GameObject _loadedCannonBallArea;
    [HideInInspector]public delegate void OnCheckAimingDelegate(int buttonState);
    [HideInInspector]public static OnCheckAimingDelegate OnCheck;

    [HideInInspector]public delegate void OnCheckHit(int[] targetData);
    [HideInInspector]public static OnCheckHit CheckHitAnything;

    [HideInInspector]public delegate void PassAlongStuff(int[] tileCoords, bool isHighlight, int blastRadius);
    [HideInInspector]public static PassAlongStuff PassToEnemyTileManager;


    private void OnEnable()
    {
        CannonManager.OnGameStateChange += CheckGameState;
        CannonBallLoadingManager.UnloadCannon += EmptyCannon;
        TileProperties.PassAlongForHighlighting += ReceiveForHighlight;
    }

    private void OnDisable()
    {
        CannonManager.OnGameStateChange -= CheckGameState;
        CannonBallLoadingManager.UnloadCannon -= EmptyCannon;
        TileProperties.PassAlongForHighlighting -= ReceiveForHighlight;
    }

    public void LoadInToCannon(GameObject cannonBallZone)
    {
        int cannonBallCapacity = cannonBallZone.GetComponentInChildren<CannonBallDisplay>().CapacitySize;
        if(_cannonUsedCapacity + cannonBallCapacity <= _cannonMaxCapacity)
        {    
            _loadedCannonBalls.Add(cannonBallZone);
            cannonBallZone.GetComponentInChildren<CannonBallDisplay>().gameObject.transform.position = _loadedCannonBallArea.transform.position;
            cannonBallZone.GetComponent<TileProperties>().SetIsOccupied(false);
            // cannonBallZone.GetComponentInChildren<CannonBallDisplay>().gameObject.transform.parent = _loadedCannonBallArea.transform;
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
        _cannonUsedCapacity =- _loadedCannonBalls[lastIndex].GetComponentInChildren<CannonBallDisplay>().CapacitySize;
        _loadedCannonBalls.RemoveAt(lastIndex);  
    }

    public void EmptyCannon(bool isUnloading)
    {
        //Does something to unload cannonballs?
        TileProperties tileProperties;
        if(isUnloading)
        {
            for(int i = 0; i< _loadedCannonBalls.Count; i++)
            {
                tileProperties = _loadedCannonBalls[i].GetComponent<TileProperties>();
                tileProperties.GetOccupant().transform.position = _loadedCannonBalls[i].transform.position;
                tileProperties.SetIsOccupied(true);
            }
        }

        _loadedCannonBalls.Clear();
        _cannonUsedCapacity=0;

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
        if(_selectedTargets.Count == _loadedCannonBalls.Count && _loadedCannonBalls.Count !=0)
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

    void FireAtTargets()
    {
        for(int i = 0; i < _loadedCannonBalls.Count; i++)
            CheckCaughtInBlastRadius(_selectedTargets[i], _loadedCannonBalls[i]);
        // Debug.Log("Salvo complete!");
    }

    void CheckCaughtInBlastRadius(GameObject targetTile, GameObject cannonBall)
    {
        int[] targetData = new int[] 
                                    {
                                        targetTile.GetComponent<TileProperties>().GetTileCoordinates()[0], //Tile Column/x coordinate
                                        targetTile.GetComponent<TileProperties>().GetTileCoordinates()[1], //Tile Row/y coordinate
                                        cannonBall.GetComponentInChildren<CannonBallDisplay>().Damage,
                                        cannonBall.GetComponentInChildren<CannonBallDisplay>().AreaOfEffect
                                    };

        CheckHitAnything?.Invoke(targetData);

    }

    void ReceiveForHighlight(int[] tileCoords, bool isHighlight)
    {
        if(_loadedCannonBalls.Count == _selectedTargets.Count && !isHighlight)
            PassToEnemyTileManager?.Invoke(tileCoords, isHighlight,_loadedCannonBalls[_selectedTargets.Count-1].GetComponentInChildren<CannonBallDisplay>().AreaOfEffect);
        else if(_loadedCannonBalls.Count > _selectedTargets.Count)
            PassToEnemyTileManager?.Invoke(tileCoords, isHighlight,_loadedCannonBalls[_selectedTargets.Count].GetComponentInChildren<CannonBallDisplay>().AreaOfEffect);
    }

    void CheckGameState(int gameState)
    {
    
        switch(gameState)
        {
            case 3:
            {
                // Debug.Log("CannonProperties.Case3");
                DeselectAllTargets();
                break;
            }

            case 5:
            {
                // Debug.Log("CannonProperties.Case5");
                FireAtTargets();
                // Debug.Log("CannonProperties.Case5: FireAtTargets finished");
                EmptyCannon(false);
                CannonManager.SetGameState(6);  // Change to Enemy phase once that's set up
                // Debug.Log("CannonProperties.Case5: SetGameState(6)");
                break;
            }
            default:
            {
                // Debug.Log("CannonProperties.CaseDefault" + gameState);
                break;
            }
        }
    }   //End of CheckGameState
}
