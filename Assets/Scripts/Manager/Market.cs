using System.Collections;
using System.Collections.Generic;
using ShopItemDegreeEnums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;

    [SerializeField] private GameObject bagPanel;

    [SerializeField] private Button marketBuy_Button;

    [SerializeField] private Button marketSell_Button;
    
    [SerializeField] private Button marketRefreshButton;

    #region  Shop
    [SerializeField] private ShopItem[] shopItemPosition;
    [SerializeField] private int[] number;
    private GameObject buyProduct;
    private List<GameObject> buyProducts;
    #endregion

    #region  Equipment
    [SerializeField] private ShopItem[] bagItemPositions;
    [SerializeField] private GameObject sellProduct;
    #endregion

    #region  Price
    
    [SerializeField] private TextMeshProUGUI[] priceTexts;
    
    
    #endregion

    
    private void Awake() 
    {
        
    }

    public void SetShopItemDegree()
    {
        number = new int[shopItemPosition.Length];

        for (int i = 0; i < shopItemPosition.Length; i++)
        {
            shopItemPosition[i].ShopItemDegree = SelectRandomShopItemDegree();
        }

        for (int i = 0; i < number.Length; i++)
        {
            number[i] = i;
        }
        MarketProductPlacement();
    }


    private ShopItemDegreeEnum SelectRandomShopItemDegree()
    {
        int randomNumber = Random.Range(1,4);
        switch(randomNumber)
        {
            case 1:
                return ShopItemDegreeEnum.Ordinary;
            case 2:
                return ShopItemDegreeEnum.Rare;
            case 3:
                return ShopItemDegreeEnum.Legendary;
            default:
                return ShopItemDegreeEnum.None;
        }
    }

    //[SerializeField] private List<GameObject> buyProducts = new List<GameObject>();

    private int lastRandomNumber = 0;
    public void MarketProductPlacement()
    {
        buyProducts = new List<GameObject>();
        
        for (int i = 0; i < shopItemPosition.Length; i++)
        {
            int randomNumber = Random.Range(0,GameManager.Instance.GameAllCards.Count);
            if(!shopItemPosition[i].IsProduct)
            {
                shopItemPosition[i].IsProduct = true;
                GameObject _buyProduct = Instantiate(GameManager.Instance.GameAllCards[randomNumber]);
                _buyProduct.name = GameManager.Instance.GameAllCards[randomNumber].name;
                _buyProduct.transform.SetParent(shopItemPosition[i].transform);
                _buyProduct.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                _buyProduct.transform.localScale = Vector3.one;
                
                Button buyProductButton = _buyProduct.GetComponent<Button>();
                buyProductButton.interactable = true;

                if(buyProductButton.onClick == null)
                {
                    buyProductButton.onClick = new Button.ButtonClickedEvent();
                }
                buyProductButton.onClick.RemoveAllListeners();

                buyProductButton.onClick.AddListener(()=>
                {
                    //GameManager.Instance.CardSave(buyProductButton.gameObject);
                    //buyProductButton.gameObject.SetActive(false);
                    buyProduct = _buyProduct.gameObject;
                    buyProducts.Add(_buyProduct);
                });

            }
        }
    }

    
    public void MarketActivation()
    {
        gameObject.SetActive(true);
        
        


    }

    public void MarketPassivity()
    {
        marketBuy_Button.gameObject.SetActive(false);
        marketSell_Button.gameObject.SetActive(false);

        bagPanel.SetActive(false);
        shopPanel.SetActive(false);

        gameObject.SetActive(false);
        UIManager.Instance.MarketIcon_Button.SetActive(true);
        BuyProductDelete();
        if(bagPanel.activeSelf)
        {
            for (int i = 0; i < bagItemPositions.Length; i++)
            {
                if(i < bagItemPositions.Length-1)
                {
                    try
                    {
                        GameObject chieldProduct = bagItemPositions[i].transform.GetChild(0).gameObject;
                        chieldProduct.transform.SetParent(UIManager.Instance.DectGameObject.transform);
                        chieldProduct.SetActive(false);
                    }catch
                    {
                    }
                }
                
            }
        }
    }

    public void MarketRefreshButton()
    {
        BuyProductDelete();
        foreach (ShopItem item in shopItemPosition)
        {
            item.IsProduct = false;
        }
        SetShopItemDegree();
    }

    private void BuyProductDelete()
    {
        if(shopPanel.activeSelf)
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
                }catch
                {
                    print(i);
                }
            }

        }
    }

    private void MarketButtonsActivation(string buttonName,bool activation)
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

    public void MarketShopButton()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
        if(bagPanel.activeSelf)
        {
            bagPanel.SetActive(false);
        }
        MarketButtonsActivation("buy",shopPanel.activeSelf);
        MarketProductPlacement();
    }

    public void MarketMyBagButton()
    {
        
        bagPanel.SetActive(!bagPanel.activeSelf);
        marketSell_Button.gameObject.SetActive(bagPanel.activeSelf);
        if(shopPanel.activeSelf)
        {
            shopPanel.SetActive(false);
        }

        MarketButtonsActivation("sell",bagPanel.activeSelf);

        for (int i = 0; i < GameManager.Instance.Equipments.Count; i++)
        {
            if(i < bagItemPositions.Length)
            {
                GameManager.Instance.Equipments[i].transform.SetParent(bagItemPositions[i].transform);
                
                RectTransform rectTransform = GameManager.Instance.Equipments[i].GetComponent<RectTransform>();
                
                Animator productAnimator = rectTransform.GetComponent<Animator>();
                
                rectTransform.gameObject.SetActive(true);

                Button productButton = rectTransform.GetComponent<Button>();
                productButton.interactable = true;

                if(productAnimator != null)
                {
                    productAnimator.enabled = false;
                }

                if(productButton.onClick == null)
                {
                    productButton.onClick = new Button.ButtonClickedEvent();
                }

                productButton.onClick.RemoveAllListeners();
                productButton.onClick.AddListener(delegate
                {
                    sellProduct = productButton.gameObject;
                });

                rectTransform.anchoredPosition = Vector2.zero;
            }
        }
    }


    public void MarketBuyButton()
    {
        buyProduct.SetActive(false);
        buyProduct.transform.SetParent(UIManager.Instance.EarnedGameObject);
        GameManager.Instance.CardSave(buyProduct);
        buyProduct = null;
    }

    public void MarketSellButton()
    {
        Destroy(sellProduct.gameObject);
        GameManager.Instance.Equipments.Remove(sellProduct);
        GameManager.Instance.CardDelete(sellProduct);
    }

    public void ProductPriceAdjust()
    {
        if(shopPanel.activeSelf)
        {
            
        }
        else if(bagPanel.activeSelf)
        {

        }
    }

}   
