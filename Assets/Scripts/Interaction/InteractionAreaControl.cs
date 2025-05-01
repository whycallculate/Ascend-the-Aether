using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionAreaControl : MonoBehaviour,IDropHandler
{
    private CardTypeControl cardTypeControl;
    private CardComboController cardComboController;
    private CardInteractionControl cardInteractionControl;

    void Awake()
    {

        if (cardTypeControl == null)
            cardTypeControl = new CardTypeControl();

        if(cardComboController == null)
            cardComboController = new CardComboController();

        if(cardInteractionControl == null)
            cardInteractionControl = new CardInteractionControl();
    
    }


    public void OnDrop(PointerEventData eventData)
    {
        var tuple = cardTypeControl.FindCardType(eventData.pointerDrag);
        
        TupleValueControl(tuple);


    }

    private bool TupleValueControl(Tuple<AttackCardController,DefenceCardController,AbilityCardController,StrengthCardController> tuple)
    {
        if(tuple != null)
        {
            bool value = false;
            if(tuple.Item1 !=null)
            {
                value =tuple.Item1.CardCombineLegendary.Length > 0 ? true : false;
                
                if(value)
                {
                    cardComboController.AttackCardCombo();
                } 
                else
                {
                    cardInteractionControl.AttackCardInteraction();
                }

                return value;
            }
            else if(tuple.Item2 !=null)
            {
                value =tuple.Item2.CardCombineLegendary.Length > 0 ? true : false;
                
                if(value)
                {
                    cardComboController.DefenceCardCombo();
                } 
                else
                {
                    cardInteractionControl.DefenceCardInteraction();
                }
                return value;
            }
            else if(tuple.Item3 !=null)
            {
                value =tuple.Item3.CardCombineLegendary.Length > 0 ? true : false;
                
                if(value)
                {
                    cardComboController.AbilityCardCombo();
                } 
                else
                {
                    cardInteractionControl.AbilityCardInteraction();
                }
                return value;
            }
            else if(tuple.Item4 != null)
            {
                value =tuple.Item4.CardCombineLegendary.Length > 0 ? true : false;
                
                if(value)
                {
                    cardComboController.StrenghCardCombo();
                } 
                else
                {
                    cardInteractionControl.StrenghCardInteraction();
                }
                return value;
            }
            return false;
        }

        return false;

    }
    
}
