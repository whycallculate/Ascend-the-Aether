using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Card_Enum;
using CardObjectCommon_Features;

public class AttackCardController : CardObjectCommonFeatures
{
    #region features of the card
    [SerializeField] private CardTypeEnum cardType;
    public CardTypeEnum CardType { get { return cardType; } }

    [SerializeField] private CardLegendaryEnum cardLegendary;
    public CardLegendaryEnum CardLegendary { get { return cardLegendary; }  set { cardLegendary = value; } }
    
    [SerializeField] private CardLegendaryEnum[] cardCombineLegendary;
    public CardLegendaryEnum[] CardCombineLegendary { get { return cardCombineLegendary;}}
    
    [SerializeField] private int energyCost;
    [SerializeField] private float duration;
    [SerializeField] private int damage;
    #endregion
    
    [SerializeField] private AttackCardController[] attackCards;
    [SerializeField] private Button cardButton;

    private string cardFolderPath;

    private void Awake() 
    {
        CardLoading();

        if(cardButton.onClick == null)
        {
            cardButton.onClick.AddListener(Card_Move);
        }

    }

    private void OnValidate()
    {
        CardLoading();

        cardButton = GetComponent<Button>();
        cardButton.onClick.AddListener(Card_Move);
    }

    // Card object allows retrieval from the Sources Folder ; allows : izin verir, retrieval :geri alma
    private void CardLoading()
    {
        try
        {
            cardFolderPath = "Prefabs/Cards/AttackCards";
            attackCards = Resources.LoadAll<AttackCardController>(cardFolderPath);
        }
        catch (Exception)
        {
            
            throw new Exception();
        }
    }

    //the card do movement 
    public void Card_Move()
    {
        CardObject_Movement(gameObject,"energy","-",energyCost);
    }
    

    public void CardInitialize(CardTypeEnum _cardType,CardLegendaryEnum _cardLegendary,int _energyCost,float _duration,CardLegendaryEnum[] _cardCombineLegendary,int _damage)
    {
        cardType = _cardType;
        cardLegendary = _cardLegendary;
        energyCost = _energyCost;
        duration = _duration;
        cardCombineLegendary = _cardCombineLegendary;
        damage = _damage;
    }
}
