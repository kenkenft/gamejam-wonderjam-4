using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonBallDisplay : MonoBehaviour
{
    public CannonBall Cb;

    public Text NameText, DescriptionText;

    public Image Portrait;
    public SpriteRenderer SpriteRenderer;
    public int CapacitySize, Damage, AreaOfEffect, RecoveryTime;

    void Start()
    {
        if(Cb != null)
        {
            Debug.Log(Cb.Name);
            // nameText.text = cb.name;
            // descriptionText.text = cb.description;
            // portrait = cb.artwork;
            SpriteRenderer.sprite = Cb.Sprite;
            CapacitySize = Cb.CapacitySize;
            Damage = Cb.Damage;
            AreaOfEffect = Cb.AreaOfEffect;
            RecoveryTime = Cb.RecoveryTime;
        }
    }
}
