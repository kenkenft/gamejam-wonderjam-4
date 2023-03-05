using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite artwork;
    public int capacitySize;
    public int health;
    public int damage;
    public int travelDistance;
    public int travelFrequency;
    public bool isImmuneToOneHit;
}
