using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProperties
{
    private static Dictionary<string, CannonBall[]> _cannonBallTypeDict = new Dictionary<string, CannonBall[]>()
                                                                                                                {
                                                                                                                    {"s", Resources.LoadAll<CannonBall>("Small")},
                                                                                                                    {"m", Resources.LoadAll<CannonBall>("Medium")},
                                                                                                                    {"l", Resources.LoadAll<CannonBall>("Large")},
                                                                                                                    {"x", Resources.LoadAll<CannonBall>("XLarge")}
                                                                                                                };
    
    private static Dictionary<int, Sprite[]> _phaseSpritesDict = new Dictionary<int, Sprite[]>()
                                                                                                    {
                                                                                                        {0, Resources.LoadAll<Sprite>("PhaseLoading")}, //"loading"
                                                                                                        {1, Resources.LoadAll<Sprite>("PhaseAiming")},   //"aiming"
                                                                                                        {2, Resources.LoadAll<Sprite>("PhaseFiring")}    //"firing"
                                                                                                    };

    private static Dictionary<string, int> _phaseNameIDsDict = new Dictionary<string, int>()// 0 is on Title Screen; 1 is start or initial setup; 2 is replenishment; 3 is loading; 4 is aiming; 5 is firing; 6 is update cannonballs and enemies; 
                                                                                        {
                                                                                            {"title", 0},
                                                                                            {"setup", 1},
                                                                                            {"replenishment", 2},
                                                                                            {"loading", 3},
                                                                                            {"aiming", 4},
                                                                                            {"firing", 5},
                                                                                            {"enemy", 6}
                                                                                        };
    public static CannonBall GetCannonBallType(string size, int typeIDNum)
    {
        return _cannonBallTypeDict[size][typeIDNum];
    }

    public static Sprite GetPhaseSprite(int phaseNameID, int spriteID)
    {
        return _phaseSpritesDict[phaseNameID][spriteID];
    }

    public static int GetPhaseID(string phaseName)
    {
        return _phaseNameIDsDict[phaseName];
    }
}
