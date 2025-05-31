using System.Collections;
using System.Collections.Generic;
using CardObjectCommon_Features;
using UnityEngine;
using System;
using CardAnimationPositions;

public class CardAnimationController : MonoBehaviour
{
    [SerializeField] private CardAnimationControl[] cardAnimations;
    public CardAnimationControl[] CardAnimations => cardAnimations;
    [SerializeField] private CardAnimationPositionController cardAnimationPositionController;
    public CardAnimationPositionController CardAnimationPositionControllers => cardAnimationPositionController;



    public void SetAnimationCardsToList(List<GameObject> cards)
    {
        cardAnimations = new CardAnimationControl[cards.Count];
        for (int i = 0; i < cardAnimations.Length; i++)
        {
            cardAnimations[i] = cards[i].GetComponent<CardAnimationControl>();
        }


    }

    //Kart'ların hareket animayou yapmasını sağlayan bir method

    public void CardMovementAnimationControlFunction()
    {
        StartCoroutine(CardMovementInumerator());
    }
    private IEnumerator CardMovementInumerator()
    {
        int index = 0;
        while (index < cardAnimations.Length)
        {
            cardAnimations[index].SetCardPositionParent(cardAnimationPositionController.CardPositionDates[index].cardPositionParent);

            //transform.SetParent(cardAnimationPositionController.CardPositionDates[index].cardPositionParent);
            cardAnimations[index].CarMovementAnimation(cardAnimationPositionController.CardPositionDates[index]);

            yield return new WaitUntil(() => cardAnimations[index].IsDeckAnimation);
            index++;
        }
    }



    private void CardReturnMovementAnimationControlFunction(CardAnimationControl card, int positionIndex, float rotationValue)
    {
        StartCoroutine(CardReturnMovementInumerator());
    }

    private IEnumerator CardReturnMovementInumerator()
    {
        int index = 0;
        while (index < cardAnimations.Length)
        {
            cardAnimations[index].SetCardPositionParent(cardAnimationPositionController.CardPositionDates[index].cardPositionParent);
            cardAnimations[index].CarMovementAnimation(cardAnimationPositionController.CardPositionDates[index]);

            yield return new WaitUntil(() => cardAnimations[index].IsDeckAnimation);
            index++;
        }
    }

    public void CardMovementAnimationClose(GameObject card)
    {
        card.GetComponent<CardAnimationControl>().IsCardMovementStartAnimation = false;

    }


}

[Serializable]
public class CardAnimationPosition
{
    public string name;
    public GameObject parent;
    public Vector3[] position;

}
