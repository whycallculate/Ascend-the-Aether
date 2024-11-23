using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardFeatureControl : MonoBehaviour
{
    #region  kart özelliklerini değiştirme ui
    
    [Header("Card Feature Adjust UI")]
    [SerializeField] private TextMeshProUGUI cardFeatureName_Text;
    [SerializeField] private TMP_InputField cardFeaturValue_InputField;
    public TMP_InputField CardFeaturValue_InputField { get { return cardFeaturValue_InputField;}}
    
    [SerializeField] private TextMeshProUGUI cardFeatureValue_Text;
    public TextMeshProUGUI CardFeatureValue_Text { get { return cardFeatureValue_Text;}}

    [SerializeField] private Button plusButton;
    public Button PlusButton {get {return plusButton;}}
    [SerializeField] private Button subtractButton;
    public Button SubtractButton {get {return subtractButton;}}

    #endregion

    #region  kart'in özelliklerinin değerlerini gösteren ui
    [Space]
    [Space]

    [Header("Card Feature Show UI")]
    [SerializeField] private TextMeshProUGUI cardFeatureNameShow_Text;
    public TextMeshProUGUI CardFeatureNameShow_Text { get { return cardFeatureNameShow_Text;}}

    [SerializeField] private TextMeshProUGUI cardFeatureValueShow_Text; 
    public TextMeshProUGUI CardFeatureValueShow_Text { get { return cardFeatureValueShow_Text;}}
    
    #endregion

    private bool cardFeatureValueIncreasing = false;
    public bool CardFeatureValueIncreasing {get { return cardFeatureValueIncreasing;}}

    private bool cardFeatureValueDecreasing = false;
    public bool CardFeatureValueDecreasing {get { return cardFeatureValueDecreasing;}}

    private int cartFeatureValue;
    public int CartFeatureValue {get { return cartFeatureValue;} set { cartFeatureValue = value; } }

    private int firstCarFeatureValue = 0;
    public int FirstCarFeatureValue {get { return firstCarFeatureValue;} set { firstCarFeatureValue = value; } }

    private int amount;
    public int Amount { get { return amount;} set { amount = value; } }

    
    public void CardFeatureUIInitialize(string cardFeatureName,int cardFeatureValue)
    {
        cardFeatureName_Text.text = cardFeatureName;
        cardFeatureValue_Text.text = cardFeatureValue.ToString();
        cartFeatureValue = cardFeatureValue;
        amount = cardFeatureValue;
        firstCarFeatureValue = cartFeatureValue;
    }
    
    public void CardFeatureShow(int cardFeatureValue,bool isCardFeatureNameChange,string cardFeatureName="")
    {
        if(isCardFeatureNameChange)
        {
            cardFeatureNameShow_Text.text = cardFeatureName;
            cardFeatureValueShow_Text.text = cardFeatureValue.ToString();

        }
        else
        {
            if(cardFeatureValueShow_Text != null)
            {
                cardFeatureValueShow_Text.text = cardFeatureValue.ToString();
            }
        }
    }

    public void PlusButtonFunction()
    {
        UIManager.Instance.CardUpdateButton.interactable = true;
        
        if (isButtonProcessing) return; // Halihazırda çalışıyorsa dur
        isButtonProcessing = true;

        if(GameManager.Instance.CrystalCount > 0)
        {
            amount--;
            
            
            GameManager.Instance.CrystalCount--;
            UIManager.Instance.CrystalCount_Text.text = GameManager.Instance.CrystalCount.ToString();
            cartFeatureValue++;

        }
        else if(GameManager.Instance.CrystalCount <= 0)
        {
            UIManager.Instance.CardFeatureValueButtonClose("Feature");

        }


        cardFeatureValue_Text.text = cartFeatureValue.ToString();
        cardFeatureValueIncreasing = true;
        cardFeatureValueDecreasing = false;

        isButtonProcessing = false;

    }

    private bool isButtonProcessing = false;
    public void SubtractButtonFunction()
    {
        UIManager.Instance.CardUpdateButton.interactable = true;

        if (isButtonProcessing) return; // Halihazırda çalışıyorsa dur
        isButtonProcessing = true;


        if(GameManager.Instance.CrystalCount > 0 )
        {
            amount--;
            
            GameManager.Instance.CrystalCount--;
            UIManager.Instance.CrystalCount_Text.text = GameManager.Instance.CrystalCount.ToString();

            if(cartFeatureValue > 0)
            {
                cartFeatureValue--;
            }
        }
        else if(GameManager.Instance.CrystalCount <= 0 )
        {
            UIManager.Instance.CardFeatureValueButtonClose("Feature");

        }


        cardFeatureValue_Text.text = cartFeatureValue.ToString();
        cardFeatureValueIncreasing = false;
        cardFeatureValueDecreasing = true;

        isButtonProcessing = false;
    }

   

    
}
