using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{

    [SerializeField] private Image characterHealtBar_Image;
    [SerializeField] private TextMeshProUGUI healtbarNumber_Text;
    [SerializeField] private TextMeshProUGUI shieldNumber_Text;
    [SerializeField] private TextMeshProUGUI energyNumber_Text;
    [SerializeField] private TextMeshProUGUI powerNumber_Text;

    //function that calculates the character's healtbar value and defines healtbara values
    public void CharacterHealtBar_Function(float value,string tranction,int characterHealt)
    {
        if(tranction == "+")
        {
            characterHealtBar_Image.fillAmount += value;
            healtbarNumber_Text.text = "%"+characterHealt.ToString();
        }
        else if(tranction == "-")
        {
            characterHealtBar_Image.fillAmount -= value;
            healtbarNumber_Text.text = "%"+characterHealt.ToString();

        }

    }

    //function that defines the shield values of the character to ui elements
    public void CharacterShild_Function(int characterShild)
    {
        shieldNumber_Text.text = characterShild.ToString();
    }

    //function that defines the energy values of the character into ui elements
    public void CharacterEnergy_Function(int characterEnergy)
    {
        energyNumber_Text.text = characterEnergy.ToString();
    }   

    public void CharacterPowerUI_Function(int characterPower)
    {
        powerNumber_Text.text = characterPower.ToString();
    }

}