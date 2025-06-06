using System;
using System.Collections;
using System.Collections.Generic;
using Card_Enum;
using UnityEngine;
using UnityEngine.UI;

namespace CardObjectCommon_Features 
{
    public abstract class CardObjectCommonFeatures : MonoBehaviour
    {
        [HideInInspector] public CardUI cardUI;
        public CardMovement cardMovement;

        #region features of the card

        [SerializeField] private CardTypeEnum cardType;
        public CardTypeEnum CardType { get { return cardType; } set { cardType = value; } }

        [SerializeField] private CardLegendaryEnum cardLegendary;
        public CardLegendaryEnum CardLegendary { get { return cardLegendary; } set { cardLegendary = value; } }

        [SerializeField] private CardLegendaryEnum[] cardCombineLegendary;
        public CardLegendaryEnum[] CardCombineLegendary { get { return cardCombineLegendary; } set { cardCombineLegendary = value; } }

        protected RectTransform rectTransform;

        public Transform cardAnimationPositionParent;

        public Vector2 cardPosition;

        [SerializeField] private DeckItemPosition deckPosition;
        public DeckItemPosition DeckPosition { get { return deckPosition; } set { deckPosition = value; } }

    
        protected Image cardImage;

        public int energyCost;
        public float duration;


        #endregion

        
    
    }

}
