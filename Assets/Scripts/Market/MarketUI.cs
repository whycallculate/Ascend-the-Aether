using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;
using UnityEngine.UI;

namespace Market
{
    public class MarketUI : MonoBehaviour
    {
        [SerializeField] private GameObject bagPanel;
        [SerializeField] private GameObject shopPanel;

        [SerializeField] private Transform shopItemContent;
        [SerializeField] private Transform bagItemContent;

        [SerializeField] private Button shopButton;
        [SerializeField] private Button bagButton;

        [SerializeField] private Button refreshButton;

        [SerializeField] private Button sellButton;
        [SerializeField] private Button buyButton;

        public void SetActiveShopPanel(bool active)
        {
            shopPanel.SetActive(active);
        }

        public void SetActiveBagPanel(bool active)
        {
            bagPanel.SetActive(active);
        }

        //Shop butonuna tıkladıktan sonra shop sayfasının açılıp içerisinde ki item'ları ayarlayan method.
        public void ShopButtonFunction()
        {
            bool active = shopPanel.activeSelf == true ? false : true;

            ItemClickValueReset();

            SetActiveShopPanel(active);
            SetActiveBagPanel(false);
            MarketManager.Instance.MarketItemAdjustment.ShopItemAdjustment(shopItemContent);
            SetBuyButtonActive(active);
            SetSellButtonActive(false);
            SetRefreshButtonActive(active);

            SetBuyButtonInteractable(false);
            
        }

        //Bag butonuna tıkladıktan sonra bag sayfasının açılıp içerisinde ki item'ları ayarlayan method.
        public void BagButtonFunction()
        {
            bool active = bagPanel.activeSelf == true ? false : true;
            MarketManager.Instance.MarketItemAdjustment.ShopItemsReset();
            ItemClickValueReset();
            
            SetActiveBagPanel(active);
            SetActiveShopPanel(false);
            MarketManager.Instance.MarketItemAdjustment.BagItemAdjusment(bagItemContent);
            SetBuyButtonActive(false);
            SetSellButtonActive(active);
            SetRefreshButtonActive(false);

            SetSellButtonInteractable(false);
            
        }

        private void ItemClickValueReset()
        {
            if(shopPanel.gameObject.activeSelf)
            {
                for (int i = 0; i < MarketManager.Instance.ShopItems.Count; i++)
                {
                    ItemController shopItemController =  MarketManager.Instance.ShopItems[i].GetComponent<ItemController>();
                    shopItemController.ItemClickAdjust(false);
                    ItemUI shopItemUI =  shopItemController.GetComponent<ItemUI>();
                    shopItemUI.SetItemClickUI(false);
                }
            }
            else if(bagPanel.gameObject.activeSelf)
            {
                for (int i = 0; i < MarketManager.Instance.BagEquipments.Count; i++)
                {
                    if(MarketManager.Instance.BagEquipments[i] != null)
                    {
                        ItemController bagEquipments = MarketManager.Instance.BagEquipments[i].GetComponent<ItemController>();
                        bagEquipments.ItemClickAdjust(false);
                        ItemUI bagItemUI =  bagEquipments.GetComponent<ItemUI>();
                        bagItemUI.SetItemClickUI(false);
                    }
                }
            }
            MarketManager.Instance.ClearShopItem();
            MarketManager.Instance.ClearBagItem();
        }

        public void TransactionButtonUIControl(bool value)
        {
            if(shopPanel.activeSelf)
            {
                SetBuyButtonInteractable(value);
            }
            else if(bagPanel.activeSelf)
            {
                SetSellButtonInteractable(value);
            }
        }

        public void SetSellButtonActive(bool active)
        {
            sellButton.gameObject.SetActive(active);
        }

        public void SetSellButtonInteractable(bool interactable)
        {
            sellButton.interactable  = interactable;
        }

        public void SetBuyButtonActive(bool active)
        {
            buyButton.gameObject.SetActive(active);
        }

