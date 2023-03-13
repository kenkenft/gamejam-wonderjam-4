using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> EnemySpawnZones = new List<GameObject>();

    private List<string> _enemySpawnPool = new List<string>{};
    // public PlayerData PlayerDataInstance;

    public List<GameObject> EnemyObjectPool = new List<GameObject>(){};
    public CannonProperties CannonInstance;
    public bool IsSetUp = false;
    private int _spawnTimer = 4;
    
    private void OnEnable()
    {
        CannonManager.OnGameStateChange += CheckGameState;
    }

    private void OnDisable()
    {
        CannonManager.OnGameStateChange -= CheckGameState;
    }
    public void SetUpEnemies()
    {
        SetUpEnemyPool();
        SpawnEnemies(4);
    }

    void SetUpEnemyPool()
    {
        GetEnemyPool(0);
    }

    public void GetEnemyPool(int stageID)
    {
        string[] tempArray = GameProperties.GetEnemyStageSpawnPool(stageID);
        for(int i = 0; i < tempArray.Length; i++)
            _enemySpawnPool.Add(tempArray[i]);
    }

    void SpawnEnemies(int amount)
    {
        int tempInt = Mathf.Clamp(amount, 0, _enemySpawnPool.Count);
        //Process which sets up the Enemy object
        IsSetUp = false;
    }

    void SpawnMoreEnemies()
    {
        if(IsTimeToSpawn())
            SpawnEnemies(2);
    }

    bool IsTimeToSpawn()
    {
        if(_spawnTimer <= 0)
        {
            _spawnTimer = 4;
            return true;
        } 
        else
        {
            _spawnTimer--;
            return false;
        }
        
    }

    public void ReplenishSelectableCannonBalls()
    {
        for(int i = 0; i < EnemySpawnZones.Count; i++)
        {
            if(!EnemySpawnZones[i].GetComponent<TileProperties>().GetIsOccupied())
                SetCannonBallData(i, GetFromPool());
        }
    }

    string GetFromPool()
    {
        int rand;
        string selected;

        rand = UnityEngine.Random.Range(0,_enemySpawnPool.Count-1);
        selected = _enemySpawnPool[rand];
        _enemySpawnPool.RemoveAt(rand);

        return selected;
    }

    void SetCannonBallData(int zoneID, string typeID)
    {
        // Debug.Log("Empty zone detected. Zone: " + zoneID + ". Selected cannonball type ID: " + typeID);
        CannonBall cannonBallData = GameProperties.GetCannonBallType(typeID[0].ToString(), System.Convert.ToInt32(char.GetNumericValue(typeID[1])));
        
        EnemyObjectPool[zoneID].GetComponent<CannonBallDisplay>().Cb = cannonBallData;
        EnemyObjectPool[zoneID].GetComponent<CannonBallDisplay>().SetUpDisplay();
        EnemySpawnZones[zoneID].GetComponent<TileProperties>().SetOccupant(EnemyObjectPool[zoneID]);
        EnemySpawnZones[zoneID].GetComponent<TileProperties>().SetIsOccupied(true);

        EnemyObjectPool[zoneID].transform.position = EnemySpawnZones[zoneID].transform.position;
        EnemyObjectPool[zoneID].transform.parent = EnemySpawnZones[zoneID].transform;
    }

    public void SetZonesSelectableState(bool state)
    {
        for(int i = 0 ; i < EnemySpawnZones.Count; i++)
            EnemySpawnZones[i].GetComponent<TileProperties>().SetIsTargetable(state);
    }

    void SetZonesCannonReference()
    {
        for(int i = 0 ; i < EnemySpawnZones.Count; i++)
            EnemySpawnZones[i].GetComponent<TileProperties>().SetCannonRef(CannonInstance);
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
                Debug.Log("gameState is 2! Calling Setting up enemies");
                SetUpEnemies();
                break;
            }
            case 3:
            {
                SpawnMoreEnemies();
                break;
            }
            default:
                break;
        }
    }   //End of CheckGameState
}
