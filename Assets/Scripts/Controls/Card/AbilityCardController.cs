using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Card_Enum;
using CardObjectCommon_Features;
using TMPro;

public class AbilityCardController : CardObjectCommonFeatures
{
    
    [SerializeField] public int ability;
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
        cardUI = GetComponent<CardUI>();
       
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

   
   
    
   
    public void CardInitialize(Sprite cardImage,string cardName,string cardDescription,CardTypeEnum _cardType,CardLegendaryEnum _cardLegendary,int _energyCost,float _duration,CardLegendaryEnum[] _cardCombineLegendary,int _defence)
    {
        cardUI.CardUIDescription(cardImage,cardName,cardDescription);
        CardType = _cardType;
        CardLegendary = _cardLegendary;
        energyCost = _energyCost;
        duration = _duration;
        CardCombineLegendary = _cardCombineLegendary;
    }
}
