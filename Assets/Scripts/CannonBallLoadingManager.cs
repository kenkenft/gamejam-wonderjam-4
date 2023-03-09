using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallLoadingManager : MonoBehaviour
{
    public GameObject[] SelectableCannonBallZones = new GameObject[6];

    private List<string> _cannonBallPool = new List<string>{};
    public PlayerData PlayerDataInstance;
    public GameProperties GamePropertiesInstance;

    public List<GameObject> CannonBallObjectPool = new List<GameObject>(){};
    
    void Start()
    {
        SetUp();
    }
    public void SetUp()
    {
        SetUpCannonBallPool();
        //Set up CannonBallPoolManager;
        ReplenishSelectableCannonBalls();
    }

    void SetUpCannonBallPool()
    {
        GetCannonBallPool("s");
        GetCannonBallPool("m");
        GetCannonBallPool("l");
    }

    public void GetCannonBallPool(string targetPool)
    {
        // Debug.Log("GetCannonBallPool called. Fetching " + targetPool + " pool");
        // ToDo From player property file, fetch player's current CannonBallPool configuration
        string[] tempArray = PlayerDataInstance.GetCannonBallPool(targetPool);
        for(int i = 0; i < tempArray.Length; i++)
            _cannonBallPool.Add(tempArray[i]);

        // Debug.Log("Retrieved " + tempArray.Length + " items");
    }

    public void ReplenishSelectableCannonBalls()
    {
        // List<int> emptyZonesList = GetEmptyZonesID(); 
        Debug.Log("ReplenishSelectableCannonBalls called");
        for(int i = 0; i < SelectableCannonBallZones.Length; i++)
        {
            if(!SelectableCannonBallZones[i].GetComponent<TileProperties>().GetIsOccupied())
            {
                //ToDo method to select CannonBall from CannonBallPool
                //ToDo create CannonBallPoolManager
                // Debug.Log("Empty zone detected. Zone: " + i + ". Selected cannonball type ID: " + GetFromPool());
                SetCannonBallData(i, GetFromPool());
            }
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
        Debug.Log("Empty zone detected. Zone: " + zoneID + ". Selected cannonball type ID: " + typeID);
        CannonBall cannonBallData = GamePropertiesInstance.GetCannonBallType(typeID[0].ToString(), System.Convert.ToInt32(char.GetNumericValue(typeID[1])));
        
        CannonBallObjectPool[zoneID].GetComponent<CannonBallDisplay>().Cb = cannonBallData;
        CannonBallObjectPool[zoneID].GetComponent<CannonBallDisplay>().SetUpDisplay();
        SelectableCannonBallZones[zoneID].GetComponent<TileProperties>().SetOccupant(CannonBallObjectPool[zoneID]);
        SelectableCannonBallZones[zoneID].GetComponent<TileProperties>().SetIsOccupied(true);

        CannonBallObjectPool[zoneID].transform.position = SelectableCannonBallZones[zoneID].transform.position;
    }

}