        public void SetBuyButtonInteractable(bool interactable)
        {
            buyButton.interactable  = interactable;
        }


        private void SetRefreshButtonActive(bool active)
        {
            refreshButton.gameObject.SetActive(active);
        }

        public void SellButtonFunction()
        {
            
            if(MarketManager.Instance.BagEquipments.Count > 0)
            {
                int result = 0;
                for (int i = 0; i < MarketManager.Instance.BagEquipments.Count; i++)
                {
                    if(MarketManager.Instance.BagEquipments[i].GetComponent<ItemController>() != null)
                    {
                        ItemController itemController = MarketManager.Instance.BagEquipments[i].GetComponent<ItemController>();
                        Destroy(itemController.gameObject);
                        MarketManager.Instance.BuyItemRemove(MarketManager.Instance.BagEquipments);
                        result += itemController.ItemPrice;
                    }
                }

                MarketManager.Instance.MarketTransaction.BagCrystalControl(result);
            }
            else
            {
                return;
            }

        }

        public void BuyButtonFunction()
        {
            int itemsPrice = ItemPriceCalculation();
            bool isBuy = GameManager.Instance.CrystalCount > itemsPrice;
            if(isBuy)
            {
                ItemProperyAdjusting();
                GameManager.Instance.CrystalCoinLose(itemsPrice);
            }
            else
            {
                print("satin alma başarisiz");
            }

            
        }

        private int ItemPriceCalculation()
        {
            int price = 0;
            for (int i = 0; i < MarketManager.Instance.ShopItems.Count; i++)
            {
                ItemController itemController =MarketManager.Instance.ShopItems[i].GetComponent<ItemController>();
                price += itemController.ItemPrice;
            }
            return price;
        }

        private void ItemProperyAdjusting()
        {
            for (int i = 0; i < MarketManager.Instance.ShopItems.Count; i++)
            {
                ItemController itemController =MarketManager.Instance.ShopItems[i].GetComponent<ItemController>();
                itemController.SetItemPosition(Vector3.zero,UIManager.Instance.BuyCardParent);
                itemController.BuyItemUIClose();         
                itemController.SetItemActive(false);     
                AddBuyItemEquipmets(itemController);  
                MarketManager.Instance.BuyItemSave();
            }

            MarketManager.Instance.ShopItems.Clear();
            MarketManager.Instance.ResetButton();
        }

        private void AddBuyItemEquipmets(ItemController itemController)
        {
            MarketManager.Instance.AddItemEquiments(itemController.gameObject);
        }


        //shop sayfasında ki item button'ları için yazıldı
        public string MarketPanelNameControl()
        {
            return shopPanel.activeSelf ? "Shop" : "Bag";
            
        }

        //market de ki refresh butonu için bu fonksiyon çalışınca shop da ki item nesneleri yenileniyor.


        public void MarketShopRefreshButton()
        {
            MarketManager.Instance.Shop.Clear();
            MarketManager.Instance.MarketItemAdjustment.ShopItemsReset();
            MarketManager.Instance.MarketItemAdjustment.ShopItemResfresh(shopItemContent);
        }
    
        public void MarketBacReturn()
        {
            MarketManager.Instance.MarketItemAdjustment.ShopItemsReset();
            //gameObject.SetActive(false);

            for (int i = 0; i < MarketManager.Instance.Shop.Count; i++)
            {
                Destroy(MarketManager.Instance.Shop[i].gameObject);
            }


            for (int i = 0; i < GameManager.Instance.Equipments.Count; i++)
            {
                GameObject equipments = GameManager.Instance.Equipments[i].gameObject;
                equipments.SetActive(false);
                equipments.transform.position = Vector2.zero;
                equipments.transform.SetParent(UIManager.Instance.Equipments);
            }
            

            MarketManager.Instance.Shop.Clear();
            MarketManager.Instance.BagEquipments.Clear();

            gameObject.SetActive(false);
            
        }


    }

}
