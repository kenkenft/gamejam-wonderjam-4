using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New CannonBall", menuName = "CannonBall")]
public class CannonBall : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Sprite;
    public Image HelpPortrait;
    public int CapacitySize, Damage, AreaOfEffect, RecoveryTime;
}
