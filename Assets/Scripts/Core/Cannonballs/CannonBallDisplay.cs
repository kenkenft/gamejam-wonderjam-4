using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonBallDisplay : MonoBehaviour
{
    public CannonBall Cb;

    public Text NameText, DescriptionText;

    public Image Portrait;
    [SerializeField] private SpriteRenderer _spriteRendererComp;
    public int CapacitySize, Damage, AreaOfEffect, RecoveryTime;

    void Start()
    {
        _spriteRendererComp = gameObject.GetComponent<SpriteRenderer>();
        // if(Cb != null)
        // {
        //     SetUpDisplay();
        // }
    }
    
    public void SetUpDisplay()
    {
        // Debug.Log(Cb.CannonBallName);
        // nameText.text = cb.name;
        // descriptionText.text = cb.description;
        // portrait = cb.artwork;
        _spriteRendererComp = gameObject.GetComponent<SpriteRenderer>();
        _spriteRendererComp.sprite = Cb.LevelSprite;
        CapacitySize = Cb.CapacitySize;
        Damage = Cb.Damage;
        AreaOfEffect = Cb.AreaOfEffect;
        RecoveryTime = Cb.RecoveryTime;
    }
}
