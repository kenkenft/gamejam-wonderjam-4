using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTileManager : MonoBehaviour
{
    public GameObject Tile;     // Prefab of Tile
    public CannonProperties CannonInstance;
    [SerializeField] public Dictionary<int, List<GameObject>> EnemyTileDict = new Dictionary<int, List<GameObject>>();
    private int  _gridWidth, _gridHeight;
    
    private float _tileMargin = 1.20f;       // Percentage margin of extra space around tile
    private GameObject[,] tiles;


    private void OnEnable()
    {
        CannonManager.OnGameStateChange += CheckGameState;
        CannonProperties.CheckHitAnything += CheckHitAnything;
        EnemyManager.AnyEnemiesOnGrid += CheckAnyOccupants;
        CannonProperties.PassToEnemyTileManager += HighlightAffectedTiles;
    }

    private void OnDisable()
    {
        CannonManager.OnGameStateChange -= CheckGameState;
        CannonProperties.CheckHitAnything -= CheckHitAnything;
        EnemyManager.AnyEnemiesOnGrid -= CheckAnyOccupants;
        CannonProperties.PassToEnemyTileManager += HighlightAffectedTiles;
    }
    public void SetUpBoard()
    {
        _gridWidth = GameProperties.GetEnemyGridDimensions()[0]; 
        _gridHeight = GameProperties.GetEnemyGridDimensions()[1];
        Vector2 offset = _tileMargin * Tile.GetComponentInChildren<SpriteRenderer>().bounds.size;
        CreateTiles(offset.x, offset.y);
    }   ////// End of SetUpBoard()

    public void CreateTiles(float tileOffsetX, float tileOffsetY) 
    {

        // At the moment, this will set the first tile at the center of the board gameObject instead of the corner of the board gameObject
        float startX = transform.position.x;    
        float startY = transform.position.y;

        // List<GameObject> tempList = new List<GameObject>();
        // Start of for loop for setting tile objects' positions and properties
        for (int row = 0; row < _gridHeight; row++) 
        {      
            List<GameObject> tempList = new List<GameObject>();
            for (int col = 0; col < _gridWidth; col++) 
            {
                GameObject newTile = Instantiate(Tile, new Vector3(startX + (tileOffsetX * col), startY - (tileOffsetY * row), 0), Tile.transform.rotation);
                tempList.Add(newTile);
                newTile.transform.parent = transform;
                SetTileProperties(newTile, col, row); // Set tile properties 
            }
            EnemyTileDict.Add(row, tempList);
        } // End of for loop for placing tile objects
    }   ////// End of CreateTiles()

    public void SetTileProperties(GameObject targetTile, int col, int row)
    {
        TileProperties tileClass = targetTile.GetComponent<TileProperties>();
        // Set the individual tile's properties
        targetTile.name = col.ToString() + row.ToString();
        tileClass.SetTileCoordinates(col, row);
        tileClass.SetIsTargetable(false);
        tileClass.SetCannonRef(CannonInstance);
    }   ////// End of SetTileProperties()

    void SetEnemyTileIsTargetable(bool state)
    {
        for(int row = 0; row < EnemyTileDict.Count; row ++)
        {
            for(int col = 0; col < EnemyTileDict[row].Count; col ++)
            {
                EnemyTileDict[row][col].GetComponent<TileProperties>().SetIsTargetable(state);
            }
        }
    }

    void CheckHitAnything(int[] targetData)
    {
        // Assumes targetData = {target tile's column/x coordinate, target tile's row/y coordinate, cannonball damage, cannonball area of effect}
        int[] searchGridCorners = SetUpGridSearchBoundaries(targetData);

        List<GameObject> occupiedTiles = FindOccupiedTiles(searchGridCorners);
        if(occupiedTiles.Count > 0)
            ApplyDamageToOccupants(occupiedTiles, targetData[2]);
    }

    int[] SetUpGridSearchBoundaries(int[] targetData)
    {
        int[] searchGridCorners = new int[4];
        
        searchGridCorners[0] = Mathf.Clamp(targetData[0]- targetData[3], 0, _gridWidth-1);
        searchGridCorners[1] = Mathf.Clamp(targetData[0]+ targetData[3], 0, _gridWidth-1);
        searchGridCorners[2] = Mathf.Clamp(targetData[1]- targetData[3], 0, _gridHeight-1);
        searchGridCorners[3] = Mathf.Clamp(targetData[1]+ targetData[3], 0, _gridHeight-1);

        return searchGridCorners;   // Assumes {Left-most boundary, right-most boundary, bottom-most boundary, top-most boundary}
    }

    List<GameObject> FindOccupiedTiles(int[] searchGridCorners)
    {
        List<GameObject> tempList = new List<GameObject>();
        for(int row = searchGridCorners[2]; row <= searchGridCorners[3]; row++)
        {
            for(int col = searchGridCorners[0]; col <= searchGridCorners[1]; col++)
            {
                if(EnemyTileDict[row][col].GetComponent<TileProperties>().GetIsOccupied())
                    tempList.Add(EnemyTileDict[row][col]);
            }
        }
        return tempList;
    }
    void ApplyDamageToOccupants(List<GameObject> occupiedTiles, int damage)
    {
        bool isDefeated;
        for(int i = 0; i < occupiedTiles.Count; i++)
        {
            TileProperties tileProperties = occupiedTiles[i].GetComponent<TileProperties>();
            isDefeated = tileProperties.GetOccupant().GetComponent<EnemyDisplay>().SubtractHP(damage);
            if(isDefeated)
            {
                tileProperties.RemoveOccupant();
                tileProperties.SetIsOccupied(false);
            }
        }
    }

    bool CheckAnyOccupants()
    {
        int[] tempArray = new int[] {_gridWidth, _gridHeight};
        int x = tempArray[0] > tempArray[1] ? tempArray[0] : tempArray[1];
        int[] targetData = {0, 0, 0, x};
        int[] searchGridCorners = SetUpGridSearchBoundaries(targetData);

        List<GameObject> occupiedTiles = FindOccupiedTiles(searchGridCorners);
        if(occupiedTiles.Count > 0)
            return true;
        else
            return false;
    }
    void EnemiesBehaviour()
    {
        int[] tempArray = new int[] {_gridWidth, _gridHeight};
        int x = tempArray[0] > tempArray[1] ? tempArray[0] : tempArray[1];
        int[] targetData = {0, 0, 0, x};
        int[] searchGridCorners = SetUpGridSearchBoundaries(targetData);

        List<GameObject> occupiedTiles = FindOccupiedTiles(searchGridCorners);

        int occupied = occupiedTiles.Count; 

        if(occupied > 0)
        {
            EnemyDisplay tempEnemyDisplay;
            for(int i = 0; i < occupied; i++)
            {
                tempEnemyDisplay = occupiedTiles[i].GetComponentInChildren<EnemyDisplay>();
                bool canTravel = CheckOccupantTravelCounter(tempEnemyDisplay);
                if(canTravel)
                {
                    TileProperties tempTileProperties = occupiedTiles[i].GetComponent<TileProperties>();
                    tempArray = tempTileProperties.GetTileCoordinates();
                    x = tempEnemyDisplay.TravelDistance;
                    TileProperties newTargetTile = EnemyTileDict[tempArray[1] + x ][ tempArray[0]].GetComponent<TileProperties>();
                    if(!newTargetTile.GetIsOccupied())
                    {
                        GameObject enemyObject = tempTileProperties.GetOccupant();
                        //Set is occupied to true, set enemy position to new tile's position, set enemy as new tile occupant,
                        //Remove enemy from old tile, set old tile isoccupied to false
                        newTargetTile.SetIsOccupied(true);
                        newTargetTile.SetOccupant(enemyObject);
                        enemyObject.transform.position = newTargetTile.transform.position;
                        enemyObject.transform.parent = newTargetTile.transform;
                        enemyObject.GetComponent<EnemyDisplay>().ResetTravelCounter();

                        tempTileProperties.SetIsOccupied(false);
                        tempTileProperties.RemoveOccupant();

                    }
                }
            }
        }

    }

    bool CheckOccupantTravelCounter(EnemyDisplay tempEnemyDisplay)
    {
        if(tempEnemyDisplay.TravelCounter > 0)
        {
            tempEnemyDisplay.TravelCounter--;
            return false;
        }
        else
        {
            return true;
        }
    }

    void HighlightAffectedTiles(int[] tileCoords, bool isHighlight, int blastRadius)
    {
        Debug.Log("tileCoords: " +tileCoords[0] + ", " + tileCoords[1] + ". isHighlight: " + isHighlight + ". blastRadius: " + blastRadius);
    }
    void CheckGameState(int gameState)
    {
    
        switch(gameState)
        {
            case 0:
                break;
            case 1:
            {
                Debug.Log("EnemyTileManager.Case1");
                SetUpBoard();
                CannonManager.SetGameState(2);
                break;
            }
            case 4:
            {
                Debug.Log("EnemyTileManager.Case4");
                SetEnemyTileIsTargetable(true);
                break;
            }
            case 6:
            {
                Debug.Log("EnemyTileManager.Case6");
                EnemiesBehaviour();
                Debug.Log("EnemyTileManager.Case6: EnemiesBehaviour finished");
                CannonManager.SetGameState(3);
                Debug.Log("EnemyTileManager.Case6: SetGameState(3)");
                break;
            }
            default:
            {
                Debug.Log("EnemyTileManager.CaseDefault" + gameState);
                SetEnemyTileIsTargetable(false);
                break;
            }
        }
    }   //End of CheckGameState
}
