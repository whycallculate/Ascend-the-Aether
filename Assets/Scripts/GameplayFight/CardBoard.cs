using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBoard : MonoBehaviour,IDropHandler
{
    
    public AbilityCardController abilityCardController;
    public AttackCardController attackCardController;
    public DefenceCardController defenceCardController;
    public StrengthCardController strenghCardController;


    public void OnDrop(PointerEventData eventData)
    {
        if(GameManager.Instance.enemy != null)
        {
            if (eventData.pointerDrag.CompareTag("AbilityCard"))
            {
                UseAbilityCard(eventData);
            }
            else if (eventData.pointerDrag.CompareTag("AttackCard"))
            {
                UseAttackCard(eventData);
            }
            else if (eventData.pointerDrag.CompareTag("DefenceCard"))
            {

                UseDefenceCard(eventData);
            }
            else if (eventData.pointerDrag.CompareTag("StrenghCard"))
            {
                UseStrenghCard(eventData);
            }
        }
        GameManager.Instance.SelectableCard(false);

    }

    public void UseAbilityCard(PointerEventData eventData)
    {
        abilityCardController = eventData.pointerDrag.GetComponent<AbilityCardController>();
        if(GameManager.Instance.character.energyCurrent >= abilityCardController.energyCost)
        {
            GameManager.Instance.CardCharacterInteraction("energy", "-", abilityCardController.energyCost);
            GameManager.Instance.enemy.TakePower(abilityCardController.ability);
            abilityCardController.gameObject.SetActive(false);
            abilityCardController = null;
        }
        else
        {
            abilityCardController = null;
        }


    }
    public void UseAttackCard(PointerEventData eventData)
    {
        attackCardController = eventData.pointerDrag.GetComponent<AttackCardController>();
        if(GameManager.Instance.character.energyCurrent >= attackCardController.energyCost)
        {
            if(GameManager.Instance.enemy.SHIELD <= 0)
            {
                GameManager.Instance.CardCharacterInteraction("energy", "-", attackCardController.energyCost);
                GameManager.Instance.enemy.TakeDamage(attackCardController.damage);
                attackCardController.gameObject.SetActive(false);
                attackCardController = null;
            }
            else if(GameManager.Instance.enemy.SHIELD >= 0)
            {
                int newDamage = GameManager.Instance.enemy.SHIELD - attackCardController.damage;

                GameManager.Instance.CardCharacterInteraction("energy", "-", attackCardController.energyCost);
                GameManager.Instance.enemy.TakeShield(attackCardController.damage);
                if (GameManager.Instance.enemy.SHIELD <= 0)
                {
                    GameManager.Instance.enemy.TakeDamage(-newDamage);
                }

                attackCardController.gameObject.SetActive(false);
                attackCardController = null;

            }

        }
        else
        {
            attackCardController = null;
        }


    }
    public void UseDefenceCard(PointerEventData eventData)
    {
        defenceCardController = eventData.pointerDrag.GetComponent<DefenceCardController>();
        if(GameManager.Instance.character.energyCurrent >= defenceCardController.energyCost)
        {
            GameManager.Instance.CardCharacterInteraction("energy", "-", defenceCardController.energyCost);
            GameManager.Instance.CardCharacterInteraction("shield", "+", defenceCardController.defence);
            defenceCardController.gameObject.SetActive(false);
            defenceCardController = null;
        }
        else
        {
            defenceCardController = null;
        }

    }
    public void UseStrenghCard(PointerEventData eventData) 
    {
        strenghCardController = eventData.pointerDrag.GetComponent<StrengthCardController>();
        if (GameManager.Instance.character.energyCurrent >= strenghCardController.energyCost)
        {
            GameManager.Instance.CardCharacterInteraction("energy", "-", strenghCardController.energyCost);
            GameManager.Instance.CardCharacterInteraction("power", "+", strenghCardController.strength);
            strenghCardController.gameObject.SetActive(false);
            strenghCardController = null;
        }
        else
        {
            defenceCardController = null;
        }

    }

}
