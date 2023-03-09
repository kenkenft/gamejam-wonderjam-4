using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallLoadingManager : MonoBehaviour
{
    public GameObject[] SelectableCannonBallZones = new GameObject[6];

    private List<string> _cannonBallPool = new List<string>{};
    public PlayerData playerData;
    public GameProperties gameProperties;
    
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
        // ToDo From player property file, fetch player's current CannonBallPool configuration
        string[] tempArray = playerData.GetCannonBallPool(targetPool);
        for(int i = 0; i < tempArray.Length; i++)
            _cannonBallPool.Add(tempArray[i]);
    }

    public void ReplenishSelectableCannonBalls()
    {
        // List<int> emptyZonesList = GetEmptyZonesID(); 

        for(int i = 0; i < SelectableCannonBallZones.Length; i++)
        {
            if(!SelectableCannonBallZones[i].GetComponent<TileProperties>().GetIsOccupied())
            {
                //ToDo method to select CannonBall from CannonBallPool
                //ToDo create CannonBallPoolManager
                GetFromPool();
            }
        }
    }

    string GetFromPool()
    {
        int rand;
        string selected;
        rand = Random.Range(0,_cannonBallPool.Count-1);
        selected = _cannonBallPool[rand];
        _cannonBallPool.RemoveAt(rand);
        return selected;
    }

}
