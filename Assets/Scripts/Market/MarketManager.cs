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
    public List<GameObject> BagEquipments {get => bagEquipments; set => bagEquipments = value;}
    
  

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

    private bool value =false;
    public void BuyItemSave()
    {
        if(!value)
        {
            string c = SaveSystem.DataExtraction("buyItems","");
            if(c != null)
            {
                if(c != "")
                {
                    string[] d = c.Split(",");
                    string[] e = new string[d.Length + shopItems.Count];
                    
                    int i= 0;
                    int j = 0;
                    bool sorgu = false;
                    while(i < e.Length)
                    {
                        if(!sorgu)
                        {
                            if(j < d.Length)
                            {
                                e[i] = d[j];
                                i++;
                                j++;
                            }
                            else
                            {
                                j = 0;
                                sorgu = true;
                            }
                        }   
                        else if(sorgu)
                        {
                            if(j < shopItems.Count)
                            {
                                e[i] = FindItemPrefabPath(shopItems[j].gameObject);
                                i++;
                                j++;
                            }
                            else
                            {
                                j = e.Length;
                            }
                        }
                    }

                    string f = string.Join(",", e);
                    SaveSystem.DataRemove("buyItems");
                    SaveSystem.DataSave("buyItems",f);
                }
                else
                {
                    string[] a = new string[shopItems.Count];
                    for (int i = 0; i < shopItems.Count; i++)
                    {
                        a[i] = FindItemPrefabPath(shopItems[i].gameObject);
                    }

                    string b = string.Join(",", a);

                    SaveSystem.DataSave("buyItems", "");

                    SaveSystem.DataSave("buyItems", b);
                }
            }
            
            
            value = true;
        }

        
    }

    public void ResetButton()
    {
        value = false;
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

        bagEquipments.Clear();

        sellItemName = "";
        sellItemName = string.Join(",",sellItemNames);

        SaveSystem.DataSave("buyItems",sellItemName);
        
    }

    

    public string FindItemPrefabPath(GameObject _item)
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
    
    //Market de ki shop listesine eleman eklememizi sağliyor
    public void AddItemShopList(GameObject item,int maxCount)
    {
        if(shop.Count < maxCount)
        {
            shop.Add(item);
        }
        
    }


    public void ClearBagEquipmentsList()
    {
        if(bagEquipments.Count > 0)
        {
            bagEquipments.Clear();
        }
    }



}
