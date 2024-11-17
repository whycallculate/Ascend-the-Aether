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

    public Button CardCancelButton { get { return cardCancelButton;}}


    #endregion


    private void Awake() 
    {
        levelsPanel = mapPrefab.transform.GetChild(0).gameObject;
        cardUpgradePanel = mapPrefab.transform.GetChild(1).gameObject;
        cardUpgradeContent = cardUpgradePanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
        cardsScroll = cardUpgradePanel.transform.GetChild(0).gameObject;
        cardDevelopmentTable = cardUpgradePanel.transform.GetChild(1).gameObject;
        
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

    public void CardUpgradeCardsFind()
    {
        if(GameManager.Instance.GameAllCards != null)
        {
            for (int i = 0; i < GameManager.Instance.GameAllCards.Count; i++)
            {
                GameObject card = Instantiate(GameManager.Instance.GameAllCards[i]);
                card.gameObject.name = GameManager.Instance.GameAllCards[i].name;
                card.transform.SetParent(earnedGameObject.transform);
                card.gameObject.SetActive(false);
                card.transform.SetSiblingIndex(i);
                card.transform.localScale = Vector3.one;
                card.GetComponent<CardMovement>().enabled = false;
                card.GetComponent<Button>().onClick.AddListener(()=>CardDevelopment.Instance.SelectCardDevelopment(card));
                GameManager.Instance.Cards.Add(card);
            }

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
        }
    }

  
    
    private List<int> cardFeatureValues = new List<int>();
    public void CardFeatureUpdateButtonFunction()
    {
        for (int i = 0; i < cardFeatureGameObjects.Count; i++)
        {   
            int cardFeatureValue = int.Parse(cardFeatureGameObjects[i].CardFeaturValue_InputField.text);
            cardFeatureValues.Add(cardFeatureValue);
            cardFeaturesShowGameObjects[i].CardFeatureShow(cardFeatureValue,false);
        }
        CardDevelopment.Instance.CardUpgrade(cardFeatureValues);

        foreach (CardFeatureControl card in cardFeatureGameObjects)
        {
            card.CardFeaturValue_InputField.text = "";
        }
        
    }

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
    }   

    public void CreateCardFeatureShowGameObject(string cardFeatureName,int cardFeatureValue)
    {
        CardFeatureControl newCardFeatureShow = Instantiate(cardFeatureShowPrefab,cardFeatureShowParent);
        newCardFeatureShow.transform.localScale = Vector3.one;
        newCardFeatureShow.CardFeatureShow(cardFeatureValue,true,cardFeatureName);
        cardFeaturesShowGameObjects.Add(newCardFeatureShow); 
        CreateCardFeatureGameObject(cardFeatureName);
    }
    
    public void CreateCardFeatureGameObject(string cardFeatureName)
    {
        CardFeatureControl newCardFeatureGameObject = Instantiate(cardFeaturePreafab.GetComponent<CardFeatureControl>(),cardFeatureParent);
        newCardFeatureGameObject.CardFeatureUIInitialize(cardFeatureName);
        cardFeatureGameObjects.Add(newCardFeatureGameObject);
    }
}
