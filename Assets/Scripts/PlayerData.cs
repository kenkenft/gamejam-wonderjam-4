using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    // private int[] _cannonBallPoolSmall = new int[8], _cannonBallPoolMedium = new int[6], 
    //                     _cannonBallPoolLarge = new int[4], _cannonBallPoolXLarge = new int[2];  // Array values represent the CannonBall type's id

    private Dictionary<string, string[]> _poolDictionary = new Dictionary<string, string[]>()
                                                                                        {
                                                                                            {"s", new string[8]},
                                                                                            {"m", new string[6]},
                                                                                            {"l", new string[4]},
                                                                                            {"x", new string[2]}
                                                                                        };
    private int _playerMoney, _playerScore;

    public void ResetPlayerData()
    {
        _playerMoney = 0;
        // SetPoolToDefault(_cannonBallPoolSmall);
        // SetPoolToDefault(_cannonBallPoolMedium);
        // SetPoolToDefault(_cannonBallPoolLarge);
        // SetPoolToDefault(_cannonBallPoolXLarge);
        SetPoolToDefault(_poolDictionary["s"], "s");
        SetPoolToDefault(_poolDictionary["m"], "m");
        SetPoolToDefault(_poolDictionary["l"], "l");
        SetPoolToDefault(_poolDictionary["x"], "x");
    }

    void SetPoolToDefault(string[] pool, string typePrefix)
    {
        for(int i = 0; i < pool.Length; i++)
            pool[i] = typePrefix + 0.ToString();
    }

    public void SetPlayerMoney(int amount)
    {
        _playerMoney += amount;
    }

    public int GetPlayerMoney()
    {
        return _playerMoney;
    }

    public string[] GetCannonBallPool(string target)
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
        _poolDictionary[target][arrayIndex] = target + cannonBallTypeID.ToString();
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
