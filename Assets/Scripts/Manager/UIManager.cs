using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    [SerializeField] private CharacterUI characterUI;
    public CharacterUI CharacterUI { get { return characterUI; }  set { characterUI = value; } }

    [SerializeField] private Transform earnedGameObject;
    public Transform EarnedGameObject { get { return earnedGameObject;}}

    public RectTransform[] cardPos;
    [SerializeField] private Button nextTourButton;
    public Button NextTourButton { get { return nextTourButton;}}

    [SerializeField] private TextMeshProUGUI energyNumber_Text;
    public TextMeshProUGUI EnergyNumber_Text {get { return energyNumber_Text;}}

    [SerializeField] private GameObject mapPrefab;
    public GameObject MapPrefab { get { return mapPrefab;}}

    [SerializeField]private GameObject levelsPanel;
    public GameObject LevelsPanel { get { return levelsPanel;}}

    [SerializeField] private GameObject cardUpgradePanel;
    public GameObject CardUpgradePanel { get { return cardUpgradePanel;}}

    [SerializeField] private GameObject cardUpgradeContent;
    public GameObject CardUpgradeContent { get { return cardUpgradeContent;}}

    [SerializeField] private GameObject cardsScroll;
    public GameObject CardsScroll { get { return cardsScroll;}}


    #region  Card Development Table UI

    #region  Card Feature Adjust UI
    
    [SerializeField]private List<CardFeatureControl> cardFeatureGameObjects = new List<CardFeatureControl>();
    public List<CardFeatureControl> CardFeaturesGameObjects { get { return cardFeatureGameObjects;}}

    [SerializeField] private GameObject cardDevelopmentTable;
    public GameObject CardDevelopmentTable { get { return cardDevelopmentTable;}}

    [SerializeField] private Transform cardFeatureParent;
    public Transform CardFeatureParent { get { return cardFeatureParent;}}

    [SerializeField] private GameObject cardFeaturePreafab;
    public GameObject CardFeaturePreafab { get {return cardFeaturePreafab;}}
    
    [SerializeField] private Transform selectCardParent;
    public Transform SelecetCardParent { get { return selectCardParent;}}

    [SerializeField] private Button cardUpdateButton;
    public Button CardUpdateButton { get { return cardUpdateButton;}}

    [SerializeField] private Button cardCancelButton;
    
    #endregion

    #region  Card Features Show UI

    [SerializeField] private List<CardFeatureControl> cardFeaturesShowGameObjects = new List<CardFeatureControl>();
    public List<CardFeatureControl> CardFeaturesShowGameObjects { get { return cardFeaturesShowGameObjects;}}

    [SerializeField] private CardFeatureControl cardFeatureShowPrefab;
    public CardFeatureControl CardFeatureShowPrefab {get { return cardFeatureShowPrefab;}}

    [SerializeField] private Transform cardFeatureShowParent;
    public Transform CardFeatureShowParent{get { return cardFeatureShowParent;}}


    #endregion

    [SerializeField] private TextMeshProUGUI crystalCount_Text;
    public TextMeshProUGUI CrystalCount_Text { get { return crystalCount_Text;}}


    #endregion


    private void Awake() 
    {
        levelsPanel = mapPrefab.transform.GetChild(0).gameObject;
        cardUpgradePanel = mapPrefab.transform.GetChild(1).gameObject;
        cardUpgradeContent = cardUpgradePanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
        cardsScroll = cardUpgradePanel.transform.GetChild(0).gameObject;
        cardDevelopmentTable = cardUpgradePanel.transform.GetChild(1).gameObject;
        

        if(levelsPanel.activeSelf)
        {
            crystalCount_Text.text = GameManager.Instance.CrystalCount.ToString();
        }


    }

    private void Start() 
    {
        if(cardCancelButton != null)
        {
            cardCancelButton.onClick.AddListener(CardFeatureCancelButtonFunction);
        }
        if(cardUpdateButton != null)
        {
            cardUpdateButton.onClick.AddListener(CardFeatureUpdateButtonFunction);
        }
    }
    
    public void SetActiveUI(string panelName)
    {
        levelsPanel.SetActive(panelName.Equals(levelsPanel.name) );
        cardUpgradePanel.SetActive(panelName.Equals(cardUpgradePanel.name) || panelName.Equals(cardDevelopmentTable.name) ||panelName.Equals(cardsScroll.name));
        cardDevelopmentTable.SetActive(panelName.Equals(cardDevelopmentTable.name));
        cardsScroll.SetActive(panelName.Equals(cardsScroll.name));

        if(cardUpgradePanel.activeSelf)
        {
            for (int i = 0; i < GameManager.Instance.Cards.Count; i++)
            {
                GameManager.Instance.Cards[i].transform.SetParent(cardUpgradeContent.transform);
                GameManager.Instance.Cards[i].SetActive(true);
            }
        }


        if(levelsPanel.activeSelf)
        {
            for (int i = 0; i < GameManager.Instance.Cards.Count; i++)
            {
                GameManager.Instance.Cards[i].GetComponent<CardMovement>().enabled = true;
                GameManager.Instance.Cards[i].GetComponent<Button>().onClick = null;
            }
            crystalCount_Text.text = GameManager.Instance.CrystalCount.ToString();
        }
    }

    private bool[] bools = new []{false,true,true,false};
    private int succesNumber = 0;
    private int failedNumber = 0;
    private int subtractResult;

    private int result;
    
    //Kartin ugrade buttonuna basınca etkileşimi sağliyan method
    public void CardFeatureUpdateButtonFunction()
    {

        if(GameManager.Instance.CrystalCount > 0)
        {
            for (int i = 0; i < cardFeatureGameObjects.Count; i++)
            {
                if(cardFeatureGameObjects[i].FirstCarFeatureValue > cardFeatureGameObjects[i].Amount)
                {
                    result += cardFeatureGameObjects[i].FirstCarFeatureValue - cardFeatureGameObjects[i].Amount;
                }
                else if(cardFeatureGameObjects[i].Amount >  cardFeatureGameObjects[i].FirstCarFeatureValue)
                {
                    result +=cardFeatureGameObjects[i].Amount- cardFeatureGameObjects[i].FirstCarFeatureValue ;
                }
                CardDevelopment.Instance.CardFeatureValues.Add(cardFeatureGameObjects[i].CartFeatureValue);
            }

          
            if(result < 0)
            {
                result *= -1;
            }

            GameManager.Instance.CrystalCoinLose(result);
            

            GameManager.Instance.CardDevelopmentRate();
            
            result = 0;
        }
        else if(GameManager.Instance.CrystalCount <= 0)
        {
            CardFeatureValueButtonClose("All");
        }
    }


    public void CardFeatureValueUpdate(bool isUpgrade)
    {
        if(isUpgrade)
        {
            for (int i = 0; i < cardFeatureGameObjects.Count; i++)
            {
                cardFeatureGameObjects[i].Amount = cardFeatureGameObjects[i].CartFeatureValue;
                cardFeatureGameObjects[i].CartFeatureValue =   cardFeatureGameObjects[i].Amount;
                cardFeatureGameObjects[i].FirstCarFeatureValue = cardFeatureGameObjects[i].CartFeatureValue;
                cardFeatureGameObjects[i].CardFeatureValue_Text.text = cardFeatureGameObjects[i].CartFeatureValue.ToString();
                cardFeaturesShowGameObjects[i].CardFeatureValueShow_Text.text = cardFeatureGameObjects[i].CartFeatureValue.ToString();
            }
        }
        else
        {
            for (int i = 0; i < cardFeatureGameObjects.Count; i++)
            {
                CardFeatureControl cardFeatureShowControl = cardFeaturesShowGameObjects[i];
                int _cardFeatureNumber = int.Parse(cardFeatureShowControl.CardFeatureValueShow_Text.text );
                CardFeatureControl cardFeatureControl = cardFeatureGameObjects[i];
                cardFeatureControl.CartFeatureValue = _cardFeatureNumber;
                cardFeatureControl.CardFeatureValue_Text.text = _cardFeatureNumber.ToString();
                cardFeatureControl.Amount = cardFeatureControl.CartFeatureValue;
            }
        }
    }


    //kart geliştirme olayında  butonlari tiklanilebilirliğini kapatiyor
    public void CardFeatureValueButtonClose(string closeObjectName)
    {
        if(closeObjectName == "All")
        {
            for (int i = 0; i < cardFeatureGameObjects.Count; i++)
            {
                cardFeatureGameObjects[i].PlusButton.interactable = false;
                cardFeatureGameObjects[i].SubtractButton.interactable = false;
            }
            cardUpdateButton.interactable = false;
        }
        else if(closeObjectName == "Upgrade")
        {
            cardUpdateButton.interactable = false;
        }
        else if(closeObjectName == "Feature")
        {
            for (int i = 0; i < cardFeatureGameObjects.Count; i++)
            {
                cardFeatureGameObjects[i].PlusButton.interactable = false;
                cardFeatureGameObjects[i].SubtractButton.interactable = false;
            }
        }
        
    }

    

    //kart geliştirme olayında cancel butonuna basınca etkileşimi sağliyor
    public void CardFeatureCancelButtonFunction()
    {
        
        for (int i = 0; i < cardFeatureGameObjects.Count; i++)
        {
            cardFeatureGameObjects[i].gameObject.SetActive(false);
            CardDevelopment.Instance.SelectedCard = null;
            Destroy(cardFeatureGameObjects[i].gameObject);
            Destroy(cardFeaturesShowGameObjects[i].gameObject);
        }
        cardFeatureGameObjects.Clear();
        cardFeaturesShowGameObjects.Clear();
        mapPrefab.SetActive(true);
        SetActiveUI(cardsScroll.name);

        crystalCount_Text.text = GameManager.Instance.CrystalCount.ToString();

    }   

    //kart'in özelliklerinin adini ve değerini gösteren prefab'i oluşturuyor
    public void CreateCardFeatureShowGameObject(string cardFeatureName,int cardFeatureValue)
    {
        CardFeatureControl newCardFeatureShow = Instantiate(cardFeatureShowPrefab,cardFeatureShowParent);
        newCardFeatureShow.transform.localScale = Vector3.one;
        newCardFeatureShow.CardFeatureShow(cardFeatureValue,true,cardFeatureName);
        cardFeaturesShowGameObjects.Add(newCardFeatureShow); 
        CreateCardFeatureGameObject(cardFeatureName,cardFeatureValue);
    }


    //kart özelliklerini artırıp azaltmamızı sağlıyan prefabi oluşturmamizi sağliyor    
    public void CreateCardFeatureGameObject(string cardFeatureName,int cardFeatureValue)
    {
        CardFeatureControl newCardFeatureGameObject = Instantiate(cardFeaturePreafab.GetComponent<CardFeatureControl>(),cardFeatureParent);
        newCardFeatureGameObject.name = "Card"+cardFeatureName.ToUpper()+"Feature";
        newCardFeatureGameObject.CardFeatureUIInitialize(cardFeatureName,cardFeatureValue);
        cardFeatureGameObjects.Add(newCardFeatureGameObject);
    }

   
}
