using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProperties : MonoBehaviour
{
    [SerializeField] private bool _isOccupied, _isTargetable, _isDesignated = false;
    [SerializeField] private int[] tileXY = new int[2]; // tile's column and row position on targetable grid. index 0 is column, index 1 is row
    [SerializeField] private GameObject _occupant;
    [SerializeField] private SpriteRenderer _tileReticle;
    [SerializeField] private CannonProperties _cannonProperties;

    [HideInInspector]public delegate void OnMouseOvering(int[] tileCoords, bool isHighlight);
    [HideInInspector]public static OnMouseOvering PassAlongForHighlighting;

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
            // Debug.Log("Tile is targetable");
            CheckGameState();
        }
        else
        {
            // Debug.Log("Tile is not targetable");
        }
    }

    void OnMouseOver()
    {
        if((CannonManager.GetGameState() == 4) && _isTargetable)
        {
            // Debug.Log("Highlight Tiles");
            PassAlongForHighlighting?.Invoke(tileXY, true);
        }
    }

    void OnMouseExit()
    {
        if((CannonManager.GetGameState() == 4) && _isTargetable)
        {
            // Debug.Log("Exit Tile");
            PassAlongForHighlighting?.Invoke(tileXY, false);
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
                // Debug.Log("TileProperties.Case3");
                _cannonProperties.LoadInToCannon(this.gameObject);
                _isTargetable = false;
                break;
            }
            case 4:
            {
                // Debug.Log("TileProperties.Case4");
                bool isTaskComplete = DesignateOrDelist();
                if(isTaskComplete)
                {    
                    _isDesignated = !_isDesignated;
                    _tileReticle.enabled = _isDesignated;
                }

                break;
            }
            default:
            {
                // Debug.Log("TileProperties.CaseDefault" + CannonManager.GetGameState());
                break;
            }
        }
    } //End of CheckGameState

    bool DesignateOrDelist()
    {
        if(!_isDesignated)
            return _cannonProperties.DesignateTarget(this.gameObject);
        else
            return _cannonProperties.DelistTarget(tileXY);
    }

    public void DeselectAndHideReticle()
    {
        _isDesignated = false;
        _tileReticle.enabled = false;
    }

    public bool GetIsDesignated()
    {
        return _isDesignated;
    }

    public void SetTileReticle(bool state)
    {
        _tileReticle.enabled = state;
    }
}
