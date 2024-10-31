using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardTypes;


[CreateAssetMenu(fileName ="Card Scriptable Object",menuName = "Create Card Scriptable Object",order = 0)]
public class CardScriptableObject : ScriptableObject
{
    public AttackCard attackCard;
    public DefenceCard defenceCard;
    public AbilityCard abilityCard;

    public StrengthCard strengthCard;

    public CardLegendary cardCombine;

    
}


