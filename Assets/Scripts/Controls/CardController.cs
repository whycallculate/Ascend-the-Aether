using System.Collections;
using System.Collections.Generic;
using Card_Enum;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private CardLegendaryEnum cardLegendary;
    [SerializeField] private int energyCost;
    [SerializeField] private float duration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardInitialize(CardLegendaryEnum _cardLegendary,int _energyCost,float _duration)
    {
        cardLegendary = _cardLegendary;
        energyCost = _energyCost;
        duration = _duration;
    }
}
