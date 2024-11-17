using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardFeatureControl : MonoBehaviour
{
    #region  kart özelliklerini değiştirme ui
    
    [Header("Card Feature Adjust UI")]
    [SerializeField] private TextMeshProUGUI cardFeatureName_Text;
    [SerializeField] private TMP_InputField cardFeaturValue_InputField;
    public TMP_InputField CardFeaturValue_InputField { get { return cardFeaturValue_InputField;}}
    
    #endregion

    #region  kart'in özelliklerinin değerlerini gösteren ui
    [Space]
    [Space]

    [Header("Card Feature Show UI")]
    [SerializeField] private TextMeshProUGUI cardFeatureNameShow_Text;
    [SerializeField] private TextMeshProUGUI cardFeatureValueShow_Text; 
    
    #endregion

    public void CardFeatureUIInitialize(string cardFeatureName)
    {
        cardFeatureName_Text.text = cardFeatureName;
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
            cardFeatureValueShow_Text.text = cardFeatureValue.ToString();
        }
    }
}
