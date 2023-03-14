using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> EnemySpawnZones = new List<GameObject>();

    [SerializeField] private List<string> _enemySpawnPool = new List<string>{};
    // public PlayerData PlayerDataInstance;

    public List<GameObject> EnemyPrefabPool = new List<GameObject>(){};

    public GameObject EnemyPrefab;
    public CannonProperties CannonInstance;
    public EnemyTileManager EnemyTileManagerInstance;
    [SerializeField] private int _spawnTimer = 2, _spawnCounter;

    [HideInInspector]public delegate bool OnPhaseThree();
    [HideInInspector]public static OnPhaseThree AnyEnemiesOnGrid;
    
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
        _spawnCounter = _spawnTimer;
        SetUpSpawnZones();
        SetUpEnemyPool();
        SpawnEnemies(4);
    }

    void SetUpSpawnZones()
    {
        for(int i = 0; i < EnemyTileManagerInstance.EnemyTileDict[0].Count; i++)
            EnemySpawnZones.Add(EnemyTileManagerInstance.EnemyTileDict[0][i]);
    }

    void SetUpEnemyPool()
    {
        GetEnemyPool(0);
        InstantiateEnemyPrefabs(_enemySpawnPool.Count);
    }

    void GetEnemyPool(int stageID)
    {
        string[] tempArray = GameProperties.GetEnemyStageSpawnPool(stageID);
        for(int i = 0; i < tempArray.Length; i++)
            _enemySpawnPool.Add(tempArray[i]);
    }


    void SpawnEnemies(int amount)
    {
        int tempInt = Mathf.Clamp(amount, 0, _enemySpawnPool.Count);
        //Process which sets up the Enemy object
        for(int i = 0; i< tempInt; i++)
        {
            SetUpEnemy();
        }

    }

    void SpawnMoreEnemies()
    {
        if(IsSpawnSpaceAvailable())
        {
            int rand = UnityEngine.Random.Range(0,GameProperties.GetEnemyGridDimensions()[0]-1);
            SpawnEnemies(rand);
            _spawnCounter = UnityEngine.Random.Range(0,_spawnTimer);;
        }
            
    }

    bool IsTimeToSpawn()
    {
        if(_spawnCounter <= 0)
            return true;
        else
        {   
            _spawnCounter--;
            _spawnCounter = Mathf.Clamp(_spawnCounter, 0, 999); 
            return false;
        }
    }

    bool IsSpawnSpaceAvailable()
    {
        for(int i = 0; i< EnemySpawnZones.Count; i++)
        {
            if(!EnemySpawnZones[i].GetComponent<TileProperties>().GetIsOccupied())
                return true;
        }
        return false;
    }
    void SetUpEnemy()
    {
        List<int> tempList = new List<int>();

        for(int i = 0; i < EnemySpawnZones.Count; i++)
        {
            if(!EnemySpawnZones[i].GetComponent<TileProperties>().GetIsOccupied())  //Only spawn enemy in a free spawn zone
            {
                tempList.Add(i);
                // SetEnemyData(i, GetFromPool());
            }
        }
        if(tempList.Count > 0)
            SetEnemyData(tempList[UnityEngine.Random.Range(0,tempList.Count-1)], GetFromPool());
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

    void SetEnemyData(int zoneID, string typeID)
    {
        // Debug.Log("Empty zone detected. Zone: " + zoneID + ". Selected cannonball type ID: " + typeID);
        Enemy enemyData = GameProperties.GetEnemyType(typeID[0].ToString(), System.Convert.ToInt32(char.GetNumericValue(typeID[1])));
        GameObject tempGameObject = GetPooledPrefab();
        tempGameObject.SetActive(true);

        tempGameObject.GetComponent<EnemyDisplay>().EnemySO = enemyData;
        tempGameObject.GetComponent<EnemyDisplay>().SetUpDisplay();
        EnemySpawnZones[zoneID].GetComponent<TileProperties>().SetOccupant(tempGameObject);
        EnemySpawnZones[zoneID].GetComponent<TileProperties>().SetIsOccupied(true);

        tempGameObject.transform.position = EnemySpawnZones[zoneID].transform.position;
        tempGameObject.transform.parent = EnemySpawnZones[zoneID].transform;
    }

    public void InstantiateEnemyPrefabs(int poolSize)
    {
        // Method that instantiates pool of enemy prefabs
        GameObject tmpObj;
        for(int i = 0; i < poolSize; i++)
        {
            tmpObj = Instantiate(EnemyPrefab);
            tmpObj.name = "EnemyPrefab" + i;
            tmpObj.SetActive(false);   // Set prefab spriterender to inactive to hide connector
            tmpObj.transform.parent = transform;
            EnemyPrefabPool.Add(tmpObj);
        }
    }

    GameObject GetPooledPrefab()
    {
        // Method returns a inactive connector gameobject prefab 
        for(int i = 0; i < EnemyPrefabPool.Count; i ++)
        {
            if(!EnemyPrefabPool[i].activeInHierarchy)
            {
                return EnemyPrefabPool[i];
            }
        }
        // Debug.Log("No inactive EnemyPrefabs found - All pooled EnemyPrefabs used");
        return null;
    }

    void CheckGameState(int gameState)
    {   
        switch(gameState)
        {
            case 2:
            {
                // Debug.Log("EnemyManager.Case2");
                SetUpEnemies();
                break;
            }
            case 3:
            {
                // Debug.Log("EnemyManager.Case3");
                if(_enemySpawnPool.Count <= 0 && !AnyEnemiesOnGrid.Invoke())
                    CannonManager.SetGameState(7);
                else if((IsTimeToSpawn() || !AnyEnemiesOnGrid.Invoke()) && _enemySpawnPool.Count > 0 )
                    SpawnMoreEnemies();
                break;
            }
            default:
            {
                // Debug.Log("EnemyManager.CaseDefault" + gameState);
                break;
            }
        }
    }   //End of CheckGameState
}
