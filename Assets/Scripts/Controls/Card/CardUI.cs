using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    #region  card ui

    [SerializeField] private Image cardImage;
    public Image CardImage { get { return cardImage; }}
    [SerializeField] private TextMeshProUGUI cardNameText;
    public TextMeshProUGUI CardNameText { get { return cardNameText; } }
    [SerializeField] private TextMeshProUGUI cardDescription;
    public TextMeshProUGUI CardDescription { get { return cardDescription; }}
    [SerializeField] private TextMeshProUGUI energyNumber_Text;
    public TextMeshProUGUI EnergyNumber_Text { get { return energyNumber_Text;} set { energyNumber_Text = value; }}

    #endregion

    public void CardUIInitialize(Sprite cardSprite,string cardName,string _cardDescription,int cardEnery)
    {
        cardImage.sprite = cardSprite;
        cardNameText.text = cardName;
        cardDescription.text = _cardDescription;
        energyNumber_Text.text = cardEnery.ToString();
    }

    public void CardUpgradeInitialize(int cardEnery)
    {
        if(energyNumber_Text != null)
        {
            energyNumber_Text.text = cardEnery.ToString();
        }
    }

    public void CardMoveAnimationFinish()
    {
        GetComponent<Animator>().enabled = false;
    }

    public void CardDeckReturnAnimationFinish()
    {
        GetComponent<Animator>().enabled = false;
        gameObject.SetActive(false);

        if(GameManager.Instance.DeadEnemyCount == GameManager.Instance.enemys.Count)
        {
            if(GameManager.Instance.FinishLevel)
            {
                GameManager.Instance.WhatOfKindCharacter(2,4);
            }
            else if(!GameManager.Instance.FinishLevel)
            {
                GameManager.Instance.WhatOfKindCharacter(2,3);

            }
        }
    }

    public void CardSelectBeginAnimationStart()
    {
        for (int i = 0; i < GameManager.Instance.hand.Count; i++)
        {
            if(GameManager.Instance.hand[i].gameObject.name != gameObject.name)
            {
                GameManager.Instance.hand[i].GetComponent<Animator>().enabled = false;  
            }
        }
    }
}
