using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallLoadingManager : MonoBehaviour
{
    public GameObject[] SelectableCannonBallZones = new GameObject[6];

    private List<string> _cannonBallPool = new List<string>{};
    public PlayerData PlayerDataInstance;

    public List<GameObject> CannonBallObjectPool = new List<GameObject>(){};
    public CannonProperties CannonInstance;
    [HideInInspector]public delegate void OnEnterPhase(bool isUnloading);
    [HideInInspector]public static OnEnterPhase UnloadCannon;
    
    private void OnEnable()
    {
        CannonManager.OnGameStateChange += CheckGameState;
    }

    private void OnDisable()
    {
        CannonManager.OnGameStateChange -= CheckGameState;
    }
    public void SetUpLoadingPhase()
    {
        SetUpCannonBallPool();
        //ToDo Set up CannonBallPoolManager;
        //Return any and all loaded cannonballs to respective zones EmptyCannon() 
        ReplenishSelectableCannonBalls();
    }

    void SetUpCannonBallPool()
    {
        GetCannonBallPool("s");
        GetCannonBallPool("m");
        GetCannonBallPool("l");
        GetCannonBallPool("x");
    }

    public void GetCannonBallPool(string targetPool)
    {
        string[] tempArray = PlayerDataInstance.GetCannonBallPool(targetPool);
        for(int i = 0; i < tempArray.Length; i++)
            _cannonBallPool.Add(tempArray[i]);
    }

    public void ReplenishSelectableCannonBalls()
    {
        for(int i = 0; i < SelectableCannonBallZones.Length; i++)
        {
            if(!SelectableCannonBallZones[i].GetComponent<TileProperties>().GetIsOccupied())
                SetCannonBallData(i, GetFromPool());
        }
    }

    string GetFromPool()
    {
        int rand;
        string selected;

        rand = UnityEngine.Random.Range(0,_cannonBallPool.Count-1);
        selected = _cannonBallPool[rand];
        _cannonBallPool.RemoveAt(rand);

        return selected;
    }

    void SetCannonBallData(int zoneID, string typeID)
    {
        // Debug.Log("Empty zone detected. Zone: " + zoneID + ". Selected cannonball type ID: " + typeID);
        CannonBall cannonBallData = GameProperties.GetCannonBallType(typeID[0].ToString(), System.Convert.ToInt32(char.GetNumericValue(typeID[1])));
        
        CannonBallObjectPool[zoneID].GetComponent<CannonBallDisplay>().Cb = cannonBallData;
        CannonBallObjectPool[zoneID].GetComponent<CannonBallDisplay>().SetUpDisplay();
        SelectableCannonBallZones[zoneID].GetComponent<TileProperties>().SetOccupant(CannonBallObjectPool[zoneID]);
        SelectableCannonBallZones[zoneID].GetComponent<TileProperties>().SetIsOccupied(true);

        CannonBallObjectPool[zoneID].transform.position = SelectableCannonBallZones[zoneID].transform.position;
        CannonBallObjectPool[zoneID].transform.parent = SelectableCannonBallZones[zoneID].transform;
    }

    public void SetZonesSelectableState(bool state)
    {
        for(int i = 0 ; i < SelectableCannonBallZones.Length; i++)
            SelectableCannonBallZones[i].GetComponent<TileProperties>().SetIsTargetable(state);
    }

    void SetZonesCannonReference()
    {
        for(int i = 0 ; i < SelectableCannonBallZones.Length; i++)
            SelectableCannonBallZones[i].GetComponent<TileProperties>().SetCannonRef(CannonInstance);
    }

    void PlaceUnloadedCannonBallsInZones(List<GameObject> unloadedCannonBalls)
    {
        Debug.Log("Cannonballs to be unloaded: " + unloadedCannonBalls.Count);
    }

    void CheckGameState(int gameState)
    {
    
        switch(gameState)
        {
            case 2:
            {
                Debug.Log("CanonProperties.Case2");
                SetUpLoadingPhase();
                CannonManager.SetGameState(GameProperties.GetPhaseID("loading"));
                break;
            }
            case 3:
            {
                Debug.Log("CanonProperties.Case3");
                SetZonesCannonReference();
                SetZonesSelectableState(true);
                UnloadCannon?.Invoke(true);
                ReplenishSelectableCannonBalls();
                break;
            }
            case 4:
            {
                Debug.Log("CanonProperties.Case4" );
                SetZonesSelectableState(false);
                break;
            }
            default:
            {
                Debug.Log("CanonProperties.CaseDefault" + gameState);
                break;
            }
        }
    }   //End of CheckGameState
}
