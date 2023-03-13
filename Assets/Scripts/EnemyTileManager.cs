using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTileManager : MonoBehaviour
{
    public GameObject Tile;     // Prefab of Tile
    public CannonProperties CannonInstance;
    [SerializeField] public Dictionary<int, List<GameObject>> EnemyTileDict = new Dictionary<int, List<GameObject>>();
    public int BoardHeight, BoardWidth;
    
    private float _tileMargin = 1.20f;       // Percentage margin of extra space around tile
    private GameObject[,] tiles;

    private void OnEnable()
    {
        CannonManager.OnGameStateChange += CheckGameState;
    }

    private void OnDisable()
    {
        CannonManager.OnGameStateChange -= CheckGameState;
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
        for (int row = 0; row < BoardHeight; row++) 
        {      
            List<GameObject> tempList = new List<GameObject>();
            for (int col = 0; col < BoardWidth; col++) 
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
