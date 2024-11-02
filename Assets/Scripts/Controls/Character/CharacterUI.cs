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

    public void CharacterShild_Function(int characterShild)
    {
        shieldNumber_Text.text = characterShild.ToString();
    }


    public void CharacterEnergy_Function(int characterEnergy)
    {
        energyNumber_Text.text = characterEnergy.ToString();
    }   


}
