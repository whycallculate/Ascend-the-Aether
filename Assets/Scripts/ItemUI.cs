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

        public void SetActiveItemUI(bool active, string itemName, int itemPrice)
        {
            if (itemName != "" && itemName != null && itemPrice != 0 && itemPrice > 0)
            {
                itemNameText.gameObject.SetActive(active);
                itemPriceText.gameObject.SetActive(active);
            }
            if (active)
            {
                SetItemName(itemName);
                SetItemPrice(itemPrice);
            }
        }
        public void CloseItemUIActive()
        {
            itemNameText.gameObject.SetActive(false);
            itemPriceText.gameObject.SetActive(false);
            itemClickUI.gameObject.SetActive(false);
        }

        public void SetItemName(string itemName)
        {
            itemNameText.text = itemName;
        }
        public void SetItemPrice(int itempPrice)
        {
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
