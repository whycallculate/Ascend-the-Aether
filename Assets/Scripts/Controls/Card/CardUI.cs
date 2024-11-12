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


    #endregion

    public void CardUIDescription(Sprite cardSprite,string cardName,string _cardDescription)
    {
        cardImage.sprite = cardSprite;
        cardNameText.text = cardName;
        cardDescription.text = _cardDescription;
    }

}
