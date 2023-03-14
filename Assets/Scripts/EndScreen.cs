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
                Debug.Log("EndScreen.CheckGameState.Case7");
                this.GetComponent<SpriteRenderer>().enabled = true;
                Debug.Log("EndScreen enabled status: " + this.GetComponent<SpriteRenderer>().enabled);
                ReplayButton.GetComponent<SpriteRenderer>().enabled = true;
                Debug.Log("EndScreen enabled status: " + ReplayButton.GetComponent<SpriteRenderer>().enabled);
                // ReplayButton.GetComponent<Collider2D>().enabled = true;
                Debug.Log("EndScreen.CheckGameState.Case7");
                break;
            }
            default:
            {
                // Debug.Log("EndScreen.CheckGameState.DefaultCase:" + gameState);
                // this.GetComponent<SpriteRenderer>().enabled = false;
                // ReplayButton.GetComponent<SpriteRenderer>().enabled = false;
                break;
            }
        }
    }   //End of CheckGameState
}
