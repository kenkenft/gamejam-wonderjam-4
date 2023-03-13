using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDisplay : MonoBehaviour
{
    [SerializeField] public Enemy EnemySO;

    public Text NameText, DescriptionText;

    public Image Portrait;
    [SerializeField] private SpriteRenderer _spriteRendererComp;
    public int CapacitySize, Health, Damage, TravelDistance, TravelFrequency, IsImmuneToOneHit;

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
        // Debug.Log(_enemy.EnemyName);
        // nameText.text = _enemy.EnemyName;
        // descriptionText.text = _enemy.Description;
        // portrait = _enemy.HelpPortrait;
        _spriteRendererComp = gameObject.GetComponent<SpriteRenderer>();
        _spriteRendererComp.sprite = EnemySO.LevelSprite;
        CapacitySize = EnemySO.CapacitySize;
        Health = EnemySO.Health;
        Damage = EnemySO.Damage;
        TravelDistance = EnemySO.TravelDistance;
        TravelFrequency = EnemySO.TravelFrequency;
        IsImmuneToOneHit = EnemySO.IsImmuneToOneHit;
    }
}
