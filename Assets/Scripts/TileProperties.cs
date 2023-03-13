using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProperties : MonoBehaviour
{
    [SerializeField] private bool _isOccupied, _isTargetable, _isDesignated = false;
    [SerializeField] private int[] tileXY = new int[2]; // tile's column and row position on targetable grid. index 0 is column, index 1 is row
    [SerializeField] private GameObject _occupant;
    [SerializeField] private CannonProperties _cannonProperties;

    public void SetUp()
    {

    }

    public void SetTileCoordinates(int col, int row)
    {
        tileXY[0] = col;
        tileXY[1] = row;
    }
    public int[] GetTileCoordinates()
    {
        return tileXY;
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
        if(_isTargetable)
        {
            Debug.Log("Tile is targetable");
            CheckGameState();
        }
        else
        {
            Debug.Log("Tile is not targetable");
        }
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

    public void SetCannonRef(CannonProperties cannonRef)
    {
        _cannonProperties = cannonRef;
    }

    void CheckGameState()
    {
        switch(CannonManager.GetGameState())
        {
            case 3:
            {
                Debug.Log("Tile clicked during loading phase! Load this into cannon!");
                _cannonProperties.LoadInToCannon(_occupant);
                break;
            }
            case 4:
            {
                Debug.Log("Tile clicked during aiming phase! Checking _isDesignated");
                if(!_isDesignated)
                    _cannonProperties.DesignateTarget(this.gameObject);
                else
                    _cannonProperties.DelistTarget(tileXY);

                _isDesignated = !_isDesignated;

                break;
            }
            default:
            {
                Debug.Log("Tile clicked during other states! That shouldn't be possible!");
                break;
            }
        }
    }
}
