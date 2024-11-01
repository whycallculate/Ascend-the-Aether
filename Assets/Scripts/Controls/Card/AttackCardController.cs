using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Card_Enum;
using CardTypes;
public class AttackCardController : MonoBehaviour
{
    #region features of the card
    [SerializeField] private CardTypeEnum cardType;
    [SerializeField] private CardLegendaryEnum cardLegendary;
    [SerializeField] private CardLegendaryEnum[] cardCombineLegendary;
    [SerializeField] private int energyCost;
    [SerializeField] private float duration;
    #endregion
    public AttackCard attackCardOptions;
    
    [SerializeField] private AttackCardController[] attackCards;
    [SerializeField] private Button cardButton;

    private string cardFolderPath;
    private bool isClickCard = false;

    private void Awake() 
    {
        CardLoading();
    }

    private void OnValidate()
    {
        attackCardOptions = new AttackCard();
        
        CardLoading();

        cardButton = GetComponent<Button>();
        cardButton.onClick.AddListener(Card_Move);
    }

    // Card object allows retrieval from the Sources Folder ; allows : izin verir, retrieval :geri alma
    private void CardLoading()
    {
        cardFolderPath = "Prefabs/Cards/AttackCards";
        attackCards = Resources.LoadAll<AttackCardController>(cardFolderPath);
    }


    public void Card_Move()
    {
        isClickCard = !isClickCard;
        
       

        StartCoroutine(CardMovement());

    }
    
    private IEnumerator CardMovement()
    {
        while (isClickCard)
        {
            transform.position = Input.mousePosition;
            
            yield return null;
        }
    }

    public void CardInitialize(CardTypeEnum _cardType,CardLegendaryEnum _cardLegendary,int _energyCost,float _duration,CardLegendaryEnum[] _cardCombineLegendary)
    {
        cardType = _cardType;
        cardLegendary = _cardLegendary;
        energyCost = _energyCost;
        duration = _duration;
        cardCombineLegendary = _cardCombineLegendary;
    }
}
