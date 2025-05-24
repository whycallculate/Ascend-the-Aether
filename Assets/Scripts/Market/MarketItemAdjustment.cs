using System.Collections;
using System.Collections.Generic;
using Card_Enum;
using Item;
using Item.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Market
{
    public class MarketItemAdjustment 
    {

        public void ShopItemAdjustment(Transform parentObject)
        {
            int missingCount  =  10 - MarketManager.Instance.Shop.Count;
            if(MarketManager.Instance.Shop.Count  == 0)
            {
                CreateWithItemRatio(parentObject,10);
            }
            else
            {
                CreateWithItemRatio(parentObject,missingCount);

            }
        }


        public void ShopItemResfresh(Transform parent)
        {


            for (int i = 0; i < parent.childCount; i++)
            {
                GameObject.Destroy(parent.transform.GetChild(i).gameObject);
            }


            CreateWithItemRatio(parent, 10);

        }

        //Shop sayfasına random bir şekilde oluşturmayı sağliyor.    
        public void CreateWithItemRatio(Transform parentObject, int _itemsCount)
        {
            int shopItemCount = _itemsCount;
            int i = 0;
            int ordinaryCount = 0;
            int rareCount = 0;
            int legendaryCount = 0;

            GameObject shopItem = null;

            // Eğer toplam eklenecek item 10'dan azsa, rarity oranı uygulama
            if (shopItemCount < 10)
            {
                while (i < shopItemCount)
                {
                    int randomIndex = Random.Range(0, GameManager.Instance.GameAllCards.Count);
                    ItemController item = GameManager.Instance.GameAllCards[randomIndex].GetComponent<ItemController>();
                    shopItem = CreateItem(parentObject, item, randomIndex);
                    ShopItemButtonFunction(shopItem.GetComponent<Button>());
                    i++;
                }
                return;
            }

            // 10 veya daha fazlaysa, oranları uygula
            int ordinaryMaxCount = (shopItemCount * 70) / 100;
            int rareMaxCount = (shopItemCount * 20) / 100;
            int legendaryMaxCount = (shopItemCount * 10) / 100;

            while (i < shopItemCount)
            {
                int randomIndex = Random.Range(0, GameManager.Instance.GameAllCards.Count);
                ItemController item = GameManager.Instance.GameAllCards[randomIndex].GetComponent<ItemController>();

                if (item.ItemRarityEnum == ItemRarityEnum.OrdinaryCard && ordinaryCount < ordinaryMaxCount)
                {
                    shopItem = CreateItem(parentObject, item, randomIndex);
                    ShopItemButtonFunction(shopItem.GetComponent<Button>());
                    i++;
                    ordinaryCount++;
                }
                else if (item.ItemRarityEnum == ItemRarityEnum.RareCard && rareCount < rareMaxCount)
                {
                    shopItem = CreateItem(parentObject, item, randomIndex);
                    ShopItemButtonFunction(shopItem.GetComponent<Button>());
                    i++;
                    rareCount++;
                }
                else if (item.ItemRarityEnum == ItemRarityEnum.LegendaryCard && legendaryCount < legendaryMaxCount)
                {
                    shopItem = CreateItem(parentObject, item, randomIndex);
                    ShopItemButtonFunction(shopItem.GetComponent<Button>());
                    i++;
                    legendaryCount++;
                }
            }
        }

        //Verilen random verilen bir kart objesini oluşturması.
        private GameObject CreateItem(Transform parentObject,ItemController itemController,int randomIndex)
        {
            GameObject itemPrefab= itemController.gameObject;
            GameObject createdItemObject = GameObject.Instantiate(itemPrefab,parentObject);
            createdItemObject.name = itemPrefab.name;
            createdItemObject.GetComponent<CardMovement>().enabled = false;
            MarketManager.Instance.AddItemShopList(createdItemObject,10);
            return createdItemObject;
        }


        public void BagItemAdjusment(Transform parentObject)
        {
            for (int i = 0; i < GameManager.Instance.Equipments.Count; i++)
            {
                Button button = SetEquipmentsPostion(parentObject,i).GetComponent<Button>();

                BagItemButtonFunction(button);

            }
        }

        private GameObject SetEquipmentsPostion(Transform parentObject,int index)
        {
            GameObject item = SetActiveEquipment(parentObject,index);
            item.SetActive(true);

            return item;
        }
        
        private GameObject SetActiveEquipment(Transform parentObject,int index)
        {
            GameObject item = GameManager.Instance.Equipments[index];

            item.transform.position = Vector3.zero;
            item.transform.SetParent(parentObject);
            item.GetComponent<CardMovement>().enabled = false;
            ItemUI itemUI = item.GetComponent<ItemUI>();
            return item;
        }

        private void BagItemButtonFunction(Button button)
        {
            if (button.onClick == null)
            {
                button.onClick = new Button.ButtonClickedEvent();
            }
            button.onClick.RemoveAllListeners();
            

            button.onClick.AddListener(delegate
            {
                ItemController item = button.GetComponent<ItemController>(); 
                string marketPanelName = MarketManager.Instance.MarketUI.MarketPanelNameControl();
                item.bagItemButtonClick(marketPanelName);

            });
        }

        private void ShopItemButtonFunction(Button button)
        {
            if (button.onClick == null)
            {
                button.onClick = new Button.ButtonClickedEvent();
            }
            button.onClick.RemoveAllListeners();
            

            button.onClick.AddListener(delegate
            {
                ItemController item = button.GetComponent<ItemController>(); 
                string marketPanelName = MarketManager.Instance.MarketUI.MarketPanelNameControl();
                item.shopItemButtonClick(marketPanelName);
            });
        }

        public void ItemPrice()
        {
            for (int i = 0; i < GameManager.Instance.GameAllCards.Count; i++)
            {
                FindItemRarity(GameManager.Instance.GameAllCards[i]);
            }
        }


        private void FindItemRarity(GameObject item)
        {
            ItemController itemController = ItemRarirty(item);
            SetItemPrice(itemController);
        }

        private void SetItemPrice(ItemController itemController)
        {
            ItemUI itemUI = itemController.GetComponent<ItemUI>();
            int itemPrice = 0;
            switch(itemController.ItemRarityEnum)
            {
                case ItemRarityEnum.OrdinaryCard :
                    itemPrice = Random.Range(100,300);
                break;
                case ItemRarityEnum.RareCard:
                    itemPrice = Random.Range(200,500);
                break;
                case ItemRarityEnum.LegendaryCard:
                    itemPrice = Random.Range(500,1500);
                break;
                default:
                break;
            } 
            
            itemController.ItemPrice = itemPrice;


            MarketManager.Instance.MarketItemScriptableObject.AddItem(itemController.gameObject.name,itemPrice);
            itemUI.SetActiveItemUI(true,itemController.gameObject.name,itemPrice);
        }

        private ItemController ItemRarirty(GameObject item)
        {
            ItemController itemController = item.GetComponent<ItemController>();
            switch(item.tag)
            {
                case "AttackCard":
                    SetItemRarity(itemController,item.GetComponent<AttackCardController>().CardLegendary);
                break;
                case "DefenceCard":
                    SetItemRarity(itemController,item.GetComponent<DefenceCardController>().CardLegendary);
                break;
                case "AbilityCard":
                    SetItemRarity(itemController,item.GetComponent<AbilityCardController>().CardLegendary);
                break;
                case "StrenghCard":
                    SetItemRarity(itemController,item.GetComponent<StrengthCardController>().CardLegendary);
                break;
                default:
                break;
            }

            return itemController;
        }

        private void SetItemRarity(ItemController item,CardLegendaryEnum cardRarity)
        {
            ItemRarityEnum itemRarity =  ItemRarityEnum.None;
            switch(cardRarity)
            {
                case CardLegendaryEnum.OrdinaryCard :
                    itemRarity = ItemRarityEnum.OrdinaryCard;
                break;
                
                case CardLegendaryEnum.RareCard :
                    itemRarity = ItemRarityEnum.RareCard;
                break;
                
                case CardLegendaryEnum.LegendaryCard :
                    itemRarity = ItemRarityEnum.LegendaryCard;
                break;
                default:
                break;
            }

            item.ItemRarityEnum = itemRarity;
        }

        
        
        public void ShopItemsReset()
        {
            MarketManager.Instance.ShopItems.Clear();
        }

    }


}
