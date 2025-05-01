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

    private void Start() 
    {
    }

    public void OnDrop(PointerEventData eventData)
    {

        GameManager.Instance.SelectableCard(false);
        if (eventData.pointerDrag.GetComponent<CanvasGroup>() != null)
        {
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        //CardInteraction(eventData.pointerDrag);
        StartCoroutine(AnimationAfter(eventData.pointerDrag));
    }

    private IEnumerator AnimationAfter(GameObject eventData)
    {
        yield return new WaitForSeconds(1f);
        CardInteraction(eventData);
    }

    
    private void CardInteraction(GameObject eventData)
    {
        if (IsPointDragCombine(eventData))
        {
            combineCard = eventData;
            StartCoroutine(CombinationWait(eventData));
        }
        else if (!IsPointDragCombine(eventData))
        {
            seasonTicket = eventData;
        
            if(GameManager.Instance.CardCombineStart)
            {
                CardCombine(ref seasonTicket,combineCard);
            }
            else if(!GameManager.Instance.CardCombineStart)
            {
                CardImpact(eventData);
                seasonTicket = null;
            }
        }
    }
    //Card_0_DeckReturnAnimation
    
    

    //combine kartı combine edip etmiyeceği kontrol ettiğimiz fonksiyon
    private IEnumerator CombinationWait(GameObject card)
    {
        

        GameManager.Instance.CardCombineStart = true;
        yield return new WaitForSeconds(2);

        
        
        if(seasonTicket == null && GameManager.Instance.CardCombineStart)
        {
            GameManager.Instance.CardCombineStart = false;
            CardImpact(card);
            
           
            seasonTicket = null;
            combineCard = null;
        }
    
    

    }
    
    //CardComboManager.Instance.CardComboFunction(GameManager.Instance.character.GetComponent<CharacterController>(), new string[]{"healtbar","energy"}, new string[]{"-","-"},new int[]{20,(attackCardController.energyCost + seasonTicketEnergy)}, GameManager.Instance.enemy, "", 20, "");
    //kartin kombine etme olayini kontrol ettiğimiz fonksiyon
    public void CardCombine(ref GameObject _seasonTicket,GameObject card)
    {
        print(_seasonTicket.name + "-" + combineCard.name);
        
        GameManager.Instance.CardCombineStart = false;

        switch(card.tag)
        {
            case "AttackCard":
                AttackCardController attackCard = card.GetComponent<AttackCardController>();
                for (int i = 0; i < attackCard.CardCombineLegendary.Length; i++)
                {
                    if(attackCard.CardCombineLegendary[i] == SeasonTicketTypeFind(_seasonTicket))
                    {
                        print("attack combosu");
                        combineCard.GetComponent<Animator>().enabled = false;
                    }
                }
            break;
            
            case "DefenceCard":
                DefenceCardController defenceCard = card.GetComponent<DefenceCardController>();
                for (int i = 0; i < defenceCard.CardCombineLegendary.Length; i++)
                {
                    if(defenceCard.CardCombineLegendary[i] == SeasonTicketTypeFind(_seasonTicket))
                    {
                        combineCard.GetComponent<Animator>().enabled = false;

                    }
                }
            break;
            
            case "AbilityCard":
                AbilityCardController abilityCard = card.GetComponent<AbilityCardController>();
                for (int i = 0; i < abilityCard.CardCombineLegendary.Length; i++)
                {
                    if(abilityCard.CardCombineLegendary[i] == SeasonTicketTypeFind(_seasonTicket))
                    {
                        combineCard.GetComponent<Animator>().enabled = false;

                    }
                }
            break;

            case "StrenghCard":
                StrengthCardController strenghCard = card.GetComponent<StrengthCardController>();
                for (int i = 0; i < strenghCard.CardCombineLegendary.Length; i++)
                {
                    if(strenghCard.CardCombineLegendary[i] == SeasonTicketTypeFind(_seasonTicket))
                    {
                        combineCard.GetComponent<Animator>().enabled = false;

                    }
                }
            break;
            
            default:
            break;
        }




        combineCard = null;
        _seasonTicket = null;
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
        //GameManager.Instance.HandCardPositionAdjust();

        
    }

    //kombine edilicek kartın lengendarysini kontrol eden fonksiyon
    private CardLegendaryEnum SeasonTicketTypeFind(GameObject seasonTicket)
    {
        switch(seasonTicket.tag)
        {
            case "AttackCard":
            print("attack : "+seasonTicket.name);
                AttackCardController seasonAttack = seasonTicket.GetComponent<AttackCardController>();
                seasonTicketEnergy = seasonAttack.energyCost;
            return seasonAttack.CardLegendary;
            
            case "DefenceCard":
            print("defence : "+seasonTicket.name);
                DefenceCardController seasonDefence = seasonTicket.GetComponent<DefenceCardController>();
                seasonTicketEnergy = seasonDefence.energyCost;
            return seasonDefence.CardLegendary;

            case "AbilityCard":
            print("ability : "+seasonTicket.name);
                AbilityCardController seasonAbility = seasonTicket.GetComponent<AbilityCardController>();
                seasonTicketEnergy = seasonAbility.energyCost;
            return seasonAbility.CardLegendary;

            case "StrenghCard":
            print("strengh : "+seasonTicket.name);
                StrengthCardController seasonStrengh = seasonTicket.GetComponent<StrengthCardController>();
                seasonTicketEnergy = seasonStrengh.energyCost;
            return seasonStrengh.CardLegendary;


            default:
            return CardLegendaryEnum.None;

        }
    }


    #region 

    public void UseAbilityCard(GameObject card)
    {
        abilityCardController = card.GetComponent<AbilityCardController>();
        if(GameManager.Instance.character.energyCurrent >= abilityCardController.energyCost)
        {
            GameManager.Instance.CardCharacterInteraction("energy", "-", abilityCardController.energyCost);
            GameManager.Instance.enemy.EnemyAI.TakePower(abilityCardController.ability);
            //abilityCardController.gameObject.SetActive(false);
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
            if(GameManager.Instance.enemy.EnemyAI.shield<= 0)
            {
                GameManager.Instance.CardCharacterInteraction("energy", "-", attackCardController.energyCost);
                GameManager.Instance.enemy.EnemyAI.TakeDamage(attackCardController.damage);
                //attackCardController.gameObject.SetActive(false);
                attackCardController = null;
            }
            else if(GameManager.Instance.enemy.EnemyAI.shield >= 0)
            {
                int newDamage = GameManager.Instance.enemy.SHIELD - attackCardController.damage;

                GameManager.Instance.CardCharacterInteraction("energy", "-", attackCardController.energyCost);
                GameManager.Instance.enemy.EnemyAI.TakeShield(attackCardController.damage);
                if (GameManager.Instance.enemy.EnemyAI.shield <= 0)
                {
                    GameManager.Instance.enemy.EnemyAI.TakeDamage(-newDamage);
                }

                //attackCardController.gameObject.SetActive(false);
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
            //defenceCardController.gameObject.SetActive(false);
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
            //strenghCardController.gameObject.SetActive(false);
            strenghCardController = null;
            GameManager.Instance.enemy.GetEnemyAiStat();
        }
        else
        {
            defenceCardController = null;
        }

    }
    #endregion

    private bool isCombineCard = false;
    
    //kart'ın combo yapabiliceğini ve sayısını kontrol eden method
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
