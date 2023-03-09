using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New CannonBall", menuName = "CannonBall")]
public class CannonBall : ScriptableObject
{
    public string Name, Description,TypeID;
    public Sprite Sprite;
    public Image HelpPortrait;
    public int CapacitySize, Damage, AreaOfEffect, RecoveryTime;
}
