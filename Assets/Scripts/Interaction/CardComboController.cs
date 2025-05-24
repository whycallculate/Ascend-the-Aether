using System.Collections;
using System.Collections.Generic;
using CardAnimationPositions;
using UnityEngine;

public class CardComboController 
{
    public void AttackCardCombo(CardAnimationPositionData cardAnimationPositionData, CardAnimationControl card)
    {
        card.CardReturnMovementAnimation(cardAnimationPositionData);
    }

    public void DefenceCardCombo(CardAnimationPositionData cardAnimationPositionData, CardAnimationControl card)
    {
        card.CardReturnMovementAnimation(cardAnimationPositionData);
    }

    public void AbilityCardCombo(CardAnimationPositionData cardAnimationPositionData, CardAnimationControl card)
    {

        card.CardReturnMovementAnimation(cardAnimationPositionData);
    }

    public void StrenghCardCombo(CardAnimationPositionData cardAnimationPositionData, CardAnimationControl card)
    {
        card.CardReturnMovementAnimation(cardAnimationPositionData);
    }

}
