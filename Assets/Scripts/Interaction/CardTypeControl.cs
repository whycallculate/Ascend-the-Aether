using System;
using System.Collections;
using System.Collections.Generic;
using CardObjectCommon_Features;
using UnityEngine;

public class CardTypeControl
{
    /// <summary>
    /// Serilen paremetre de ki objenin AttackCardController component'i çekmemizi sağliyor.
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public AttackCardController CardTypeAttack(CardObjectCommonFeatures card)
    {
        AttackCardController attackCardController = card as AttackCardController;
        return attackCardController;
    }


    /// <summary>
    /// Serilen paremetre de ki objenin DefenceCardController component'i çekmemizi sağliyor.
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public DefenceCardController CardTypeDefense(CardObjectCommonFeatures card)
    {
        DefenceCardController defenceCardController = card as DefenceCardController;
        return defenceCardController;
    }

    /// <summary>
    /// Serilen paremetre de ki objenin AbilityCardController component'i çekmemizi sağliyor.
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public AbilityCardController CardTypeAbility(CardObjectCommonFeatures card)
    {
        AbilityCardController abilityCardController = card as AbilityCardController;
        return abilityCardController;
    }

    /// <summary>
    /// Serilen paremetre de ki objenin StrengthCardController component'i çekmemizi sağliyor.
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public StrengthCardController CardTypeStrengh(CardObjectCommonFeatures card)
    {
        StrengthCardController strengthCardController= card as StrengthCardController;
        return strengthCardController;
    }

    /// <summary>
    /// Paremetre ile verilen objenin hangi tür kart olduğunu bulup geri döndürmesini sağliyor.
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public Tuple<AttackCardController,DefenceCardController,AbilityCardController,StrengthCardController> FindCardType(GameObject card)
    {
        if(card == null) 
        {
            return null;
        }
        
        CardObjectCommonFeatures cardObjectCommonFeatures = card.GetComponent<CardObjectCommonFeatures>();
        AttackCardController attackCardController = CardTypeAttack(cardObjectCommonFeatures);
        DefenceCardController defenceCardController = CardTypeDefense(cardObjectCommonFeatures);
        AbilityCardController abilityCardController = CardTypeAbility(cardObjectCommonFeatures);
        StrengthCardController strengthCardController = CardTypeStrengh(cardObjectCommonFeatures);

        return Tuple.Create(attackCardController,defenceCardController,abilityCardController,strengthCardController);
    }
   
}

[Serializable]
public enum _CardTypeEnum
{
    Attack,Defence,Ability,Strengh
}
