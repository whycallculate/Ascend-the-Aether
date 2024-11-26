using System.Collections;
using System.Collections.Generic;
using Card_Enum;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBoard : MonoBehaviour,IDropHandler
{
    
    public AbilityCardController abilityCardController;
    public AttackCardController attackCardController;
    public DefenceCardController defenceCardController;
    public StrengthCardController strenghCardController;

    private bool isCardCombine = true;

    [SerializeField] private GameObject seasonTicket;
    [SerializeField] private GameObject combineCard;

    private AttackCardController combineAttackCard;
    private DefenceCardController combineDefenceCard;
    private AbilityCardController combineAbilityCard;
    private StrengthCardController combineStrengthCard;

    private int seasonTicketEnergy = 0;

    public void OnDrop(PointerEventData eventData)
    {

        GameManager.Instance.SelectableCard(false);
        if(eventData.pointerDrag.GetComponent<CanvasGroup>() != null)
        {
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        

        if(IsPointDragCombine(eventData.pointerDrag))
        {
            combineCard = eventData.pointerDrag;
            StartCoroutine(CombinationWait(eventData.pointerDrag));
        }
        if(!IsPointDragCombine(eventData.pointerDrag))
        {
            if(GameManager.Instance.CardCombineStart)
            {
                seasonTicket = eventData.pointerDrag;
                CardCombine(ref seasonTicket);
                
            }
            if(!GameManager.Instance.CardCombineStart)
            {
                CardImpact(eventData.pointerDrag);
            }

            
        }
    }

    private void CardImpact(GameObject card)
    {
        if (GameManager.Instance.enemy != null)
        {
            if (card.CompareTag("AbilityCard"))
            {
                UseAbilityCard(card);
            }
            else if (card.CompareTag("AttackCard"))
            {
                UseAttackCard(card);
            }
            else if (card.CompareTag("DefenceCard"))
            {

                UseDefenceCard(card);
            }
            else if (card.CompareTag("StrenghCard"))
            {
                UseStrenghCard(card);
            }

        }
        GameManager.Instance.HandCardPositionAdjust();
        seasonTicket = null;
        combineCard = null;
    }

    //combine kartı combine edip etmiyeceği kontrol ettiğimiz fonksiyon
    private IEnumerator CombinationWait(GameObject card)
    {
        GameManager.Instance.CardCombineStart = true;
        yield return new WaitForSeconds(5);

        
        if(seasonTicket == null && GameManager.Instance.CardCombineStart)
        {
            CardImpact(card);
        }
        GameManager.Instance.CardCombineStart = false;
    }
    
    //kartin kombine etme olayini kontrol ettiğimiz fonksiyon
    public void CardCombine(ref GameObject seasonTicket)
    {
        if(attackCardController != null)
        {
            for (int i = 0; i < attackCardController.CardCombineLegendary.Length; i++)
            {
                if(attackCardController.CardCombineLegendary[i] == SeasonTicketTypeFind(seasonTicket))
                {
                    CardComboManager.Instance.CardComboFunction(GameManager.Instance.character.GetComponent<CharacterController>(), new string[]{"healtbar","energy"}, new string[]{"-","-"},new int[]{20,(attackCardController.energyCost + seasonTicketEnergy)}, GameManager.Instance.enemy, "", 20, "");
                    seasonTicket.SetActive(false);
                    attackCardController.gameObject.SetActive(false);
                    isCardCombine = true;
                }
            }
        }
        else if(defenceCardController != null)
        {
            for (int i = 0; i < defenceCardController.CardCombineLegendary.Length; i++)
            {
                if(defenceCardController.CardCombineLegendary[i] == SeasonTicketTypeFind(seasonTicket))
                {
                    CardComboManager.Instance.CardComboFunction(GameManager.Instance.character.GetComponent<CharacterController>(), new string[]{"healtbar","energy"}, new string[]{"-","-"},new int[]{20,(defenceCardController.energyCost + seasonTicketEnergy)}, GameManager.Instance.enemy, "", 20, "");    
                    defenceCardController.gameObject.SetActive(false);
                    seasonTicket.SetActive(false);

                    isCardCombine = true;
                }
            }
        }
        else if(abilityCardController != null)
        {
            for (int i = 0; i < abilityCardController.CardCombineLegendary.Length; i++)
            {
                if(abilityCardController.CardCombineLegendary[i] == SeasonTicketTypeFind(seasonTicket))
                {
                    CardComboManager.Instance.CardComboFunction(GameManager.Instance.character.GetComponent<CharacterController>(), new string[]{"healtbar","energy"}, new string[]{"-","-"},new int[]{20,(abilityCardController.energyCost + seasonTicketEnergy)}, GameManager.Instance.enemy, "", 20, "");
                    isCombineCard = true;
                    abilityCardController.gameObject.SetActive(false);
                    seasonTicket.SetActive(false);
                }
            }
        }
        else if(strenghCardController != null)
        {   
            for (int i = 0; i < strenghCardController.CardCombineLegendary.Length; i++)
            {
                if(strenghCardController.CardCombineLegendary[i] == SeasonTicketTypeFind(seasonTicket))
                {
                    CardComboManager.Instance.CardComboFunction(GameManager.Instance.character.GetComponent<CharacterController>(), new string[]{"healtbar","energy"}, new string[]{"-","-"},new int[]{20,(strenghCardController.energyCost + seasonTicketEnergy)}, GameManager.Instance.enemy, "", 20, "");
                    strenghCardController.gameObject.SetActive(false);
                    seasonTicket.SetActive(false);
                    isCardCombine = true;
                }
            }
        }
        else
        {
            isCardCombine = false;
        }

        if(isCardCombine)
        {
            seasonTicket = null;
            combineCard = null;
        }
        
        
        GameManager.Instance.CardCombineStart = false;
    }

    //kombine edilicek kartın lengendarysini kontrol eden fonksiyon
    private CardLegendaryEnum SeasonTicketTypeFind(GameObject seasonTicket)
    {
        switch(seasonTicket.tag)
        {
            case "AttackCard":
                AttackCardController seasonAttack = seasonTicket.GetComponent<AttackCardController>();
                seasonTicketEnergy = seasonAttack.energyCost;
            return seasonAttack.CardLegendary;
            
            case "DefenceCard":
                DefenceCardController seasonDefence = seasonTicket.GetComponent<DefenceCardController>();
                seasonTicketEnergy = seasonDefence.energyCost;
            return seasonDefence.CardLegendary;

            case "AbilityCard":
                AbilityCardController seasonAbility = seasonTicket.GetComponent<AbilityCardController>();
                seasonTicketEnergy = seasonAbility.energyCost;
            return seasonAbility.CardLegendary;

            case "StrenghCard":
                StrengthCardController seasonStrengh = seasonTicket.GetComponent<StrengthCardController>();
                seasonTicketEnergy = seasonStrengh.energyCost;
            return seasonStrengh.CardLegendary;


            default:
            return CardLegendaryEnum.None;

        }
    }

    public void UseAbilityCard(GameObject card)
    {
        abilityCardController = card.GetComponent<AbilityCardController>();
        if(GameManager.Instance.character.energyCurrent >= abilityCardController.energyCost)
        {
            GameManager.Instance.CardCharacterInteraction("energy", "-", abilityCardController.energyCost);
            GameManager.Instance.enemy.EnemyAI.TakePower(abilityCardController.ability);
            abilityCardController.gameObject.SetActive(false);
            abilityCardController = null;
        }
        else
        {
            abilityCardController = null;
        }

        GameManager.Instance.enemy.GetEnemyAiStat();

    }
    public void UseAttackCard(GameObject card)
    {
        attackCardController = card.GetComponent<AttackCardController>();
        if(GameManager.Instance.character.energyCurrent >= attackCardController.energyCost)
        {
            if(GameManager.Instance.enemy.SHIELD <= 0)
            {
                GameManager.Instance.CardCharacterInteraction("energy", "-", attackCardController.energyCost);
                GameManager.Instance.enemy.EnemyAI.TakeDamage(attackCardController.damage);
                attackCardController.gameObject.SetActive(false);
                attackCardController = null;
            }
            else if(GameManager.Instance.enemy.SHIELD >= 0)
            {
                int newDamage = GameManager.Instance.enemy.SHIELD - attackCardController.damage;

                GameManager.Instance.CardCharacterInteraction("energy", "-", attackCardController.energyCost);
                GameManager.Instance.enemy.EnemyAI.TakeShield(attackCardController.damage);
                if (GameManager.Instance.enemy.SHIELD <= 0)
                {
                    GameManager.Instance.enemy.EnemyAI.TakeDamage(-newDamage);
                }

                attackCardController.gameObject.SetActive(false);
                attackCardController = null;

            }
            GameManager.Instance.enemy.GetEnemyAiStat();
        }
        else
        {
            attackCardController = null;
        }


    }
    public void UseDefenceCard(GameObject card)
    {
        defenceCardController = card.GetComponent<DefenceCardController>();
        if(GameManager.Instance.character.energyCurrent >= defenceCardController.energyCost)
        {
            GameManager.Instance.CardCharacterInteraction("energy", "-", defenceCardController.energyCost);
            GameManager.Instance.CardCharacterInteraction("shield", "+", defenceCardController.defence);
            defenceCardController.gameObject.SetActive(false);
            defenceCardController = null;
            GameManager.Instance.enemy.GetEnemyAiStat();
        }
        else
        {
            defenceCardController = null;
        }

    }
    public void UseStrenghCard(GameObject card) 
    {
        strenghCardController = card.GetComponent<StrengthCardController>();
        if (GameManager.Instance.character.energyCurrent >= strenghCardController.energyCost)
        {
            GameManager.Instance.CardCharacterInteraction("energy", "-", strenghCardController.energyCost);
            GameManager.Instance.CardCharacterInteraction("power", "+", strenghCardController.strength);
            strenghCardController.gameObject.SetActive(false);
            strenghCardController = null;
            GameManager.Instance.enemy.GetEnemyAiStat();
        }
        else
        {
            defenceCardController = null;
        }

    }

    private bool isCombineCard = false;
    
    private bool IsPointDragCombine(GameObject pointDragGameObject)
    {
        switch(pointDragGameObject.tag)
        {
            case "AttackCard":
                isCombineCard = pointDragGameObject.GetComponent<AttackCardController>().CardCombineLegendary.Length > 0 ? true : false;
                attackCardController = pointDragGameObject.GetComponent<AttackCardController>();
                return isCombineCard;

            case "DefenceCard":
                isCombineCard = pointDragGameObject.GetComponent<DefenceCardController>().CardCombineLegendary.Length > 0 ? true : false;
                defenceCardController = pointDragGameObject.GetComponent<DefenceCardController>();
                return isCombineCard;
                
            case "AbilityCard":
                isCombineCard = pointDragGameObject.GetComponent<AbilityCardController>().CardCombineLegendary.Length > 0 ? true : false;
                abilityCardController = pointDragGameObject.GetComponent<AbilityCardController>();
                return isCombineCard;
                
            case "StrenghCard":
                isCombineCard = pointDragGameObject.GetComponent<StrengthCardController>().CardCombineLegendary.Length > 0 ? true : false;
                strenghCardController = pointDragGameObject.GetComponent<StrengthCardController>();
                return isCombineCard;

            default:
                isCombineCard = false;
                return false;

        }
    }

}
