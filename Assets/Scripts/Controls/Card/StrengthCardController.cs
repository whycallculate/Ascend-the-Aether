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
        if(gameObject.tag != "StrenghCard")
        {
            gameObject.tag = "StrenghCard";
        }
        rectTransform = GetComponent<RectTransform>();


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
   

    
   
    public void CardInitialize(Sprite cardImage,string cardName,string cardDescription,CardTypeEnum _cardType,CardLegendaryEnum _cardLegendary,int _energyCost,float _duration,CardLegendaryEnum[] _cardCombineLegendary,int _strength)
    {
        cardUI = GetComponent<CardUI>();
        cardUI.CardUIInitialize(cardImage,cardName,cardDescription,_energyCost);
        CardType = _cardType;
        CardLegendary = _cardLegendary;
        energyCost = _energyCost;
        duration = _duration;
        CardCombineLegendary = _cardCombineLegendary;
        strength = _strength;
    }

    public void CardUpgradeInitialize(int cardEnergy,int cardStrengh)
    {
        if(cardUI != null)
        {
            cardUI.CardUpgradeInitialize(cardEnergy);
        }
        else
        {
            cardUI = GetComponent<CardUI>();
            cardUI.CardUpgradeInitialize(cardEnergy);
        }
        energyCost = cardEnergy;
        strength = cardStrengh;
    }

}
