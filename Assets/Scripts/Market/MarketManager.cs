using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Item;
using Market;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    private static MarketManager instance;
    public static MarketManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<MarketManager>();
            }
            return instance;
        }
    }

    

    private  MarketItemAdjustment marketItemAdjustment;
    public  MarketItemAdjustment MarketItemAdjustment=> marketItemAdjustment;

    [SerializeField] private MarketItemScriptableObject marketItemScriptableObject;
    public MarketItemScriptableObject MarketItemScriptableObject => marketItemScriptableObject;

    [SerializeField] private MarketUI marketUI;
    public MarketUI MarketUI => marketUI;


    [SerializeField] private List<GameObject> shop = new List<GameObject>();
    public List<GameObject> Shop => shop;



    [SerializeField] private List<ItemController> shopItems = new List<ItemController>();
    public List<ItemController> ShopItems => shopItems; 

    
    [SerializeField] private List<GameObject> bagEquipments = new List<GameObject>();
    public List<GameObject> BagEquipments => bagEquipments;
    
  

    [SerializeField] private MarketTransaction marketTransaction;
    public MarketTransaction MarketTransaction => marketTransaction;    

    private 



    void OnEnable()
    {
        marketUI = GetComponent<MarketUI>();    
    }

    
    void Awake()
    {

    }

    void Start()
    {

        if(marketItemAdjustment == null)
        {
            marketItemAdjustment = new MarketItemAdjustment();
        }



        if(marketItemScriptableObject.ItemNames.Count != GameManager.Instance.GameAllCards.Count)
        {
            marketItemAdjustment.ItemPrice();
        }
        else
        {
            for (int i = 0; i < marketItemScriptableObject.ItemPrices.Count; i++)
            {
                GameManager.Instance.GameAllCards[i].GetComponent<ItemController>().ItemPrice = marketItemScriptableObject.ItemPrices[i];
            }
        }

        if(marketTransaction == null)
        {
            marketTransaction = new MarketTransaction();
        }

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            GameManager.Instance.CrystalCoinWin(12000);

        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            string a = SaveSystem.DataExtraction("buyItems","");
            string[] b = a.Split(","); 
            foreach (var item in b)
            {
                print(item);
            }
        }


        if(Input.GetKeyDown(KeyCode.X))
        {
            List<string> path = new List<string>();
            for (int i = 0; i < shopItems.Count; i++)
            {
                path.Add(FindItemPrefabPath(shopItems[i].gameObject));
            }

            string _path = string.Join(",",path);
            SaveSystem.DataSave("buyItems", _path);
        }
    }



    public void AddShopItem(ItemController item)
    {
        if(!shopItems.Contains(item))
            shopItems.Add(item);
    }

    public void RemoveShopItem(ItemController item)
    {
        if(shopItems.Contains(item))
            shopItems.Remove(item);
    }
    
    public void ClearShopItem()
    {
        shopItems.Clear();
    }


    public void AddBagItem(GameObject item)
    {
        if(!bagEquipments.Contains(item))
            bagEquipments.Add(item);
    }

    public void RemoveBagItem(GameObject item)
    {
        if(bagEquipments.Contains(item))
            bagEquipments.Remove(item);
    }
    
    public void ClearBagItem()
    {
        bagEquipments.Clear();
    }

    [SerializeField] string[] newBuyItems;
    public void BuyItemSave()
    {
        string buyItem = SaveSystem.DataExtraction("buyItems","");
        string[] buyItems = buyItem.Split(',');

        int buyItemsLength = buyItem.Split(",").Length;
        
        newBuyItems = new string[buyItemsLength + shopItems.Count];

        int buyItemIndex = 0;

        for (int i = 0; i < buyItems.Length; i++)
        {
            newBuyItems[i] = buyItems[i];
        }

        
        for (int i = buyItemsLength; i < newBuyItems.Length; i++)
        {
            newBuyItems[i] = FindItemPrefabPath(shopItems[buyItemIndex].gameObject);
            if(buyItemIndex != 0)
            {
                if(GameManager.Instance.Equipments[buyItemIndex-1] != shopItems[buyItemIndex])
                {
                    GameManager.Instance.Equipments.Add(shopItems[buyItemIndex].gameObject);
                    buyItemIndex++;
                }

            }

        }       

       
    }


    public void BuyItemRemove(List<GameObject> items)
    {
        string sellItemName = SaveSystem.DataExtraction("buyItems","");
        
        
        List<string> sellItemNames = sellItemName.Split(',').ToList();

        for (int i = 0; i < items.Count; i++)
        {
            GameManager.Instance.RemoveItemEquipments(items[i]);
            for (int j = 0; j < sellItemNames.Count; j++)
            {
                string itemPath = FindItemPrefabPath(items[i]);
                if (itemPath == sellItemNames[j])
                {
                    sellItemNames.Remove(itemPath);
                }
            }

        }

        sellItemName = "";
        sellItemName = string.Join(",",sellItemNames);

        SaveSystem.DataSave("buyItems",sellItemName);
        
    }

    

    private string FindItemPrefabPath(GameObject _item)
    {
        switch(_item.tag)
        {
            case "AttackCard":
                return $"Prefabs/Cards/AttackCards/{_item.name}";            

            case "DefenceCard":
                return $"Prefabs/Cards/DefenceCards/{_item.name}";            
            
            case "AbilityCard":
                return $"Prefabs/Cards/AbilityCards/{_item.name}";            
            
            case "StrenghCard":
                return $"Prefabs/Cards/StrengthCards/{_item.name}";            
            default:
                return "";            

        }
        
    }

    private void QueryEarnedItem(string itemPath)
    {
        string earnedItemName = SaveSystem.DataExtraction("earnedCardNames","");
        string[] earnedItemNames = earnedItemName.Split(",");
        earnedItemName = "";
        for (int i = 0; i < earnedItemNames.Length; i++)
        {
            if(earnedItemNames[i] != itemPath) earnedItemName+=earnedItemNames[i];
            else continue;
        }

        print(earnedItemName);
    }

    private void QueryBuyItem(string itemPath)
    {
        string buyItemName = SaveSystem.DataExtraction("buyItems","");
        string[] buyItemNames = buyItemName.Split(",");
        buyItemName = "";

        for (int i = 0; i < buyItemNames.Length; i++)
        {
            if(buyItemNames[i] != itemPath) buyItemName+=buyItemNames[i];
            else 
                continue;
        }

        print(buyItemName);
    }

    public void AddItemEquiments(GameObject item)
    {
        GameManager.Instance.AddItemEquiptmens(item);
    }

    public void AddItemShopList(GameObject item)
    {
        shop.Add(item);
    }

}
