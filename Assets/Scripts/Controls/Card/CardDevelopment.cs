using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDevelopment : MonoBehaviour
{
    private static CardDevelopment instance;
    
    public static CardDevelopment Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CardDevelopment>();
            }
            return instance;
        }
    }
    
    [SerializeField] private GameObject selectCard;
    
    public GameObject SelectedCard { get { return selectCard; }  set { selectCard = value; } }
    
    private AttackCardController attackCard;
    private DefenceCardController defenceCard;
    private AbilityCardController abilityCard;
    private StrengthCardController strenghCard;

    [SerializeField] private GameObject _object;

    public void SelectCardDevelopment(GameObject _selectCard)
    {
        selectCard = _selectCard;
        _selectCard.transform.SetParent(UIManager.Instance.SelecetCardParent);
        _selectCard.transform.position = UIManager.Instance.SelecetCardParent.position;
        _selectCard.SetActive(true);
        SelectCarInformationFind(_selectCard);
        UIManager.Instance.CardDevelopmentTable.SetActive(true);
        UIManager.Instance.CardsScroll.SetActive(false);

    }

   
    //geliştirme için şeçilen kart'in türü bulup değerlerinin adini ve değerlerini yazdırtan method
    public void SelectCarInformationFind(GameObject _selectCard)
    {
        
        switch(_selectCard.tag)
        {
            case "AttackCard":
                attackCard = _selectCard.GetComponent<AttackCardController>();

                string[] attackCardFeaturesName = {"Energy","Damge"};
                int[] attackFeaturesValue = {attackCard.energyCost,attackCard.damage};
                
                CardFeaturesUICreate(attackCardFeaturesName, attackFeaturesValue);
            
            break;
            
            case "DefenceCard":
                defenceCard = _selectCard.GetComponent<DefenceCardController>();

                string[] defenceCardFeaturesName = {"Energy","Defence"};
                int[] defenceFeaturesValue = {defenceCard.energyCost,defenceCard.defence};

                CardFeaturesUICreate(defenceCardFeaturesName,defenceFeaturesValue);
            break;
            
            case "AbilityCard":
                abilityCard = _selectCard.GetComponent<AbilityCardController>();

                string[] abilityCardFeaturesName = {"Energy","Ability"};
                int[] abilityFeaturesValue = {abilityCard.energyCost,abilityCard.ability};
                
                CardFeaturesUICreate(abilityCardFeaturesName,abilityFeaturesValue);
            
            break;

            case "StrenghCard":
            
                strenghCard = _selectCard.GetComponent<StrengthCardController>();
                string[] strenghCardFeaturesName = {"Energy","Strengh"};
                int[] strenghFeaturesValue = {strenghCard.energyCost,strenghCard.strength};

                CardFeaturesUICreate(strenghCardFeaturesName,strenghFeaturesValue);
            break;

            default:
            break;
        
        }
    }


    // kartin bilgileri için prefab oluşturup initialize ettiğimiz fonksiyon
    private void CardFeaturesUICreate(string[] cardFeatures,int[] cardFeaturesValue)
    {
        for (int i = 0; i < cardFeatures.Length; i++)
        {
            
            
            UIManager.Instance.CreateCardFeatureShowGameObject(cardFeatures[i],cardFeaturesValue[i]);

        }
        
    }

    

    //kartlari upgrade ettiğimiz fonksiyon

    private List<int> cardFeatureValues = new List<int>();
    public List<int> CardFeatureValues {get {return cardFeatureValues;}}
    
    public void CardUpgrade(bool isCardUpgrade)
    {
        if(isCardUpgrade)
        {
            switch (selectCard.tag)
            {
                case "AttackCard":
                    attackCard = selectCard.GetComponent<AttackCardController>();
                    attackCard.CardUpgradeInitialize(cardFeatureValues[0], cardFeatureValues[1]);
                    _object = Resources.Load<GameObject>($"Prefabs/Cards/AttackCards/{attackCard.name}");
                    _object.GetComponent<CardUI>().CardUpgradeInitialize(1);
                    _object.GetComponent<AttackCardController>().CardUpgradeInitialize(cardFeatureValues[0], cardFeatureValues[1]);
                    break;

                case "DefenceCard":
                    defenceCard = selectCard.GetComponent<DefenceCardController>();
                    defenceCard.CardUpgradeInitialize(cardFeatureValues[0], cardFeatureValues[1]);
                    _object = Resources.Load<GameObject>($"Prefabs/Cards/DefenceCards/{defenceCard.name}");
                    _object.GetComponent<CardUI>().CardUpgradeInitialize(1);
                    _object.GetComponent<DefenceCardController>().CardUpgradeInitialize(cardFeatureValues[0], cardFeatureValues[1]);
                    break;

                case "AbilityCard":
                    abilityCard = selectCard.GetComponent<AbilityCardController>();
                    abilityCard.CardUpgradeInitialize(cardFeatureValues[0], cardFeatureValues[1]);
                    _object = Resources.Load<GameObject>($"Prefabs/Cards/AbilityCards/{abilityCard.name}");
                    _object.GetComponent<CardUI>().CardUpgradeInitialize(1);
                    _object.GetComponent<AbilityCardController>().CardUpgradeInitialize(cardFeatureValues[0], cardFeatureValues[1]);
                    break;

                case "StrenghCard":
                    strenghCard = selectCard.GetComponent<StrengthCardController>();
                    strenghCard.CardUpgradeInitialize(cardFeatureValues[0], cardFeatureValues[1]);
                    _object = Resources.Load<GameObject>($"Prefabs/Cards/StrengthCards/{strenghCard.name}");
                    _object.GetComponent<CardUI>().CardUpgradeInitialize(1);
                    _object.GetComponent<StrengthCardController>().CardUpgradeInitialize(cardFeatureValues[0], cardFeatureValues[1]);
                    break;

                default:
                    break;
            }

            for (int i = 0; i < UIManager.Instance.CardFeaturesShowGameObjects.Count; i++)
            {
                CardFeatureControl cardFeatureControl = UIManager.Instance.CardFeaturesShowGameObjects[i];
                cardFeatureControl.CardFeatureShow(cardFeatureValues[i], false);

                UIManager.Instance.CardFeaturesGameObjects[i].CardFeatureValue_Text.text = cardFeatureValues[i].ToString();
            }
        }
        else
        {
            if(attackCard != null)
            {
                Deneme(new int[]{attackCard.energyCost,attackCard.damage});
            }
            else if(defenceCard != null)
            {
                Deneme(new int[]{defenceCard.energyCost,defenceCard.defence});
            }
            else if(abilityCard != null)
            {
                Deneme(new int[]{abilityCard.energyCost,abilityCard.ability});
            }
            else if(strenghCard != null)
            {
                Deneme(new int[]{strenghCard.energyCost,strenghCard.strength});
            }
        }
    }

    private void Deneme(int[] values)
    {
        for (int i = 0; i < UIManager.Instance.CardFeaturesGameObjects.Count; i++)
        {
            CardFeatureControl cardFeatureControl  = UIManager.Instance.CardFeaturesGameObjects[i];
            cardFeatureControl.CardFeatureValue_Text.text =values[i].ToString();
        }
    }
    
}
