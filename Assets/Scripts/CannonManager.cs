using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    private CannonBallLoadingManager cannonBallLoadingManager;  
    [SerializeField] private int gameState = 0; // 0 is start or initial setup; 1 is replenishment; 2 is loading; 3 is aiming; 4 is firing; 5 is update cannonballs and enemies; 6 is endgame checks

    public void SetUp()
    {
        cannonBallLoadingManager = GetComponentInChildren<CannonBallLoadingManager>();
    }
}
