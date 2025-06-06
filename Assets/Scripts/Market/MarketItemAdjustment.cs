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
            Dictionary<string, ItemController> keyValues = new Dictionary<string, ItemController>();
            HashSet<string> usedNames = new HashSet<string>();

            int shopItemCount = _itemsCount;
            int i = 0;

            List<ItemController> ordinaryItems = new List<ItemController>();
            List<ItemController> rareItems = new List<ItemController>();
            List<ItemController> legendaryItems = new List<ItemController>();

            foreach (GameObject card in GameManager.Instance.GameAllCards)
            {
                ItemController item = card.GetComponent<ItemController>();

                if (item == null) continue;

                switch (item.ItemRarityEnum)
                {

                    case ItemRarityEnum.OrdinaryCard:
                        ordinaryItems.Add(item);
                        break;
                    case ItemRarityEnum.RareCard:
                        rareItems.Add(item);
                        break;
                    case ItemRarityEnum.LegendaryCard:
                        legendaryItems.Add(item);
                        break;
                }


            }


            int ordinaryMax = shopItemCount < 10 ? shopItemCount : (shopItemCount * 70) / 100;
            int rareMax = shopItemCount < 10 ? 1 : (shopItemCount * 20) / 100;
            int legendaryMax = shopItemCount < 10 ? 0 : (shopItemCount * 10) / 100;

            int ordinaryCreateItem = 0;
            int rareCreateItem = 0;
            int legendaryCreateItem = 0;

            i += AddItemsFromList(usedNames, parentObject, ordinaryItems, ordinaryMax,ordinaryCreateItem,3);
            i += AddItemsFromList(usedNames, parentObject, rareItems, rareMax,rareCreateItem,3);
            i += AddItemsFromList(usedNames, parentObject, legendaryItems, legendaryMax,legendaryCreateItem,3);

            

           
        }

        private int AddItemsFromList(HashSet<string> usedNames, Transform parent, List<ItemController> itemList, int maxCount,int createCount,int createdMaxCount)
        {
            int added = 0;
            int attempts = 0;
            int maxTries = 500;

            while (added < maxCount && attempts < maxTries)
            {
                

                if (itemList.Count == 0) break;

                int randomIndex = Random.Range(0, itemList.Count);

                ItemController itemPrefab = itemList[randomIndex];

                if (usedNames.Contains(itemPrefab.name))
                {
                    if (createCount >= createdMaxCount)
                    {
                        attempts++;
                        continue;
                    }
                    
                }

                ItemController createdItem = CreateShopItem(parent,itemPrefab);

                if (createdItem != null)
                {
                    added++;
                    usedNames.Add(createdItem.name);
                }

                createCount++;
                attempts++;
            }

            return added;
        }

        private ItemController CreateShopItem(Transform parentObject, ItemController item)
        {
            ItemController createdItem = GameObject.Instantiate(item, parentObject);
            createdItem.name = item.name;
            createdItem.GetComponent<CardMovement>().enabled = false;
            GameManager.Instance.CardAnimationController.CardMovementAnimationStop(createdItem.gameObject);
            var tuple = MarketManager.Instance.MarketItemScriptableObject.GetItemPriceAndName(item.name);
            createdItem.ItemFirstCreated(tuple.Item1, tuple.Item2);
            ShopItemButtonFunction(createdItem.GetComponent<Button>());
            return createdItem;

        }

        private GameObject CreateItem(Transform parentObject, ItemController itemController)
        {
            GameObject itemPrefab = itemController.gameObject;
                GameObject createdItemObject = GameObject.Instantiate(itemPrefab, parentObject);
                createdItemObject.name = itemPrefab.name;
                createdItemObject.GetComponent<CardMovement>().enabled = false;
                MarketManager.Instance.AddItemShopList(createdItemObject, 10);
                return createdItemObject;
        }


        public void BagItemAdjusment(Transform parentObject)
        {
            for (int i = 0; i < GameManager.Instance.Equipments.Count; i++)
            {
                Button button = SetEquipmentsPostion(parentObject,i).GetComponent<Button>();
                ItemController itemController = GameManager.Instance.Equipments[i].GetComponent<ItemController>();

                var tuple =  MarketManager.Instance.MarketItemScriptableObject.GetItemPriceAndName(itemController.gameObject.name);

                itemController.ItemFirstCreated(tuple.Item1,tuple.Item2);
                
                BagItemButtonFunction(button);

            }
        }

        private GameObject SetEquipmentsPostion(Transform parentObject,int index)
        {
            GameObject item = SetActiveBagEquipment(parentObject,index);
            GameManager.Instance.CardAnimationController.CardMovementAnimationStop(item);
            item.SetActive(true);

            return item;
        }
        
        private GameObject SetActiveBagEquipment(Transform parentObject,int index)
        {
            GameObject item = GameManager.Instance.Equipments[index];

            item.transform.position = Vector3.zero;
            item.transform.SetParent(parentObject);
            item.GetComponent<CardMovement>().enabled = false;
            ItemUI itemUI = item.GetComponent<ItemUI>();
            return item;
        }

        //Bag sayfasında ki item'ın butonun componentin önceki event'i sıfırlamamizi sağliyor.
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

        
        //Shop sayfasında ki item'ın butonun componentin önceki event'i sıfırlamamizi sağliyor.
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
                FindItemRarity(GameManager.Instance.GameAllCards[i],GameManager.Instance.GameAllCards.Count);
            }
        }


        private void FindItemRarity(GameObject item,int equipmentCount)
        {
            ItemController itemController = ItemRarirty(item);
            SetItemPrice(itemController,equipmentCount);
        }

        //Shop sayfasında ki item'ın nadirliğine göre price değerini set etmemizi sağliyor.
        private void SetItemPrice(ItemController itemController, int equipmentCount)
        {
            ItemUI itemUI = itemController.GetComponent<ItemUI>();
            int itemPrice = 0;
            switch (itemController.ItemRarityEnum)
            {
                case ItemRarityEnum.OrdinaryCard:
                    itemPrice = Random.Range(100, 300);
                    break;
                case ItemRarityEnum.RareCard:
                    itemPrice = Random.Range(200, 500);
                    break;
                case ItemRarityEnum.LegendaryCard:
                    itemPrice = Random.Range(500, 1500);
                    break;
                default:
                    break;
            }

            itemController.ItemPrice = itemPrice;


            MarketManager.Instance.MarketItemScriptableObject.AddItem(itemController.gameObject.name, itemPrice, equipmentCount);
        }

         //Shop da ki olan bir item'ın türüne ve nadirliğine göre rarity'sini set etmemizi sağlıyor.
        private ItemController ItemRarirty(GameObject item)
        {
            ItemController itemController = item.GetComponent<ItemController>();
            switch (item.tag)
            {
                case "AttackCard":
                    SetItemRarity(itemController, item.GetComponent<AttackCardController>().CardLegendary);
                    break;
                case "DefenceCard":
                    SetItemRarity(itemController, item.GetComponent<DefenceCardController>().CardLegendary);
                    break;
                case "AbilityCard":
                    SetItemRarity(itemController, item.GetComponent<AbilityCardController>().CardLegendary);
                    break;
                case "StrenghCard":
                    SetItemRarity(itemController, item.GetComponent<StrengthCardController>().CardLegendary);
                    break;
                default:
                    break;
            }

            return itemController;
        }

        //Shop da ki olan bir item'ın nadirliğine göre rarity'sini set etmemizi sağlıyor.
        private void SetItemRarity(ItemController item, CardLegendaryEnum cardRarity)
        {
            ItemRarityEnum itemRarity = ItemRarityEnum.None;
            switch (cardRarity)
            {
                case CardLegendaryEnum.OrdinaryCard:
                    itemRarity = ItemRarityEnum.OrdinaryCard;
                    break;

                case CardLegendaryEnum.RareCard:
                    itemRarity = ItemRarityEnum.RareCard;
                    break;

                case CardLegendaryEnum.LegendaryCard:
                    itemRarity = ItemRarityEnum.LegendaryCard;
                    break;
                default:
                    break;
            }

            item.ItemRarityEnum = itemRarity;
        }

        
        //Shop sayfasındayken oluşturulan item'ları temizlememizi sağliyor.
        public void ShopItemsReset()
        {
            MarketManager.Instance.ShopItems.Clear();
        }

    }


}
