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

    private string seasonTicketType;
    private string combineCardType;


    //bir kart ile başka bir kartı kombine yapınca karşı düşmana veya karaktere etki etmesini sağliyor
    public void CardComboFunction(CharacterController character,string[] characterFeatures,string[] characterFeatureTransactions,int[] characterFeatureValues, EnemyController enemy,string enemyFeature,int enemyFeatureValue,string enemyFeatureTransaction,GameObject seasonTicket,GameObject combineCard)
    {
        for (int i = 0; i < characterFeatures.Length; i++)
        {
            GameManager.Instance.CardCharacterInteraction(characterFeatures[i],characterFeatureTransactions[i],characterFeatureValues[i]);
        }
        CardTypeInteraction(seasonTicket,combineCard);
    }

    private void CardTypeInteraction(GameObject seasonTicket,GameObject combineCard)
    {
        TagEnquiry(seasonTicket.tag,ref seasonTicketType);
        TagEnquiry(combineCard.tag,ref combineCardType);

        switch(combineCardType,seasonTicketType)
        {
            case ("AttackCard","AttackCard"):
                print("attack attack");
            break;
            case ("AttackCard","DefenceCard"):
                print("attack defence");
            break;
            case ("AttackCard","AbilityCard"):
                print("attack ability");
            break;
            case ("AttackCard","StrenghCard"):
                print("attack strengh");
            break;

            case ("DefenceCard","AttackCard"):
                print("defence attack");
            break;
            case ("DefenceCard","DefenceCard"):
                print("defence defence");
            break;
            case ("DefenceCard","AbilityCard"):
                print("defence ability");
            break;
            case ("DefenceCard","StrenghCard"):
                print("defence strengh");
            break;

            case ("AbilityCard","AttackCard"):
                print("ability attack");
            break;
            case ("AbilityCard","DefenceCard"):
                print("ability defence");
            break;
            case ("AbilityCard","AbilityCard"):
                print("ability ability");
            break;
            case ("AbilityCard","StrenghCard"):
                print("ability strengh");
            break;

            case ("StrenghCard","AttackCard"):
                print("strengh attack");
            break;
            case ("StrenghCard","DefenceCard"):
                print("strengh defence");
            break;
            case ("StrenghCard","AbilityCard"):
                print("strengh ability");
            break;
            case ("StrenghCard","StrenghCard"):
                print("strengh strengh");
            break;

        }

    }

    private void TagEnquiry(string seasonTicketTag,ref string type)
    {
        switch (seasonTicketTag)
        {
            case "AttackCard":
                type = "AttackCard";
                break;
            case "DefenceCard":
                type = "DefenceCard";
                break;
            case "AbilityCard":
                type = "AbilityCard";
                break;
            case "StrenghCard":
                type = "StrenghCard";
                break;
            default:
                break;

        }
    }

    private void AttackInteraction(string combineCardType)
    {
        switch(combineCardType)
        {
            case "AttackCard":
            break;
            case "DefenceCard":
            break;
            case "AbilityCard":
            break;
            case "StrenghCard":
            break;
            default:
            break;
        }
    }

}
