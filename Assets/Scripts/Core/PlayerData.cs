using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{

    [SerializeField] private Dictionary<string, string[]> _poolDictionary = new Dictionary<string, string[]>()
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
        SetPoolToDefault(_poolDictionary["s"], "s");
        SetPoolToDefault(_poolDictionary["m"], "m");
        SetPoolToDefault(_poolDictionary["l"], "l");
        SetPoolToDefault(_poolDictionary["x"], "x");
    }

    void SetPoolToDefault(string[] pool, string typePrefix)
    {
        for(int i = 0; i < pool.Length; i++)
        {    
            if(i % 2 == 0)
                pool[i] = typePrefix + 0.ToString();
            else
                pool[i] = typePrefix + 1.ToString();
        }
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
        ResetPlayerData();
        return _poolDictionary[target];
    }

    public void SetCannonBallPool(string target, int arrayIndex, int cannonBallTypeID)
    {
        _poolDictionary[target][arrayIndex] = target + cannonBallTypeID.ToString();
    }
}
