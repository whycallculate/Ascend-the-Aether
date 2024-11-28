using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCharacter : CharacterType
{
    double increaseRate = .20;
    double decreaseRate = .10;

    public override void CharacterSpecialFeature(ref int healt, ref int shield,ref int energy,ref int power,ref int damage,ref int toursCount)
    {
        toursCount++;
        if(toursCount % 2 == 0)
        {
            double increaseAmount = damage * increaseRate;
            damage +=  (int)increaseAmount;
            
            double decreaseAmount = shield * decreaseRate;
            shield -= (int)decreaseAmount;

            increaseRate +=.20;
            decreaseRate += .10;
            toursCount = 0;
        }


    }
}
