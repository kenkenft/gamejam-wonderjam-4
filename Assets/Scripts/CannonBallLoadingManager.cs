using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallLoadingManager : MonoBehaviour
{
    public GameObject[] SelectableCannonBallZones = new GameObject[6];

    private List<string> _cannonBallPool = new List<string>{};
    public PlayerData PlayerDataInstance;
    // public GameProperties GamePropertiesInstance;

    public List<GameObject> CannonBallObjectPool = new List<GameObject>(){};
    
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
        //Set up CannonBallPoolManager;
        ReplenishSelectableCannonBalls();

        // SetZonesSelectableState(true);
        // Method that sets CannonManager._gameState to 2.
    }

    void SetUpCannonBallPool()
    {
        GetCannonBallPool("s");
        GetCannonBallPool("m");
        GetCannonBallPool("l");
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
    }

    public void SetZonesSelectableState(bool state)
    {
        for(int i = 0 ; i < SelectableCannonBallZones.Length; i++)
            SelectableCannonBallZones[i].GetComponent<TileProperties>().SetIsTargetable(state);
    }

    void CheckGameState(int gameState)
    {
        switch(gameState)
        {
            case 1:
            {
                Debug.Log("gameState is 1! Calling SetUpLoadingPhase");
                SetUpLoadingPhase();
                break;
            }
            case 2:
            {
                Debug.Log("gameState is 2! Calling SetZonesSelectableState(true)");
                SetZonesSelectableState(true);
                break;
            }
            case 3:
            {
                Debug.Log("gameState is 3! Calling SetZonesSelectableState(false)" );
                SetZonesSelectableState(false);
                break;
            }
            default:
            {
                Debug.Log("gameState is " + gameState + ". CannonBallLoadingManager doesn't do anything");
                break;
            }
        }
    }
}
