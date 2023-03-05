using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New CannonBall", menuName = "CannonBall")]
public class CannonBall : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;
    public Image helpPortrait;
    public int capacitySize;
    public int damage;
    public int areaOfEffect;
    public int recoveryTime;
}
