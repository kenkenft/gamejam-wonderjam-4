using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New CannonBall", menuName = "CannonBall")]
public class CannonBall : ScriptableObject
{
    public string CannonBallName, Description,TypeID;
    public Sprite LevelSprite;
    public Image HelpPortrait;
    public int CapacitySize, Damage, AreaOfEffect, RecoveryTime;
}
