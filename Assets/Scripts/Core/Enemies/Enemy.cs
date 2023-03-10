using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Artwork;
    public int CapacitySize, Health, Damage, TravelDistance, TravelFrequency, IsImmuneToOneHit;
}
