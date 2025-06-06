using System.Collections;
using System.Collections.Generic;
using Item.Enums;
using UnityEngine;

namespace Item
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField] private ItemUI itemUI;
        [SerializeField] private ItemRarityEnum itemRarity;
        public ItemRarityEnum ItemRarityEnum { get { return itemRarity; } set { itemRarity = value; } }
        [SerializeField] private int itemPrice;
        public int ItemPrice { get { return itemPrice; } set { itemPrice = value; } }

        private bool isClick = false;
        private string oldMarketPanelName = "";


        void Awake()
        {
            ItemComponentValueIdentification();
        }

        public void ItemComponentValueIdentification()
        {
            if (itemUI == null)
            {
                itemUI = GetComponent<ItemUI>();
            }

        }

        public void shopItemButtonClick(string marketPanleName)
        {

            if (!isClick)
            {
                itemUI.SetItemClickUI(true);
                MarketManager.Instance.AddShopItem(this);
            }
            else if (isClick)
            {
                itemUI.SetItemClickUI(false);
                MarketManager.Instance.RemoveShopItem(this);
            }


            if (MarketManager.Instance.ShopItems.Count <= 1)
            {

                if (oldMarketPanelName != "")
                {
                    if (marketPanleName != oldMarketPanelName)
                    {
                        isClick = false;

                    }

                }


                if (!isClick)
                {
                    MarketManager.Instance.MarketUI.TransactionButtonUIControl(true);
                    itemUI.SetItemClickUI(true);
                }
                else if (isClick & MarketManager.Instance.ShopItems.Count > 0)
                {
                    itemUI.SetItemClickUI(false);
                }
                else if (isClick && MarketManager.Instance.ShopItems.Count == 0)
                {
                    MarketManager.Instance.MarketUI.TransactionButtonUIControl(false);
                    itemUI.SetItemClickUI(false);
                }



            }
            isClick = !isClick;
            oldMarketPanelName = marketPanleName;

        }

        public void bagItemButtonClick(string marketPanleName)
        {

            if (!isClick)
            {
                itemUI.SetItemClickUI(true);
                MarketManager.Instance.AddBagItem(gameObject);
            }
            else if (isClick)
            {
                itemUI.SetItemClickUI(false);
                MarketManager.Instance.RemoveBagItem(gameObject);
            }


            if (MarketManager.Instance.BagEquipments.Count <= 1)
            {

                if (oldMarketPanelName != "")
                {
                    if (marketPanleName != oldMarketPanelName)
                    {
                        isClick = false;

                    }

                }


                if (!isClick)
                {
                    MarketManager.Instance.MarketUI.TransactionButtonUIControl(true);
                    itemUI.SetItemClickUI(true);
                }
                else if (isClick & MarketManager.Instance.BagEquipments.Count > 0)
                {
                    itemUI.SetItemClickUI(false);
                }
                else if (isClick && MarketManager.Instance.BagEquipments.Count == 0)
                {
                    MarketManager.Instance.MarketUI.TransactionButtonUIControl(false);
                    itemUI.SetItemClickUI(false);
                }



            }

            isClick = !isClick;
            oldMarketPanelName = marketPanleName;
        }

        public void ItemClickAdjust(bool isClick)
        {
            this.isClick = isClick;
        }

        public void SetItemPosition(Vector3 pos, Transform parent)
        {
            transform.SetParent(parent);
            transform.position = pos;
        }

        public void SetItemActive(bool active)
        {
            gameObject.SetActive(active);
        }


        public void ItemUIClose()
        {
            isClick = false;
            itemUI.SetItemClickUI(false);
        }

        //shop sayfasinda oluşturulan kart'ın nametext ve pricetext'ne değer atamamizi sağliyor.
        public void ItemPropertAndUIValueAdjust(string _itemName, int _itemPrice)
        {
            itemPrice = _itemPrice;
            ItemComponentValueIdentification();
            itemUI.OpenItemInfoUI(_itemName, itemPrice);
        }

        public void ItemFirstCreated(string _itemName,int _itemPrice)
        {
            
            ItemPropertAndUIValueAdjust(_itemName,_itemPrice);
        }            

    }


}
