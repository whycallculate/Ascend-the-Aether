using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Market
{
    [CreateAssetMenu(fileName = "MarketItemScriptableObject", menuName = "Create MarketItemScriptableObject")]
    public class MarketItemScriptableObject : ScriptableObject
    {
        [SerializeField] private int minOrdinaryItemPrice;
        public int MinOrdinaryItemPrice { get { return minOrdinaryItemPrice; } }

        [SerializeField] private int minRareItemPrice;
        public int MinRareItemPrice { get { return minRareItemPrice; } }

        [SerializeField] private int minLegendaryItemPrice;
        public int MinLegendaryItemPrice { get { return minRareItemPrice; } }

        [SerializeField] private List<string> itemNames = new List<string>();
        public List<string> ItemNames { get { return itemNames; } }

        private Dictionary<string, int> items = new Dictionary<string, int>();
        public Dictionary<string, int> Items => items;

        [SerializeField] private List<int> itemPrices = new List<int>();
        public List<int> ItemPrices => itemPrices;
        public void AddItem(string itemName, int itemPrice, int equipmentCount)
        {
            if (itemNames.Count < equipmentCount && itemPrices.Count < equipmentCount)
            {

                itemNames.Add(itemName);
                itemPrices.Add(itemPrice);
            }
        }

        public Tuple<string,int> GetItemPriceAndName(string itemName)
        {
            int itemIndex = 0;
            for (int i = 0; i < itemNames.Count; i++)
            {
                if (itemNames[i] == itemName)
                {
                    itemIndex = i;
                    break;
                }
            }
            return new Tuple<string, int>(itemNames[itemIndex],itemPrices[itemIndex]);
        }

        
    }
}
