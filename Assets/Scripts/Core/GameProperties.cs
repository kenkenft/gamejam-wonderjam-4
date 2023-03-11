using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProperties
{
    public static Dictionary<string, CannonBall[]> CannonBallTypeDict = new Dictionary<string, CannonBall[]>()
                                                                                                                {
                                                                                                                    {"s", Resources.LoadAll<CannonBall>("Small")},
                                                                                                                    {"m", Resources.LoadAll<CannonBall>("Medium")},
                                                                                                                    {"l", Resources.LoadAll<CannonBall>("Large")},
                                                                                                                    {"x", Resources.LoadAll<CannonBall>("XLarge")}
                                                                                                                };
    public void OnEnable()
    {
        
        CannonBallTypeDict.Add("s", Resources.LoadAll<CannonBall>("Small"));
        CannonBallTypeDict.Add("m", Resources.LoadAll<CannonBall>("Medium"));
        CannonBallTypeDict.Add("l", Resources.LoadAll<CannonBall>("Large"));
        CannonBallTypeDict.Add("x", Resources.LoadAll<CannonBall>("XLarge"));

        // Debug.Log("Small pool type size: " + _cannonBallTypeDict["s"].Length);
        // Debug.Log("Medium pool type size: " + _cannonBallTypeDict["m"].Length);
        // Debug.Log("Large pool type size: " + _cannonBallTypeDict["l"].Length);
        // Debug.Log("XLarge pool type size: " + _cannonBallTypeDict["x"].Length);
        
    }
    
    public static CannonBall GetCannonBallType(string size, int typeIDNum)
    {
        return CannonBallTypeDict[size][typeIDNum];
    }
}
