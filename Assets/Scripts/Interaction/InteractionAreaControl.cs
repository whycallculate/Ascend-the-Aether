using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionAreaControl : MonoBehaviour,IDropHandler
{
    [SerializeField]private CardAnimationController cardAnimationController;
    private CardTypeControl cardTypeControl;
    private CardComboController cardComboController;
    private CardInteractionControl cardInteractionControl;

    void Awake()
    {

        if (cardTypeControl == null)
            cardTypeControl = new CardTypeControl();

        if (cardComboController == null)
            cardComboController = new CardComboController();

        if (cardInteractionControl == null)
            cardInteractionControl = new CardInteractionControl();

        if (cardAnimationController == null) cardAnimationController = GameObject.FindWithTag("CardAnimationController").GetComponent<CardAnimationController>();
    
    }


    public void OnDrop(PointerEventData eventData)
    {
        var tuple = cardTypeControl.FindCardType(eventData.pointerDrag);
        eventData.pointerDrag.transform.SetParent(transform);
        TupleValueControl(tuple);
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        UIManager.Instance.GraphicRaycaster.Raycast(eventData, raycastResults);


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
                    cardComboController.AttackCardCombo(cardAnimationController.CardAnimationPositionControllers.CardReturnPositionDates[0],tuple.Item1.GetComponent<CardAnimationControl>());
                } 
                else
                {
                    cardInteractionControl.AttackCardInteraction(cardAnimationController.CardAnimationPositionControllers.CardReturnPositionDates[0],tuple.Item1.GetComponent<CardAnimationControl>());
                }

                return value;
            }
            else if(tuple.Item2 !=null)
            {
                value =tuple.Item2.CardCombineLegendary.Length > 0 ? true : false;
                
                if(value)
                {
                    cardComboController.DefenceCardCombo(cardAnimationController.CardAnimationPositionControllers.CardReturnPositionDates[0],tuple.Item2.GetComponent<CardAnimationControl>());
                } 
                else
                {
                    cardInteractionControl.DefenceCardInteraction(cardAnimationController.CardAnimationPositionControllers.CardReturnPositionDates[0],tuple.Item2.GetComponent<CardAnimationControl>());
                }
                return value;
            }
            else if(tuple.Item3 !=null)
            {
                value =tuple.Item3.CardCombineLegendary.Length > 0 ? true : false;
                
                if(value)
                {
                    cardComboController.AbilityCardCombo(cardAnimationController.CardAnimationPositionControllers.CardReturnPositionDates[0],tuple.Item3.GetComponent<CardAnimationControl>());
                } 
                else
                {
                    cardInteractionControl.AbilityCardInteraction(cardAnimationController.CardAnimationPositionControllers.CardReturnPositionDates[0],tuple.Item3.GetComponent<CardAnimationControl>());
                }
                return value;
            }
            else if(tuple.Item4 != null)
            {
                value =tuple.Item4.CardCombineLegendary.Length > 0 ? true : false;
                
                if(value)
                {
                    cardComboController.StrenghCardCombo(cardAnimationController.CardAnimationPositionControllers.CardReturnPositionDates[0],tuple.Item4.GetComponent<CardAnimationControl>());
                } 
                else
                {
                    cardInteractionControl.StrenghCardInteraction(cardAnimationController.CardAnimationPositionControllers.CardReturnPositionDates[0],tuple.Item4.GetComponent<CardAnimationControl>());
                }
                return value;
            }
            return false;
        }

        return false;

    }
    
}
