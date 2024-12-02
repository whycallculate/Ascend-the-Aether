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
    public TextMeshProUGUI EnergyNumber_Text {get {return energyNumber_Text;} set {energyNumber_Text = value;}}
    [SerializeField] private TextMeshProUGUI powerNumber_Text;
    [SerializeField] private TextMeshProUGUI characterDamage_Text;


    public void CharacterInitialize(int healt,int shield,int power,int characterDamage)
    {
        healtbarNumber_Text.text = healt.ToString();
        shieldNumber_Text.text = shield.ToString();
        powerNumber_Text.text = power.ToString();
        characterDamage_Text.text = characterDamage.ToString();
        
    }

    //function that calculates the character's healtbar value and defines healtbara values
    public void CharacterHealtBar_Function(float value,string tranction,int characterHealt)
    {
        if(tranction == "+")
        {
            characterHealtBar_Image.fillAmount += value;
            healtbarNumber_Text.text = characterHealt.ToString();
        }
        else if(tranction == "-")
        {
            characterHealtBar_Image.fillAmount -= value;
            healtbarNumber_Text.text = characterHealt.ToString();

        }
        else
        {
            characterHealtBar_Image.fillAmount = value/100;
            healtbarNumber_Text.text = characterHealt.ToString();
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

    public void CharacterDamageUI_Function(int characterDamage)
    {
        characterDamage_Text.text = characterDamage.ToString();
    }
}
