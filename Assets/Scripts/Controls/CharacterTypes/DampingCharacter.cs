using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DampingCharacter : CharacterType
{
    double increaseHealtRate = .10;
    double increaseDamageRate = .10;

    public override void CharacterSpecialFeature(ref int healt, ref int shield,ref int energy,ref int power,ref int damage,ref int toursCount)
    {
        toursCount++;
        if(toursCount % 2 == 0)
        {
            double valueAdd = damage * increaseHealtRate;
            if ((healt + valueAdd) < 100)
            {

                GameManager.Instance.character.CharacterTraits_Function("healtbar", "+", (int)valueAdd);

            }
            else if ((healt + valueAdd) >= 100)
            {
                int addValue = 100 - healt;
                GameManager.Instance.character.CharacterTraits_Function("healtbar", "+", addValue);
            }

            double increaseAmount = damage * increaseDamageRate;
            damage += (int)increaseAmount;

            increaseHealtRate += .10;
            increaseDamageRate += .10f;
            toursCount = 0;
        }
        
    }
}
