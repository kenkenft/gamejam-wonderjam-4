using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    // private int[] _cannonBallPoolSmall = new int[8], _cannonBallPoolMedium = new int[6], 
    //                     _cannonBallPoolLarge = new int[4], _cannonBallPoolXLarge = new int[2];  // Array values represent the CannonBall type's id

    private Dictionary<string, int[]> _poolDictionary = new Dictionary<string, int[]>()
                                                                                        {
                                                                                            {"small", new int[8]},
                                                                                            {"medium", new int[6]},
                                                                                            {"large", new int[4]},
                                                                                            {"xlarge", new int[2]}
                                                                                        };
    private int _playerMoney, _playerScore;

    public void ResetPlayerData()
    {
        _playerMoney = 0;
        // SetPoolToDefault(_cannonBallPoolSmall);
        // SetPoolToDefault(_cannonBallPoolMedium);
        // SetPoolToDefault(_cannonBallPoolLarge);
        // SetPoolToDefault(_cannonBallPoolXLarge);
        SetPoolToDefault(_poolDictionary["small"]);
        SetPoolToDefault(_poolDictionary["medium"]);
        SetPoolToDefault(_poolDictionary["large"]);
        SetPoolToDefault(_poolDictionary["xlarge"]);
    }

    void SetPoolToDefault(int[] pool)
    {
        for(int i = 0; i < pool.Length; i++)
            pool[i] = 0;
    }

    public void SetPlayerMoney(int amount)
    {
        _playerMoney += amount;
    }

    public int GetPlayerMoney()
    {
        return _playerMoney;
    }

    public int[] GetCannonBallPool(string target)
    {
        return _poolDictionary[target];
        // switch(target)
        // {
        //     case "small":
        //         return _cannonBallPoolSmall;
        //     case "medium":
        //         return _cannonBallPoolMedium;
        //     case "large":
        //         return _cannonBallPoolLarge;
        //     case "xlarge":
        //         return _cannonBallPoolXLarge;
        //     default:
        //         return _cannonBallPoolMedium;
        // }
    }

    public void SetCannonBallPool(string target, int arrayIndex, int cannonBallTypeID)
    {
        _poolDictionary[target][arrayIndex] = cannonBallTypeID;
        //  switch(target)
        //  {
        //     case "small":
        //     {    
        //         _cannonBallPoolSmall[arrayIndex] = typeID;
        //         break;
        //     }
        //     case "medium":
        //     {    
        //         _cannonBallPoolMedium[arrayIndex] = typeID;
        //         break;
        //     }
        //     case "large":
        //     {    
        //         _cannonBallPoolLarge[arrayIndex] = typeID;
        //         break;
        //     }
        //     case "xlarge":
        //     {    
        //         _cannonBallPoolXLarge[arrayIndex] = typeID;
        //         break;
        //     }
        //     default:
        //         break;
        // }
    }
}
