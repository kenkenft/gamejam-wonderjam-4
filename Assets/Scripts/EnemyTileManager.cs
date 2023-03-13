using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTileManager : MonoBehaviour
{
    public GameObject Tile;     // Prefab of Tile
    public CannonProperties CannonInstance;
    [SerializeField] public Dictionary<int, List<GameObject>> EnemyTileDict = new Dictionary<int, List<GameObject>>();
    // public int BoardHeight, BoardWidth;
    
    private float _tileMargin = 1.20f;       // Percentage margin of extra space around tile
    private GameObject[,] tiles;


    private void OnEnable()
    {
        CannonManager.OnGameStateChange += CheckGameState;
        CannonProperties.CheckHitAnything += CheckHitAnything;
    }

    private void OnDisable()
    {
        CannonManager.OnGameStateChange -= CheckGameState;
        CannonProperties.CheckHitAnything -= CheckHitAnything;
    }
    public void SetUpBoard()
    {
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
        for (int row = 0; row < GameProperties.GetEnemyGridDimensions()[0]; row++) 
        {      
            List<GameObject> tempList = new List<GameObject>();
            for (int col = 0; col < GameProperties.GetEnemyGridDimensions()[1]; col++) 
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

        for(int row = searchGridCorners[0]; row <= searchGridCorners[1]; row++)
        {
            for(int col = searchGridCorners[2]; col <= searchGridCorners[3]; col++)
            {
                if(EnemyTileDict[row][col].GetComponent<TileProperties>().GetIsOccupied())
                {
                    EnemyTileDict[row][col].GetComponent<TileProperties>().GetOccupant().GetComponent<EnemyDisplay>().SubtractHP(targetData[2]);
                }
            }
        }
    }

    int[] SetUpGridSearchBoundaries(int[] targetData)
    {
        int[] searchGridCorners = new int[4];
        
        searchGridCorners[0] = Mathf.Clamp(targetData[0]- targetData[3], 0, GameProperties.GetEnemyGridDimensions()[0]);
        searchGridCorners[1] = Mathf.Clamp(targetData[0]+ targetData[3], 0, GameProperties.GetEnemyGridDimensions()[0]);
        searchGridCorners[2] = Mathf.Clamp(targetData[1]- targetData[3], 0, GameProperties.GetEnemyGridDimensions()[1]);
        searchGridCorners[3] = Mathf.Clamp(targetData[1]+ targetData[3], 0, GameProperties.GetEnemyGridDimensions()[1]);

        return searchGridCorners;   // Assumes {Left-most boundary, right-most boundary, bottom-most boundary, top-most boundary}
    }

    void CheckGameState(int gameState)
    {
    
        switch(gameState)
        {
            case 0:
                break;
            case 1:
            {
                Debug.Log("gameState is 1! Calling SetUpLoadingPhase");
                SetUpBoard();
                CannonManager.SetGameState(2);
                break;
            }
            case 4:
            {
                SetEnemyTileIsTargetable(true);
                break;
            }
            default:
            {
                SetEnemyTileIsTargetable(false);
                break;
            }
        }
    }   //End of CheckGameState
}
