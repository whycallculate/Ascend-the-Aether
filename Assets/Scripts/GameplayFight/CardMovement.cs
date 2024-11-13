using System.Collections;
using System.Collections.Generic;
using Card_Enum;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardMovement : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler,IDropHandler
{   
    private CardTypeEnum cardType;
    public CardTypeEnum CardType {get {return cardType;} set {cardType = value;} }
    private CardLegendaryEnum[] combineCardLegendary;
    public CardLegendaryEnum[] CombineCardLengendary { get { return combineCardLegendary; } set { combineCardLegendary = value;}}
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;

    private RectTransform startParent;
    
    private Button cardButton;

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
        if(cardButton.interactable)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        switch(eventData.pointerDrag.tag)
        {
            case "AttackCard":
            break;
        }
        

        
    }
}
