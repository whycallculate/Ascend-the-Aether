using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Card_Enum;
using CardTypes;

public class DefenceCardController : MonoBehaviour
{
    #region features of the card
    [SerializeField] private CardTypeEnum cardType;
    [SerializeField] private CardLegendaryEnum cardLegendary;
    [SerializeField] private CardLegendaryEnum[] cardCombineLegendary;
    [SerializeField] private int energyCost;
    [SerializeField] private float duration;
    [SerializeField] private int defence;
    #endregion

    [SerializeField] private DefenceCardController[] defenceCards;
    [SerializeField] private Button cardButton;

    private string cardFolderPath;
    private bool isClickCard = false;

    private void Awake() 
    {
    }

    private void OnValidate() 
    {

        cardFolderPath = "Prefabs/Cards/DefenceCards";
        defenceCards = Resources.LoadAll<DefenceCardController>(cardFolderPath);
        
        cardButton = GetComponent<Button>();    
        cardButton.onClick.AddListener(Card_Move);
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

    public void CardInitialize(CardTypeEnum _cardType,CardLegendaryEnum _cardLegendary,int _energyCost,float _duration,CardLegendaryEnum[] _cardCombineLegendary,int _defence)
    {
        cardType = _cardType;
        cardLegendary = _cardLegendary;
        energyCost = _energyCost;
        duration = _duration;
        cardCombineLegendary = _cardCombineLegendary;
    }
}
