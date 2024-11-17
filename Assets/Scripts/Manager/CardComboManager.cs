using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardComboManager : MonoBehaviour
{
    private static CardComboManager instance;
    public static CardComboManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CardComboManager>();
            }
            return instance;
        }
    }

    //bir kart ile başka bir kartı kombine yapınca karşı düşmana veya karaktere etki etmesini sağliyor
    public void CardComboFunction(CharacterController character,string[] characterFeatures,string[] characterFeatureTransactions,int[] characterFeatureValues, EnemyController enemy,string enemyFeature,int enemyFeatureValue,string enemyFeatureTransaction)
    {
        for (int i = 0; i < characterFeatures.Length; i++)
        {
            GameManager.Instance.CardCharacterInteraction(characterFeatures[i],characterFeatureTransactions[i],characterFeatureValues[i]);
        }
    }


}
