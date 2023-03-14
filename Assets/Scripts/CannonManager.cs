using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    // private CannonBallLoadingManager _cannonBallLoadingManager;  
    [SerializeField] private CannonProperties _cannonProperties;
    [SerializeField] private static int _gameState = 0; // 0 is on Title Screen; 1 is start or initial setup; 2 is replenishment; 3 is loading; 4 is aiming; 5 is firing; 6 is update cannonballs and enemies; 7 is endgame checks
    [HideInInspector]public delegate void OnChangeDelegate(int value);
    [HideInInspector]public static OnChangeDelegate OnGameStateChange;

    void OnEnable()
    {
        PhaseButtonClick.SetGameState += SetGameState;
    }
    void OnDiable()
    {
        PhaseButtonClick.SetGameState -= SetGameState;
    }
    void Start()
    {
        SetGameState(1);
    }

    public static void SetGameState(int state)
    {
        int counter = OnGameStateChange.GetInvocationList().Length;
        Debug.Log("Number of subscribers to OnGameStateChange:" + counter);
        if(state != _gameState)
        {
            _gameState = state;
            OnGameStateChange?.Invoke(_gameState);
            Debug.Log("_gameState updated! " + _gameState);
        }
    }

    public static int GetGameState()
    {
        return _gameState;
    }

}
