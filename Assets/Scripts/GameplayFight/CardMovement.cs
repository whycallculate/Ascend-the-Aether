using System.Collections;
using System.Collections.Generic;
using Card_Enum;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardMovement : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler,IPointerEnterHandler,IDropHandler
{   
    private CardTypeEnum cardType;
    public CardTypeEnum CardType {get {return cardType;} set {cardType = value;} }
    private CardLegendaryEnum[] combineCardLegendary;
    public CardLegendaryEnum[] CombineCardLengendary { get { return combineCardLegendary; } set { combineCardLegendary = value;}}
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;

    private RectTransform startParent;
    
    private Button cardButton;

    public CanvasGroup canvasGroup;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cardButton = GetComponent<Button>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }
    
    private void Update() 
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(GameManager.Instance.enemy != null)
        {
            if (cardButton.interactable)
            {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
            
        }
       
        eventData.pointerDrag.transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || eventData.pointerEnter == null)
        {
            return;
        }

        if(!eventData.pointerEnter.CompareTag("CardBoard"))
        {
            GameObject card = eventData.pointerDrag;
            CardBackPositionMove(ref card);
        }
    }

    #region 
    //seçtiğimiz kartı başka kartın üstüne götürüp bırakınca combine olup olmuyacağı belirleyen fonksiyon
    /*
    public void CardCombineFunction(GameObject eventData,bool isCombineTimeFinish)
    {
        if(!isCombineTimeFinish)
        {
            switch (eventData.tag)
            {
                case "AttackCard":
                    foreach (CardLegendaryEnum item in CardLegendaryFind(eventData))
                    {
                        if (item == eventData.GetComponent<AttackCardController>().CardLegendary)
                        {
                            eventData.gameObject.SetActive(false);
                            eventData.gameObject.SetActive(false);
                            CardComboManager.Instance.CardComboFunction(GameManager.Instance.character.GetComponent<CharacterController>(), "healtbar", "-", 20, GameManager.Instance.enemy, "", 20, "");
                        }
                    }
                    break;
                case "AbilityCard":
                    foreach (CardLegendaryEnum item in CardLegendaryFind(eventData))
                    {
                        if (item == eventData.GetComponent<AbilityCardController>().CardLegendary)
                        {
                            eventData.gameObject.SetActive(false);
                            eventData.gameObject.SetActive(false);
                            CardComboManager.Instance.CardComboFunction(GameManager.Instance.character.GetComponent<CharacterController>(), "healtbar", "-", 20, GameManager.Instance.enemy, "", 20, "");
                        }
                    }
                    break;
                case "DefenceCard":
                    foreach (CardLegendaryEnum item in CardLegendaryFind(eventData))
                    {
                        if (item == eventData.GetComponent<DefenceCardController>().CardLegendary)
                        {
                            eventData.gameObject.SetActive(false);
                            eventData.gameObject.SetActive(false);
                            CardComboManager.Instance.CardComboFunction(GameManager.Instance.character.GetComponent<CharacterController>(), "healtbar", "-", 20, GameManager.Instance.enemy, "", 20, "");
                        }
                    }
                    eventData.gameObject.SetActive(false);
                    eventData.gameObject.SetActive(false);
                    break;
                case "StrenghCard":
                    foreach (CardLegendaryEnum item in CardLegendaryFind(eventData))
                    {
                        if (item == eventData.GetComponent<StrengthCardController>().CardLegendary)
                        {
                            eventData.gameObject.SetActive(false);
                            eventData.gameObject.SetActive(false);
                            CardComboManager.Instance.CardComboFunction(GameManager.Instance.character.GetComponent<CharacterController>(), "healtbar", "-", 20, GameManager.Instance.enemy, "", 20, "");
                        }
                    }
                    break;
                default:
                    Debug.Log(eventData.name + " karti ile konbine edemezsin");
                    break;
            }
        }
        else
        {

        }
        
    }
    */
    #endregion

    private void CardBackPositionMove(ref GameObject card)
    {
        
        switch(card.tag)
        {

            case "AttackCard":
                card.GetComponent<RectTransform>().anchoredPosition =  card.GetComponent<AttackCardController>().cardPosition;
            break;

            case "DefenceCard":
                card.GetComponent<RectTransform>().anchoredPosition =  card.GetComponent<DefenceCardController>().cardPosition;
            break;
            
            case "AbilityCard":
                card.GetComponent<RectTransform>().anchoredPosition =  card.GetComponent<AbilityCardController>().cardPosition;
            break;
            
            case "StrenghCard":
                card.GetComponent<RectTransform>().anchoredPosition =  card.GetComponent<StrengthCardController>().cardPosition;
            break;
        
        }
    }
}
