using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Card_Enum;
using CardObjectCommon_Features;
public class AbilityCardController : CardObjectCommonFeatures
{
    #region features of the card
    [SerializeField] private CardTypeEnum cardType;
    [SerializeField] private CardLegendaryEnum cardLegendary;
    public CardLegendaryEnum CardLegendary { get { return cardLegendary; } }
    [SerializeField] private CardLegendaryEnum[] cardCombineLegendary;
    [SerializeField] private int energyCost;
    [SerializeField] private float duration;
    [SerializeField] private int ability;
    #endregion
    
    [SerializeField] private AbilityCardController[] abilityCards;
    [SerializeField] private Button cardButton;
    
    private string cardFolderPath;

    private void Awake() 
    {
        CardLoading();
    }

    private void OnValidate() 
    {
        
        CardLoading();
        cardButton = GetComponent<Button>();    
        cardButton.onClick.AddListener(Card_Move);
    }

    private void CardLoading()
    {
        try
        {
            cardFolderPath = "Prefabs/Cards/AbilityCards";
            abilityCards = Resources.LoadAll<AbilityCardController>(cardFolderPath);
        }
        catch (Exception)
        {
            
            throw new Exception();
        }
    }

   
    public void Card_Move()
    {
        CardObject_Movement(gameObject,"energy","-",energyCost);
    }
    
   
    public void CardInitialize(CardTypeEnum _cardType,CardLegendaryEnum _cardLegendary,int _energyCost,float _duration,CardLegendaryEnum[] _cardCombineLegendary,int _ability)
    {
        cardType = _cardType;
        cardLegendary = _cardLegendary;
        energyCost = _energyCost;
        duration = _duration;
        cardCombineLegendary = _cardCombineLegendary;
        ability = _ability;
    }

}
