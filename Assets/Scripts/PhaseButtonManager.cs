using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseButtonManager : MonoBehaviour
{
    public GameObject[] phaseButtonsArray = new GameObject[3];  //Assumes index 0 is Loading, 1 is Aiming, and 2 is Firing
    [SerializeField] private SpriteRenderer[] phaseButtonsSpriteRenderer = new SpriteRenderer[3];

    void OnEnable()
    {
        CannonManager.OnGameStateChange += CheckGameState;
        CannonProperties.OnCheck += SetIntermediateAimingStates;
    }

    void OnDisable()
    {
        CannonManager.OnGameStateChange -= CheckGameState;
        CannonProperties.OnCheck -= SetIntermediateAimingStates;
    }

    void Start()
    {
        phaseButtonsSpriteRenderer[0] = phaseButtonsArray[0].GetComponent<SpriteRenderer>();
        phaseButtonsSpriteRenderer[1] = phaseButtonsArray[1].GetComponent<SpriteRenderer>();
        phaseButtonsSpriteRenderer[2] = phaseButtonsArray[2].GetComponent<SpriteRenderer>();
    }

    void CheckGameState(int gameState)
    {
        switch(gameState)
        {
            case 3:
            {
                Debug.Log("gameState is 3! Setting up buttons for Loading Phase");
                SetButtonPhaseStates(3,0,0);
                break;
            }
            case 4:
            {
                Debug.Log("gameState is 4! Setting up buttons for Aiming Phase");
                SetButtonPhaseStates(2,3,0);
                break;
            }
            case 5:
            {
                Debug.Log("gameState is 5! Setting up buttons for Firing Phase");
                SetButtonPhaseStates(0,0,3);
                break;
            }
            default:
            {
                Debug.Log("gameState is " + gameState + ". Disabling all buttons");
                SetButtonPhaseStates(0,0,0);
                break;
            }
        }
    }   // End of CheckGameState

    void SetButtonPhaseStates(int loadingSpriteID, int aimingSpriteID, int firingSpriteID)
    {
        phaseButtonsSpriteRenderer[0].sprite = GameProperties.GetPhaseSprite("loading", loadingSpriteID); 
        phaseButtonsSpriteRenderer[1].sprite = GameProperties.GetPhaseSprite("aiming", aimingSpriteID);
        phaseButtonsSpriteRenderer[2].sprite = GameProperties.GetPhaseSprite("firing", firingSpriteID);
    }

    void SetIntermediateAimingStates(bool state)
    {
        if(state)
            SetButtonPhaseStates(3,1,0);
        else
            SetButtonPhaseStates(3,0,0);
    }
}
