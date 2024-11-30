using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCharacter : CharacterType
{
    double increaseRate = .10;
    double decreaseRate = .10;
    
    public override void CharacterSpecialFeature(ref int healt, ref int shield,ref int energy,ref int power,ref int toursCount,int damage)
    {
        toursCount++;
        if(toursCount % 2 == 0 )
        {
            double increaseAmount = damage * increaseRate;
            
            GameManager.Instance.character.CharacterTraits_Function("damage","+",(int)increaseAmount);
            
            double decreaseAmount = shield * decreaseRate;
            if(shield == 1)
            {
                GameManager.Instance.character.CharacterTraits_Function("shield","-",1); 
            }
            else
            {
                GameManager.Instance.character.CharacterTraits_Function("shield","-",(int)decreaseAmount);
            }


            increaseRate +=.10;
            decreaseRate += .10;
            toursCount = 0;

        }


    }
}
