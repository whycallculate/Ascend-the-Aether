using System;
using System.Collections;
using System.Collections.Generic;
using Card_Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardObjectCommon_Features 
{
    public abstract class CardObjectCommonFeatures : MonoBehaviour
    {
        protected CardUI cardUI;
        
        #region features of the card

        [SerializeField] private CardTypeEnum cardType;
        public CardTypeEnum CardType { get { return cardType; }  set { cardType = value; } }

        [SerializeField] private CardLegendaryEnum cardLegendary;
        public CardLegendaryEnum CardLegendary { get { return cardLegendary; } set { cardLegendary = value; } }

        [SerializeField] private CardLegendaryEnum[] cardCombineLegendary;
        public CardLegendaryEnum[] CardCombineLegendary { get { return cardCombineLegendary; }  set {cardCombineLegendary = value;}}

        [SerializeField] public int energyCost;
        [SerializeField] public float duration;

        #endregion

    }

}
