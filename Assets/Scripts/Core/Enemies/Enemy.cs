using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string EnemyName, Description, TypeID;
    public Sprite LevelSprite;
    public Image HelpPortrait;
    public int CapacitySize, Health, Damage, TravelDistance, TravelFrequency, IsImmuneToOneHit;
}
