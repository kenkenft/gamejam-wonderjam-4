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

    private static Dictionary<string, Enemy[]> _enemyTypeDict = new Dictionary<string, Enemy[]>()
                                                                                                {
                                                                                                    {"s", Resources.LoadAll<Enemy>("Small")},
                                                                                                    {"m", Resources.LoadAll<Enemy>("Medium")},
                                                                                                    {"l", Resources.LoadAll<Enemy>("Large")},
                                                                                                    {"x", Resources.LoadAll<Enemy>("XLarge")},
                                                                                                    {"b", Resources.LoadAll<Enemy>("Boss")}
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
    
    private static Dictionary<int, string[]> _enemySpawnPools = new Dictionary<int, string[]>()
                                                                                                                {
                                                                                                                    {0, new string[] {"s0", "s0", "m0", "m0", "l0", "l0"}}
                                                                                                                };
    
    private static int[] _enemyGridDimensions = new int[] {6, 8};
    public static CannonBall GetCannonBallType(string size, int typeIDNum)
    {
        return _cannonBallTypeDict[size][typeIDNum];
    }

    public static Enemy GetEnemyType(string size, int typeIDNum)
    {
        // Debug.Log("Enemy size: " + size  + ". typeIDNum: " + typeIDNum);
        // Debug.Log("Dictionary size: " + _enemyTypeDict.Count + ". Type array size: " + _enemyTypeDict[size].Length);
        return _enemyTypeDict[size][typeIDNum];
    }

    public static Sprite GetPhaseSprite(int phaseNameID, int spriteID)
    {
        return _phaseSpritesDict[phaseNameID][spriteID];
    }

    public static int GetPhaseID(string phaseName)
    {
        return _phaseNameIDsDict[phaseName];
    }

    public static string[] GetEnemyStageSpawnPool(int stageID)
    {
        return _enemySpawnPools[stageID];
    }

    public static int[] GetEnemyGridDimensions()
    {
        return _enemyGridDimensions;
    }
}
