using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameProperties", menuName = "Game Properties")]
public class GameProperties : ScriptableObject
{
    // private CannonBall[] _cannonBallTypesSmall = Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/Small");   //Assummes index 0 is the small plain cannonball
    // private CannonBall[] _cannonBallTypesMedium = Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/Medium");   //Assummes index 0 is the medium plain cannonball
    // private CannonBall[] _cannonBallTypesLarge = Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/Large");   //Assummes index 0 is the large plain cannonball

    // private CannonBall[] _cannonBallTypesXLarge = Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/XLarge");   

    // private Dictionary<string, CannonBall[]> _cannonBallTypeDict = new Dictionary<string, CannonBall[]>()
    //                                                                                     {
    //                                                                                         {"s", Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/Small")},
    //                                                                                         {"m", Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/Medium")},
    //                                                                                         {"l", Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/Large")},
    //                                                                                         {"x", Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/XLarge")}
    //                                                                                     };
    [SerializeField] private Dictionary<string, CannonBall[]> _cannonBallTypeDict = new Dictionary<string, CannonBall[]>(){};
    public void OnEnable()
    {
        
        _cannonBallTypeDict.Add("s", Resources.LoadAll<CannonBall>("Small"));
        _cannonBallTypeDict.Add("m", Resources.LoadAll<CannonBall>("Medium"));
        _cannonBallTypeDict.Add("l", Resources.LoadAll<CannonBall>("Large"));
        _cannonBallTypeDict.Add("x", Resources.LoadAll<CannonBall>("XLarge"));

        // Debug.Log("Small pool type size: " + _cannonBallTypeDict["s"].Length);
        // Debug.Log("Medium pool type size: " + _cannonBallTypeDict["m"].Length);
        // Debug.Log("Large pool type size: " + _cannonBallTypeDict["l"].Length);
        // Debug.Log("XLarge pool type size: " + _cannonBallTypeDict["x"].Length);
        
    }
    
    public CannonBall GetCannonBallType(string size, int typeIDNum)
    {
        return _cannonBallTypeDict[size][typeIDNum];
    }
}
