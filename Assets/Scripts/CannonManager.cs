using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    private CannonBallLoadingManager _cannonBallLoadingManager;  
    [SerializeField] private CannonProperties _cannonProperties;
    [SerializeField] private int _gameState; // 0 is on Title Screen; 1 is start or initial setup; 2 is replenishment; 3 is loading; 4 is aiming; 5 is firing; 6 is update cannonballs and enemies; 7 is endgame checks

    void Start()
    {
        SetUp();
    }
    public void SetUp()
    {
        SetGameState(1);
        _cannonBallLoadingManager = GetComponentInChildren<CannonBallLoadingManager>();
        _cannonBallLoadingManager.SetUpLoadingPhase();

        _cannonProperties = GetComponentInChildren<CannonProperties>();
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
