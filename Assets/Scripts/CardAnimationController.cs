using System.Collections;
using System.Collections.Generic;
using CardObjectCommon_Features;
using UnityEngine;
using DG.Tweening;
using System;

public class CardAnimationController : MonoBehaviour
{
    [SerializeField] List<CardAnimationPosition> vectors = new List<CardAnimationPosition>();
    [SerializeField] List<CardAnimationPosition> returnPosition = new List<CardAnimationPosition>();

    public delegate void CardMovementAnimationDelegate(CardAnimationControl card,int positionIndex,float rotationValue);
    public CardMovementAnimationDelegate cardMovementAnimation => CardMovementAnimationControlFunction;

    public delegate void CardReturnMovementAnimationDelegate(CardAnimationControl card,int positionIndex,float rotationValue);
    public CardReturnMovementAnimationDelegate cardReturnMovementAnimation => CardReturnMovementAnimationControlFunction;


    //Kart'ların hareket animayou yapmasını sağlayan bir method
    
    private void CardMovementAnimationControlFunction(CardAnimationControl card,int positionIndex,float rotationValue)
    {
        card.CardHareketAnimation(card,positionIndex,rotationValue,vectors);
    }

    private void CardReturnMovementAnimationControlFunction(CardAnimationControl card,int positionIndex,float rotationValue)
    {
        card.CardReturnMovementAnimation(card,positionIndex,rotationValue,returnPosition);
    }

}

[Serializable]
public class CardAnimationPosition
{
    public string name;
    public GameObject parent;
    public Vector3[] position;

}
