using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonBallDisplay : MonoBehaviour
{
    public CannonBall cb;

    public Text nameText, descriptionText;

    public Image portrait;
    public SpriteRenderer spriteRenderer;
    public int capacitySize, damage, areaOfEffect, recoveryTime;

    void Start()
    {
        if(cb != null)
        {
            Debug.Log(cb.name);
            // nameText.text = cb.name;
            // descriptionText.text = cb.description;
            // portrait = cb.artwork;
            spriteRenderer.sprite = cb.sprite;
            capacitySize = cb.capacitySize;
            damage = cb.damage;
            areaOfEffect = cb.areaOfEffect;
            recoveryTime = cb.recoveryTime;
        }
    }
}
