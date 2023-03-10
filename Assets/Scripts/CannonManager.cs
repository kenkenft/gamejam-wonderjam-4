using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    private CannonBallLoadingManager _cannonBallLoadingManager;  
    [SerializeField] private int _gameState; // 0 is start or initial setup; 1 is replenishment; 2 is loading; 3 is aiming; 4 is firing; 5 is update cannonballs and enemies; 6 is endgame checks

    void Start()
    {
        SetUp();
    }
    public void SetUp()
    {
        _gameState = 0;
        _cannonBallLoadingManager = GetComponentInChildren<CannonBallLoadingManager>();
        _cannonBallLoadingManager.SetUpLoadingPhase();
    }

    public void SetGameState(int state)
    {
        _gameState = state;
    }

    public int GetGameState()
    {
        return _gameState;
    }
}
