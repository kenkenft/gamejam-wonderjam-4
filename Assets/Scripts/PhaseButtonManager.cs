using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseButtonManager : MonoBehaviour
{
    public GameObject[] PhaseButtonsArray = new GameObject[3];  //Assumes index 0 is Loading, 1 is Aiming, and 2 is Firing
    [SerializeField] private SpriteRenderer[] _phaseButtonsSpriteRendererArray = new SpriteRenderer[3];
    [SerializeField] private Collider2D[] _phaseButtonsColliderArray = new Collider2D[3];

    private List<int[]> _buttonStateCombinArray = new List<int[]>(){
                                                new int[] {3,0,0},  //Loading phase button configuration 
                                                new int[] {2,3,0},  //Aiming phase button configuration
                                                new int[] {0,0,3},  //Firing phase button configuration
                                                new int[] {0,0,0},  //Other phases button configuration
                                                new int[] {3,1,0},  //Loading-Aiming intermediate button configuration
                                                new int[] {2,3,1}   //Aiming-Firing intermediate button configuration
                                            };

    void OnEnable()
    {
        CannonManager.OnGameStateChange += CheckGameState;
        CannonProperties.OnCheck += SetIntermediateStates;
    }

    void OnDisable()
    {
        CannonManager.OnGameStateChange -= CheckGameState;
        CannonProperties.OnCheck -= SetIntermediateStates;
    }

    void Start()
    {
        for(int i = 0; i < PhaseButtonsArray.Length; i++)
        {
            _phaseButtonsSpriteRendererArray[i] = PhaseButtonsArray[i].GetComponent<SpriteRenderer>();
            _phaseButtonsColliderArray[i] = PhaseButtonsArray[i].GetComponent<Collider2D>();
        }
        
    }

    void CheckGameState(int gameState)
    {
        switch(gameState)
        {
            case 3:
            {
                Debug.Log("PhaseButtonManager.Case3");
                SetButtonPhaseStates(_buttonStateCombinArray[0]);
                break;
            }
            case 4:
            {
                Debug.Log("PhaseButtonManager.Case4");
                SetButtonPhaseStates(_buttonStateCombinArray[1]);
                break;
            }
            case 5:
            {
                Debug.Log("PhaseButtonManager.Case5");
                SetButtonPhaseStates(_buttonStateCombinArray[2]);
                break;
            }
            default:
            {
                Debug.Log("PhaseButtonManager.CaseDefault" + gameState);
                SetButtonPhaseStates(_buttonStateCombinArray[3]);
                break;
            }
        }
    }   // End of CheckGameState

    void SetButtonPhaseStates(int[] spriteIDArray)
    {
        for(int i = 0; i < spriteIDArray.Length; i++)
        {
            _phaseButtonsSpriteRendererArray[i].sprite = GameProperties.GetPhaseSprite(i, spriteIDArray[i]); 
            _phaseButtonsColliderArray[i].enabled = (spriteIDArray[i] == 1 || spriteIDArray[i] == 2);
        }
    }

    void SetIntermediateStates(int buttonsState)
    {
        Debug.Log("SetIntermediateStates called. State: " + buttonsState);
        SetButtonPhaseStates(_buttonStateCombinArray[buttonsState]);
    }
}
