using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProperties : MonoBehaviour
{
    [SerializeField] private bool _isOccupied, _isTargetable;
    [SerializeField] private int[] tileXY = new int[2]; // tile's column and row position on targetable grid. index 0 is column, index 1 is row
    [SerializeField] private GameObject _occupant;

    public void SetUp()
    {

    }

    public void SetTileCoordinates(int col, int row)
    {
        tileXY[0] = col;
        tileXY[1] = row;
    }

    public void SetIsOccupied(bool state)
    {
        _isOccupied = state;
    }

    public void SetIsTargetable(bool state)
    {
        _isTargetable = state;
    }

    void OnMouseDown()
    {
        Debug.Log("Tile Clicked!");
    }

    public bool GetIsOccupied()
    {
        return _isOccupied;
    }

    public void SetOccupant(GameObject newOccupant)
    {
        _occupant = newOccupant;
    }

    public GameObject GetOccupant()
    {
        return _occupant;
    }

    public void RemoveOccupant()
    {
        _occupant = null;
    }
}
