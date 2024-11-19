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
    [SerializeField] private TextMeshProUGUI cardFeatureValueShow_Text; 
    
    #endregion

    private bool cardFeatureValueIncreasing = false;
    public bool CardFeatureValueIncreasing {get { return cardFeatureValueIncreasing;}}

    private bool cardFeatureValueDecreasing = false;
    public bool CardFeatureValueDecreasing {get { return cardFeatureValueDecreasing;}}

    private int cartFeatureValue;
    public int CartFeatureValue {get { return cartFeatureValue;}}

    private int firstCarFeatureValue = 0;
    public int FirstCarFeatureValue {get { return firstCarFeatureValue;} set { firstCarFeatureValue = value; } }


    public void CardFeatureUIInitialize(string cardFeatureName,int cardFeatureValue)
    {
        cardFeatureName_Text.text = cardFeatureName;
        cardFeatureValue_Text.text = cardFeatureValue.ToString();
        cartFeatureValue = cardFeatureValue;
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
        GameManager.Instance.CrystalCount--;
        if(GameManager.Instance.CrystalCount > 0)
        {
            cartFeatureValue++;

        }
        else
        {
            //NewMethod();
            UIManager.Instance.CardFeatureValueButtonClose();
        }


        cardFeatureValue_Text.text = cartFeatureValue.ToString();
        cardFeatureValueIncreasing = true;
        cardFeatureValueDecreasing = false;
    }

    private static void NewMethod()
    {
        for (int i = 0; i < UIManager.Instance.CardFeaturesGameObjects.Count; i++)
        {
            UIManager.Instance.CardFeaturesGameObjects[i].plusButton.interactable = false;
            UIManager.Instance.CardFeaturesGameObjects[i].subtractButton.interactable = false;
        }
    }

    public void SubtractButtonFunction()
    {
        GameManager.Instance.CrystalCount--;
        if(GameManager.Instance.CrystalCount >0)
        {
            if(cartFeatureValue > 0)
            {
                cartFeatureValue--;
            }
        }
        else
        {
            UIManager.Instance.CardFeatureValueButtonClose();
        }
        cardFeatureValue_Text.text = cartFeatureValue.ToString();
        cardFeatureValueIncreasing = false;
        cardFeatureValueDecreasing = true;
    }

   

    
}
