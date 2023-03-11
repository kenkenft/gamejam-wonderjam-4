using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    // private CannonBallLoadingManager _cannonBallLoadingManager;  
    [SerializeField] private CannonProperties _cannonProperties;
    [SerializeField] private int _gameState; // 0 is on Title Screen; 1 is start or initial setup; 2 is replenishment; 3 is loading; 4 is aiming; 5 is firing; 6 is update cannonballs and enemies; 7 is endgame checks

    void Start()
    {
        SetUp();
    }
    public void SetUp()
    {
        OnGameStateChange += SetGameState;
        OnGameStateChange += DelegateMultiCastTest;
        OnGameStateChange?.Invoke(1);
        OnGameStateChange?.Invoke(4);
        
        // _cannonBallLoadingManager = GetComponentInChildren<CannonBallLoadingManager>();
        // _cannonBallLoadingManager.SetUpLoadingPhase();

        // _cannonProperties = GetComponentInChildren<CannonProperties>();
    }

    public void SetGameState(int state)
    {
        _gameState = state;
        Debug.Log("_gameState " + _gameState);
    }

    public void DelegateMultiCastTest(int target)
    {
        switch(target)
        {
            case 0:
            {
                Debug.Log("Case 0 triggered!");
                break;
            }
            case 1:
            {
                Debug.Log("Case 1 triggered!");
                break;
            }
            default:
            {
                Debug.Log("No valid case");
                break;
            }
        }
    }

    public int GetGameState()
    {
        return _gameState;
    }

    public delegate void OnChangeDelegate(int value);
    public static OnChangeDelegate OnGameStateChange;
    
}
