using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTileManager : MonoBehaviour
{
    public GameObject Tile;     // Prefab of Tile and Connector pieces
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
        //
        // tiles = new GameObject[boardHeight, boardWidth];

        // At the moment, this will set the first tile at the center of the board gameObject instead of the corner of the board gameObject
        float startX = transform.position.x;    
        float startY = transform.position.y;

        // Start of for loop for setting tile objects' positions and properties
        for (int row = 0; row < BoardHeight; row++) 
        {      
            for (int col = 0; col < BoardWidth; col++) 
            {
                GameObject newTile = Instantiate(Tile, new Vector3(startX + (tileOffsetX * col), startY - (tileOffsetY * row), 0), Tile.transform.rotation);
                       
                // tiles[row, col] = newTile;
                newTile.transform.parent = transform;
                SetTileProperties(newTile, row, col); // Set tile properties e.g. tile id, element, tile sprite 
                // Debug.Log("row: " + row + ", col: " + col + ", tiledID: " + newTile.GetComponent<TileProperties>().GetTileID());
            }
        } // End of for loop for placing tile objects
    }   ////// End of CreateTiles()

    public void SetTileProperties(GameObject targetTile, int row, int col)
    {
        TileProperties tileClass = targetTile.GetComponent<TileProperties>();
        // Set the individual tile's properties
        
        // tileClass.SetTileID(col + (boardHeight * row));  // Causes autotest failure
        targetTile.name = tileClass.GetTileCoordinates()[0].ToString() + tileClass.GetTileCoordinates()[1].ToString();
    }   ////// End of SetTileProperties()

    void CheckGameState(int gameState)
    {
    
        switch(gameState)
        {
            case 1:
            {
                Debug.Log("gameState is 1! Calling SetUpLoadingPhase");
                SetUpBoard();
                CannonManager.SetGameState(2);
                break;
            }
            default:
                break;
        }
    }   //End of CheckGameState
}
