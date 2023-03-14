using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public GameObject ReplayButton;
    private void OnEnable()
    {
        CannonManager.OnGameStateChange += CheckGameState;
    }

    private void OnDisable()
    {
        CannonManager.OnGameStateChange -= CheckGameState;
    }

    void CheckGameState(int gameState)
    {
    
        switch(gameState)
        {
            case 7:
            {
                this.GetComponent<SpriteRenderer>().enabled = true;
                ReplayButton.GetComponent<SpriteRenderer>().enabled = true;
                break;
            }
            default:
            {
                this.GetComponent<SpriteRenderer>().enabled = false;
                ReplayButton.GetComponent<SpriteRenderer>().enabled = false;
                break;
            }
        }
    }   //End of CheckGameState
}
