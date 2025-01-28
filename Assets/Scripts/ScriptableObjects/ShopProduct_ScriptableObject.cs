using System;
using System.Collections.Generic;
using ShopItemDegreeEnums;
using UnityEngine;

namespace ScriptableObjects.Market
{
    [CreateAssetMenu(fileName = "Shop Product ScriptableObject", menuName = "Create Shop Product ScriptableObject", order = 2)]
    internal class ShopProduct_ScriptableObject : ScriptableObject
    {
        [SerializeField] private ProductDegree[] products;
        public ProductDegree[] Products { get { return products; } }

        [Space]    
        
        [SerializeField] private int legendaryProductCount;
        public int LegendaryProductCount { get { return legendaryProductCount; }  set { legendaryProductCount = value;}}

        [SerializeField] private int legendaryProductCountMax;
        public int LegendaryProductCountMax { get {return legendaryProductCountMax;}}   
        
        [Space]    
        
        [SerializeField] private int rareProductCount;
        public int RareProductCount { get { return rareProductCount; } set {rareProductCount = value;} }

        [SerializeField]private int rareProductCountMax;
        public int RareProductCountMax { get {return rareProductCountMax;}}

        [Space]    
        
        [SerializeField] private int ordinaryProductCount;
        public int OrdinaryProductCount { get { return ordinaryProductCount; } set {ordinaryProductCount = value;}}

        [SerializeField] private int ordinaryProductCountMax;
        public int OrdinaryProductCountMax { get {return ordinaryProductCountMax;}}

        [Space]

        [SerializeField]private List<string> productPriceNames;
        public List<string> ProductPriceNames { get { return productPriceNames;} set {productPriceNames = value;} }

        [SerializeField]private List<int> productPrices;
        public List<int> ProductPrices { get { return productPrices;} set {productPrices = value;} }

    }

    [Serializable]    
    internal class ProductDegree
    {
        
        public ShopItemDegreeEnum productDegreeEnum;        
        public int minProductPrice;

        public int maxProductPrice;

    }

    
}
