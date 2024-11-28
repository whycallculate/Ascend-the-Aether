using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCharacter : CharacterType
{
    double increaseRate = .10;
    public override void CharacterSpecialFeature(ref int healt, ref int shield,ref int energy,ref int power,ref int damage,ref int toursCount)
    {
        toursCount++;
        if(toursCount % 2 == 0)
        {
            double subtractValue = damage * increaseRate;
            if ((damage - subtractValue) > 0)
            {
                damage -= (int)subtractValue;
                GameManager.Instance.character.CharacterTraits_Function("shield", "-", damage);
                increaseRate += .10;
                toursCount = 0;
            }
        }
    }
}
