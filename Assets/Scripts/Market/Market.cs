using System.Collections;
using System.Collections.Generic;
using Card_Enum;
using Products;
using ScriptableObjects.Market;
using ShopItemDegreeEnums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Shop
{

    public class Market : MonoBehaviour
    {
        [SerializeField] private GameObject shopPanel;

        [SerializeField] private GameObject bagPanel;

        [SerializeField] private Button marketBuy_Button;

        [SerializeField] private Button marketSell_Button;

        [SerializeField] private Button marketRefreshButton;

        #region  Shop
        [SerializeField] private List<GameObject> products = new List<GameObject>();
        [SerializeField] private ShopItem[] shopItemPosition;
        [SerializeField] private int[] number;
        private List<GameObject> buyProducts;
        private GameObject buyProduct;


        #endregion

        #region  Equipment
        [SerializeField] private ShopItem[] bagItemPositions;
        [SerializeField] private GameObject sellProduct;
        #endregion

        #region  Price
        [SerializeField] private Transform priceTextParent;
        [SerializeField] private TextMeshProUGUI[] priceTexts;


        #endregion

        #region  ScriptableObject

        [SerializeField] private ShopProduct_ScriptableObject shopProduct_ScriptableObject; 

        #endregion


        private void Awake()
        {
            SetPriceText();
        }


        private void Update() 
        {
            
        }

        //shop panel position objectlerin derecesini belirliyor
        public void SetShopItemDegree(int index,GameObject productCard)
        {
            ShopItemDegreeEnum shopItemDegreeEnum = shopItemPosition[index].ShopItemDegree;
            FindProductCardType(productCard,ref shopItemDegreeEnum);
            shopItemPosition[index].ShopItemDegree = shopItemDegreeEnum;

        }

      

        //shop paneldeki ürünleri belirleyen method
        public void MarketProductPlacement()
        {
            bool currentProducts = false;
            ProductCountReset();
            buyProducts = new List<GameObject>();

            for (int i = 0; i < shopItemPosition.Length; i++)
            {
                int randomNumber = Random.Range(0, GameManager.Instance.GameAllCards.Count);
                if (!shopItemPosition[i].IsProduct)
                {
                    shopItemPosition[i].IsProduct = true;
                    if(IncreaseProductCardType(GameManager.Instance.GameAllCards[randomNumber]))
                    {
                        randomNumber = Random.Range(0, GameManager.Instance.GameAllCards.Count);
                    }
                    
                    GameObject _buyProduct = Instantiate(GameManager.Instance.GameAllCards[randomNumber]);
                    ProductControl productControl = _buyProduct.GetComponent<ProductControl>();
                    if(productControl == null)
                    {
                        _buyProduct.AddComponent<ProductControl>();
                    }
                    
                    _buyProduct.name = GameManager.Instance.GameAllCards[randomNumber].name;
                    _buyProduct.transform.SetParent(shopItemPosition[i].transform);
                    _buyProduct.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    _buyProduct.transform.localScale = Vector3.one;
                    SetShopItemDegree(i,_buyProduct);
                    SetProductPriceTextValue(i,_buyProduct.GetComponent<ProductControl>());


                    products.Add(_buyProduct);

                    Button buyProductButton = _buyProduct.GetComponent<Button>();
                    buyProductButton.interactable = true;

                    if (buyProductButton.onClick == null)
                    {
                        buyProductButton.onClick = new Button.ButtonClickedEvent();
                    }
                    buyProductButton.onClick.RemoveAllListeners();

                    buyProductButton.onClick.AddListener(() =>
                    {
                        buyProduct = _buyProduct.gameObject;
                        buyProducts.Add(_buyProduct);
                    });

                }
                else
                {
                    currentProducts = true;
                    break;
                }
            }

            if(currentProducts)
            {
                for (int i = 0; i < products.Count; i++)
                {
                    SetShopItemDegree(i, products[i]);
                    SetProductPriceTextValue(i, products[i].GetComponent<ProductControl>());
                    if (i == products.Count - 1)
                    {
                        currentProducts = false;
                    }
                }

            }

            
        }

        
        public void MarketActivation()
        {
            gameObject.SetActive(true);
        }
        
        //market panelin'den çikmayi sağliyor
        public void MarketPassivity()
        {
            ProductCountReset();
            ClearProductObject();
            PriceTextAllReset();

            marketBuy_Button.gameObject.SetActive(false);
            marketSell_Button.gameObject.SetActive(false);


            gameObject.SetActive(false);
            //UIManager.Instance.MarketIcon_Button.SetActive(true);
            BuyProductDelete();
            if (bagPanel.activeSelf)
            {
                for (int i = 0; i < bagItemPositions.Length; i++)
                {
                    if (i < bagItemPositions.Length - 1)
                    {
                        try
                        {
                            GameObject chieldProduct = bagItemPositions[i].transform.GetChild(0).gameObject;
                            chieldProduct.transform.position = Vector3.zero;
                            chieldProduct.GetComponent<Button>().interactable = false;
                            chieldProduct.name = bagItemPositions[i].transform.GetChild(0).name;
                            ProductControl productControl = chieldProduct.GetComponent<ProductControl>();
                            
                            if(productControl != null)
                            {
                                if(productControl.ProductEnum == ProductEnum.Earned)
                                {
                                    chieldProduct.transform.SetParent(UIManager.Instance.EarnedGameObject.transform);
                                }
                                else
                                {
                                    chieldProduct.transform.SetParent(UIManager.Instance.DectGameObject.transform);
                                }
                            }
                            
                            chieldProduct.transform.SetSiblingIndex(0);
                            chieldProduct.GetComponent<Button>().interactable = false;
                            chieldProduct.SetActive(false);
                        }
                        catch
                        {
                        }
                    }

                }
            }
            bagPanel.SetActive(false);
            shopPanel.SetActive(false);
        }

        //shop panelin deki ürünleri yeniden farkli ürünler olmasini sağliyor
        public void MarketRefreshButton()
        {
            products.Clear();
            BuyProductDelete();
            foreach (ShopItem item in shopItemPosition)
            {
                item.IsProduct = false;
            }
            MarketProductPlacement();
        }

        //satin alınan ürünleri liste'den silmeyi sağliyor
        private void BuyProductDelete()
        {
            if (shopPanel.activeSelf)
            {
                for (int i = 0; i < shopItemPosition.Length; i++)
                {
                    try
                    {
                        if (shopItemPosition[i].transform.GetChild(0) != null)
                        {
                            shopItemPosition[i].IsProduct = false;
                            Destroy(shopItemPosition[i].transform.GetChild(0).gameObject);
                        }
                    }
                    catch
                    {
                    }
                }

            }
        }

        //market buttonlari panele göre aktive ediyor
        private void MarketButtonsActivation(string buttonName, bool activation)
        {
            switch (buttonName)
            {
                case "buy":
                    marketBuy_Button.gameObject.SetActive(activation);
                    marketSell_Button.gameObject.SetActive(false);
                    break;
                case "sell":
                    marketSell_Button.gameObject.SetActive(activation);
                    marketBuy_Button.gameObject.SetActive(false);
                    break;
                default:
                    break;

            }
        }

        //rasgele kartlari shop paneline göstermeyi sağliyan method
        public void MarketShopButton()
        {

            shopPanel.SetActive(!shopPanel.activeSelf);
            if (bagPanel.activeSelf)
            {
                bagPanel.SetActive(false);
            }

                MarketButtonsActivation("buy", shopPanel.activeSelf);
            if(shopPanel.activeSelf)
            {
                MarketProductPlacement();
            }
            else if (!shopPanel.activeSelf)
            {   
                
                PriceTextAllReset();
                return;
            }

            marketRefreshButton.gameObject.SetActive(true);
            
            
        
        }

        //mevcut bulunan kartlari bag paneline göstermeyi sağliyan method
        public void MarketMyBagButton()
        {
            marketRefreshButton.gameObject.SetActive(false);
            PriceTextAllReset();
            bagPanel.SetActive(!bagPanel.activeSelf);
            marketSell_Button.gameObject.SetActive(bagPanel.activeSelf);

            if (shopPanel.activeSelf)
            {
                shopPanel.SetActive(false);
            }
            if(bagPanel.activeSelf)
            {
                MarketButtonsActivation("sell", bagPanel.activeSelf);

                for (int i = 0; i < GameManager.Instance.Equipments.Count; i++)
                {
                    if (i < bagItemPositions.Length)
                    {
                        GameManager.Instance.Equipments[i].transform.SetParent(bagItemPositions[i].transform);
                        GameManager.Instance.Equipments[i].transform.localRotation = Quaternion.identity;
                        GameManager.Instance.Equipments[i].transform.localScale =Vector2.one; 
                        
                        ProductControl eqipment = GameManager.Instance.Equipments[i].GetComponent<ProductControl>();
                        SetProductPriceTextValue(i,eqipment);

                        RectTransform rectTransform = GameManager.Instance.Equipments[i].GetComponent<RectTransform>();

                        Animator productAnimator = rectTransform.GetComponent<Animator>();

                        rectTransform.gameObject.SetActive(true);

                        Button productButton = rectTransform.GetComponent<Button>();
                        if(productButton != null)
                        {
                            productButton.interactable = true;
                            if (productButton.onClick == null)
                            {
                                productButton.onClick = new Button.ButtonClickedEvent();
                            }


                            productButton.onClick.RemoveAllListeners();
                            productButton.onClick.AddListener(delegate
                            {
                                sellProduct = productButton.gameObject;
                            });
                        }
                        

                        if (productAnimator != null)
                        {
                            productAnimator.enabled = false;
                        
                        }


                        rectTransform.anchoredPosition = Vector2.zero;
                    }
                }
            }
            else
            {
                PriceTextAllReset();
            }
        }

        //market deki seçtiğiniz ürünü almanizi sağliyan button fonksiyonu
        public void MarketBuyButton()
        {
            int priceIndex = buyProduct.transform.parent.transform.GetSiblingIndex();
            if(GameManager.Instance.CrystalCount > int.Parse(priceTexts[priceIndex].text))
            {
                GameManager.Instance.CrystalCoinLose(int.Parse(priceTexts[priceIndex].text));
                buyProduct.SetActive(false);
                PriceTextReset(priceIndex);
                RemoveBagCardObject(buyProduct.gameObject.name);
                buyProduct.transform.SetParent(UIManager.Instance.EarnedGameObject);
                GameManager.Instance.CardSave(buyProduct);
                buyProduct = null;
                buyProducts.Clear();
            }
            else
            {
                print("paran yetmiyor fakir");
            }
        }

        //market deki seçtiğiniz ürünü satmayi sağliyan button fonksiyonu
        public void MarketSellButton()
        {
            GameManager.Instance.CrystalCoinWin(sellProduct.GetComponent<ProductControl>().Price);
            GameManager.Instance.CardDelete(sellProduct);
            int siblingIndex = sellProduct.transform.parent.transform.GetSiblingIndex();

            priceTexts[siblingIndex].text = "";
            priceTexts[siblingIndex].gameObject.SetActive(false);

            RemoveProductObject(sellProduct.GetComponent<ProductControl>());
            
            Destroy(sellProduct.gameObject);
            
            GameManager.Instance.CardDelete(sellProduct);

        }

        //market deki seçtiğiniz ürünü aldiğiniz veya sattiğiniz ürünü silmeyi sağliyor
        private void RemoveProductObject(ProductControl product)
        {
            if(product.ProductEnum == ProductEnum.Equipment)
            {
                print(product.name);
                for (int i = 0; i < GameManager.Instance.GameDates.DecCards.Count; i++)
                {
                    if(product.name == GameManager.Instance.GameDates.DecCards[i])
                    {
                        GameManager.Instance.GameDates.DecCards.Remove(product.name);
                    }
                }
            }
            else if(product.ProductEnum == ProductEnum.Earned)
            {
                GameManager.Instance.CardDelete(sellProduct);
            }
            GameManager.Instance.Equipments.Remove(sellProduct);
            
        }

        
        public void SetProductPriceTextValue(int productTextIndex,ProductControl product)
        {
            
            priceTexts[productTextIndex].gameObject.SetActive(true);
            for (int i = 0; i < shopProduct_ScriptableObject.ProductPriceNames.Count; i++)
            {
                if(shopProduct_ScriptableObject.ProductPriceNames[i] == product.name)
                {
                    product.Price =shopProduct_ScriptableObject.ProductPrices[i];
                    priceTexts[productTextIndex].text = product.Price.ToString();
                }
            }
        }

        

        private void RemoveBagCardObject(string productName)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if(products[i].name == productName)
                {
                    products.Remove(products[i]);
                }
            }
        }

        private void ClearProductObject()
        {
            for (int i = 0; i < products.Count; i++)
            {
                Destroy(products[i].gameObject);
            }
            MarketRefreshButton();
        }


        private bool IncreaseProductCardType(GameObject product)
        {
            
            switch(product.tag)
            {
                case "AttackCard":
                    AttackCardController attackProductCard = product.GetComponent<AttackCardController>();
                    if(attackProductCard != null)
                    {
                        if(attackProductCard.CardLegendary == CardLegendaryEnum.LegendaryCard)
                        {
                            ProductTypeIdentification(attackProductCard.CardLegendary);
                        }
                    }
                break; 
                case "DefenceCard":
                    DefenceCardController defenceProductCard = product.GetComponent<DefenceCardController>();
                    if (defenceProductCard != null)
                    {
                        if(defenceProductCard.CardLegendary == CardLegendaryEnum.LegendaryCard)
                        {
                            ProductTypeIdentification(defenceProductCard.CardLegendary);

                        }
                    }
                    break;
                case "AbilityCard":
                    AbilityCardController abilityProductCard = product.GetComponent<AbilityCardController>();
                    if (abilityProductCard != null)
                    {
                        if(abilityProductCard.CardLegendary == CardLegendaryEnum.LegendaryCard)
                        {
                            ProductTypeIdentification(abilityProductCard.CardLegendary);
                        }
                    }
                    break;
                case "StrenghCard":
                    StrengthCardController strenghProductCard = product.GetComponent<StrengthCardController>();
                    if (strenghProductCard != null)
                    {
                        if(strenghProductCard.CardLegendary == CardLegendaryEnum.LegendaryCard)
                        {
                            ProductTypeIdentification(strenghProductCard.CardLegendary);
                        }
                    }
                break;
                default:
                break;
            }

            return QueryProductCount();
        }
        
        

        private bool QueryProductCount()
        {
            return shopProduct_ScriptableObject.LegendaryProductCount < shopProduct_ScriptableObject.LegendaryProductCountMax ? true : false;
        }

        private void FindProductCardType(GameObject product,ref ShopItemDegreeEnum shopItemDegreeEnum)
        {
            
            switch(product.tag)
            {
                case "AttackCard":
                    AttackCardController attackProductCard = product.GetComponent<AttackCardController>();
                    if(attackProductCard != null)
                    {
                        shopItemDegreeEnum = FindProductCardDegree(attackProductCard.CardLegendary);
                    }
                break;
                case "DefenceCard":
                    DefenceCardController defenceProductCard = product.GetComponent<DefenceCardController>();
                    if (defenceProductCard != null)
                    {
                        shopItemDegreeEnum = FindProductCardDegree(defenceProductCard.CardLegendary);
                    }
                    break;
                case "AbilityCard":
                    AbilityCardController abilityProductCard = product.GetComponent<AbilityCardController>();
                    if (abilityProductCard != null)
                    {
                        shopItemDegreeEnum = FindProductCardDegree(abilityProductCard.CardLegendary);
                    }
                    break;
                case "StrenghCard":
                    StrengthCardController strenghProductCard = product.GetComponent<StrengthCardController>();
                    if (strenghProductCard != null)
                    {
                        shopItemDegreeEnum = FindProductCardDegree(strenghProductCard.CardLegendary);
                    }
                break;
                default:
                break;
            }

        }

        private CardLegendaryEnum FindProductCardType(GameObject product)
        {
            switch(product.tag)
            {
                case "AttackCard":
                    AttackCardController attackProductCard = product.GetComponent<AttackCardController>();
                    return attackProductCard != null ? attackProductCard.CardLegendary : CardLegendaryEnum.None;
                case "DefenceCard":
                    DefenceCardController defenceProductCard = product.GetComponent<DefenceCardController>();
                    return defenceProductCard != null ? defenceProductCard.CardLegendary : CardLegendaryEnum.None;
                case "AbilityCard":
                    AbilityCardController abilityProductCard = product.GetComponent<AbilityCardController>();
                    return abilityProductCard != null ? abilityProductCard.CardLegendary : CardLegendaryEnum.None;
                default:
                    StrengthCardController strenghProductCard = product.GetComponent<StrengthCardController>();
                    return strenghProductCard != null ? strenghProductCard.CardLegendary : CardLegendaryEnum.None;
            }

        }
        

        private ShopItemDegreeEnum FindProductCardDegree(CardLegendaryEnum productCard)
        {
            ShopItemDegreeEnum shopItemDegreeEnum = ShopItemDegreeEnum.None;
            switch(productCard)
            {
                case CardLegendaryEnum.OrdinaryCard:
                    shopItemDegreeEnum = ShopItemDegreeEnum.Ordinary;
                break;
                case CardLegendaryEnum.RareCard:
                    shopItemDegreeEnum = ShopItemDegreeEnum.Rare;
                break;
                case CardLegendaryEnum.LegendaryCard:
                    shopItemDegreeEnum = ShopItemDegreeEnum.Legendary;
                break;
                default:
                break;
            }

            return shopItemDegreeEnum;
        }

        private void SetPriceText()
        {
            priceTexts = new TextMeshProUGUI[priceTextParent.childCount];
            bool queryListCount = shopProduct_ScriptableObject.ProductPrices.Count < GameManager.Instance.GameAllCards.Count && shopProduct_ScriptableObject.ProductPriceNames.Count < GameManager.Instance.GameAllCards.Count;
            if(queryListCount)
            {
                for (int i = 0; i < GameManager.Instance.GameAllCards.Count; i++)
                {
                    shopProduct_ScriptableObject.ProductPrices.Add(0);
                    shopProduct_ScriptableObject.ProductPriceNames.Add("");
                }

            }

            for (int i = 0; i < priceTextParent.childCount; i++)
            {
                priceTexts[i] = priceTextParent.GetChild(i).GetComponent<TextMeshProUGUI>();
            }
            for (int i = 0; i < GameManager.Instance.GameAllCards.Count; i++)
            {
                if(QueryProductGameObject(GameManager.Instance.GameAllCards[i]))
                {
                    SetProductFeature(i);
                }
            }
        }

        private void SetProductFeature(int i)
        {
            shopProduct_ScriptableObject.ProductPriceNames[i] = GameManager.Instance.GameAllCards[i].name;
            shopProduct_ScriptableObject.ProductPrices[i] = Random.Range(shopProduct_ScriptableObject.Products[IndexProductDegree(FindProductCardType(GameManager.Instance.GameAllCards[i]))].minProductPrice, shopProduct_ScriptableObject.Products[IndexProductDegree(FindProductCardType(GameManager.Instance.GameAllCards[i]))].maxProductPrice);
        }

        private bool QueryProductGameObject(GameObject product)
        {
            for (int j = 0; j < shopProduct_ScriptableObject.ProductPriceNames.Count; j++)
            {
                if(shopProduct_ScriptableObject.ProductPriceNames[j] == product.name)
                {
                    return false;
                }
            }
            return true;
        }

        private int IndexProductDegree(CardLegendaryEnum cardDegree)
        {
            switch(cardDegree)
            {
                case CardLegendaryEnum.RareCard:
                return 1;
                case CardLegendaryEnum.LegendaryCard:
                return 2;
                default:
                return 0;
            }
        }


        


        private void PriceTextReset(int priceTextIndex)
        {
            priceTexts[priceTextIndex].text = "";
            priceTexts[priceTextIndex].gameObject.SetActive(false);
        }

        private void PriceTextAllReset()
        {
            for (int i = 0; i < priceTexts.Length; i++)
            {
                priceTexts[i].text = "";
            }
        }


        private void ProductTypeIdentification(CardLegendaryEnum cardLegendaryEnum)
        {
            switch(cardLegendaryEnum)
            {
                case CardLegendaryEnum.LegendaryCard:
                    if(shopProduct_ScriptableObject.LegendaryProductCount< shopProduct_ScriptableObject.LegendaryProductCountMax)
                    {
                        shopProduct_ScriptableObject.LegendaryProductCount++;
                    }
                break;
                case CardLegendaryEnum.RareCard:
                    if(shopProduct_ScriptableObject.RareProductCount< shopProduct_ScriptableObject.RareProductCountMax)
                    {
                        shopProduct_ScriptableObject.RareProductCount++;
                    }
                break;
                case CardLegendaryEnum.OrdinaryCard:
                    if(shopProduct_ScriptableObject.OrdinaryProductCount< shopProduct_ScriptableObject.OrdinaryProductCountMax)
                    {
                        shopProduct_ScriptableObject.OrdinaryProductCount++;
                    }
                break;
                default:
                break;
            }
        }

        private void ProductCountReset()
        {
            shopProduct_ScriptableObject.LegendaryProductCount = 0;
            shopProduct_ScriptableObject.RareProductCount = 0;
            shopProduct_ScriptableObject.OrdinaryProductCount = 0;
        }

        private void OnApplicationQuit() 
        {
            ProductCountReset();
        }
       
    }
}
