using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProperties
{
    private static Dictionary<string, CannonBall[]> CannonBallTypeDict = new Dictionary<string, CannonBall[]>()
                                                                                                                {
                                                                                                                    {"s", Resources.LoadAll<CannonBall>("Small")},
                                                                                                                    {"m", Resources.LoadAll<CannonBall>("Medium")},
                                                                                                                    {"l", Resources.LoadAll<CannonBall>("Large")},
                                                                                                                    {"x", Resources.LoadAll<CannonBall>("XLarge")}
                                                                                                                };
    
    public static CannonBall GetCannonBallType(string size, int typeIDNum)
    {
        return CannonBallTypeDict[size][typeIDNum];
    }
}
