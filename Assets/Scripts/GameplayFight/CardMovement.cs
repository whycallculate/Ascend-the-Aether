using System.Collections;
using System.Collections.Generic;
using Card_Enum;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardMovement : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler,IPointerEnterHandler,IPointerExitHandler,IDropHandler
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
        eventData.pointerDrag.transform.localScale = Vector3.one;
        eventData.pointerDrag.GetComponent<Animator>().enabled = false;

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
        /*
        if(eventData.pointerEnter.GetComponent<CardUI>() != null)
        {
            GameManager.Instance.CardSelectedBeginAnimation(eventData.pointerEnter);
        }
        */
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        /*
        if(eventData.pointerEnter.GetComponent<CardUI>() != null)
        {

            GameManager.Instance.CardSelectedLeftAnimation(eventData.pointerEnter);
        }
        */
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
