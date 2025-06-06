using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemPriceText;
        [SerializeField] private Image itemClickUI;



        public void OpenItemInfoUI(string itemName, int itemPrice)
        {
            itemNameText.gameObject.SetActive(true);
            itemPriceText.gameObject.SetActive(true);
            SetItemName(itemName);
            SetItemPrice(itemPrice);
            

        }


        public void CloseItemUIActive()
        {
            itemNameText.gameObject.SetActive(false);
            itemPriceText.gameObject.SetActive(false);
            itemClickUI.gameObject.SetActive(false);
        }

        public void SetItemName(string itemName)
        {
            if (!itemNameText.gameObject.activeSelf) itemNameText.gameObject.SetActive(true);

            itemNameText.text = itemName;
        }
        public void SetItemPrice(int itempPrice)
        {

            if (!itemPriceText.gameObject.activeSelf) itemPriceText.gameObject.SetActive(true);

            itemPriceText.text = itempPrice.ToString();

        }

        public void SetItemClickUI(bool click)
        {
            itemClickUI.gameObject.SetActive(click);
        }

        public void ItemPriceTextClose()
        {
            itemPriceText.gameObject.SetActive(false);
        }
        public void ItemPriceTextOpen()
        {
            itemPriceText.gameObject.SetActive(true);
        }

    }

}
