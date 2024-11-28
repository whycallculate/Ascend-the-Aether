using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorcererCharacter : CharacterType
{
    
    public override void CharacterSpecialFeature(ref int healt, ref int shield, ref int energy, ref int power, ref int toursCount)
    {
        if(!featureDisables)
        {
            StartCoroutine(StunEnemies());
        }
        else
        {
            if(GameManager.Instance.IsEnemysStan)
            {
                GameManager.Instance.IsEnemysStan = false;
                return;
            }  
        }
    }
    private IEnumerator StunEnemies()
    {
        while(!GameManager.Instance.IsLevelOver)
        {
            GameManager.Instance.IsEnemysStan = true;
            yield return null;
            if(featureDisables) break;
        }


        GameManager.Instance.IsEnemysStan = false;
    }
}
