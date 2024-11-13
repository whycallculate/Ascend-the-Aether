using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Card_Enum;
using CardObjectCommon_Features;
public class StrengthCardController : CardObjectCommonFeatures
{

    [SerializeField] public int strength;
    [SerializeField] private StrengthCardController[] strengthCards;
    
    [SerializeField] private Button cardButton;
    

    private string cardFolderPath;

    private void Awake() 
    {
        CardLoading();
        cardMovement = GetComponent<CardMovement>();
        if(cardMovement != null)
        {
            cardMovement.CombineCardLengendary = CardCombineLegendary;
            cardMovement.CardType = CardType;

        }

    }

    
    
    private void CardLoading()
    {
        try
        {
            cardFolderPath = "Prefabs/Cards/StrengthCards";
            strengthCards = Resources.LoadAll<StrengthCardController>(cardFolderPath);
        }
        catch (Exception)
        {
            
            throw new Exception();
        }
    }
   

    
   
    public void CardInitialize(Sprite cardImage,string cardName,string cardDescription,CardTypeEnum _cardType,CardLegendaryEnum _cardLegendary,int _energyCost,float _duration,CardLegendaryEnum[] _cardCombineLegendary,int _defence)
    {
        cardUI = GetComponent<CardUI>();
        cardUI.CardUIDescription(cardImage,cardName,cardDescription);
        CardType = _cardType;
        CardLegendary = _cardLegendary;
        energyCost = _energyCost;
        duration = _duration;
        CardCombineLegendary = _cardCombineLegendary;

    }
}
