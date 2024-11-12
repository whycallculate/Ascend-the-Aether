using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Card_Enum;
using CardObjectCommon_Features;
using TMPro;

public class AttackCardController : CardObjectCommonFeatures
{
    [SerializeField] public int damage;
    [SerializeField] private AttackCardController[] attackCards;
    [SerializeField] private Button cardButton;


    private string cardFolderPath;

    private void Awake() 
    {
        CardLoading();
    }

    private void OnValidate()
    {
        CardLoading();
        //cardUI = GetComponent<CardUI>();

        
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

    

    public void CardInitialize(Sprite cardImage,string cardName,string cardDescription,CardTypeEnum _cardType,CardLegendaryEnum _cardLegendary,int _energyCost,float _duration,CardLegendaryEnum[] _cardCombineLegendary,int _defence)
    {
        //cardUI.CardUIDescription(cardImage,cardName,cardDescription);
        CardType = _cardType;
        CardLegendary = _cardLegendary;
        energyCost = _energyCost;
        duration = _duration;
        CardCombineLegendary = _cardCombineLegendary;
    }
}
