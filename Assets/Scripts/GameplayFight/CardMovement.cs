using System.Collections;
using System.Collections.Generic;
using Card_Enum;
using CardObjectCommon_Features;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardMovement : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler,IPointerEnterHandler,IPointerExitHandler,IDropHandler
{
    [SerializeField] private CardAnimationControl cardAnimationControl;
    private CardTypeEnum cardType;
    public CardTypeEnum CardType {get {return cardType;} set {cardType = value;} }
    private CardLegendaryEnum[] combineCardLegendary;
    public CardLegendaryEnum[] CombineCardLengendary { get { return combineCardLegendary; } set { combineCardLegendary = value;}}
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;

    private Button cardButton;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cardButton = GetComponent<Button>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        if (cardAnimationControl == null) cardAnimationControl = GetComponent<CardAnimationControl>(); 
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

        if(GameManager.Instance.enemy != null)
        {
            if (cardButton.interactable)
            {
                transform.position = Input.mousePosition;
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

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || eventData.pointerEnter == null)
        {
            return;
        }
        
        if (!eventData.pointerEnter.CompareTag("CardBoard"))
        {
            GameObject card = eventData.pointerDrag;
            CardBackPositionMove( card);
        }
        
       
        
    }



    private void CardBackPositionMove(GameObject card)
    {
        /*
        switch(card.tag)
        {

            case "AttackCard":
                rectTransform.anchoredPosition =  card.GetComponent<AttackCardController>().cardPosition;
            break;

            case "DefenceCard":
                rectTransform.anchoredPosition =  card.GetComponent<DefenceCardController>().cardPosition;
            break;
            
            case "AbilityCard":
                rectTransform.anchoredPosition =  card.GetComponent<AbilityCardController>().cardPosition;
            break;
            
            case "StrenghCard":
                rectTransform.anchoredPosition =  card.GetComponent<StrengthCardController>().cardPosition;
            break;
        
        }
        */
        CardObjectCommonFeatures _card = card.GetComponent<CardObjectCommonFeatures>();
        transform.SetParent(_card.cardAnimationPositionParent);
        rectTransform.anchoredPosition = _card.cardPosition;
    }

    
}
