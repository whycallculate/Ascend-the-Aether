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
        canvasGroup = GetComponent<CanvasGroup>();
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
            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = false; // Raycast'i kapat
            }
        }
       
        eventData.pointerDrag.transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true; // Raycast'i aç
        }
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

    //combine etmek istediğimiz kartın hangi kartlarla combine yapılacağını dizi olarak döndüren fonksiyon
    private CardLegendaryEnum[] CardLegendaryFind(GameObject selectCard)
    {
        switch(selectCard.tag)
        {
            case "AttackCard":
                return selectCard.GetComponent<AttackCardController>().CardCombineLegendary;
            case "DefenceCard":
            return selectCard.GetComponent<DefenceCardController>().CardCombineLegendary;
            
            case "AbilityCard":
            return selectCard.GetComponent<AbilityCardController>().CardCombineLegendary;
            
            case "StrenghCard":
            return selectCard.GetComponent<StrengthCardController>().CardCombineLegendary;
            
            default:
            return null;
        }
    }



}
