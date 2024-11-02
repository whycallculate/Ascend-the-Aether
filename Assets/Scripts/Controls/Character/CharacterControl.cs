using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private UIManager uiManager;
    private const int healt = 100;
    private const int energy = 5;
    private int currentHealt;
    private int shieldCurrent;
    private int energyCurrent;
    
    private void Awake() 
    {
        currentHealt = healt;   
        energyCurrent = energy;
        uiManager = UIManager.Instance; 
    }

    private void Update() 
    {

    }

    private void CharacterHealtBarTransaction_Function(int healtValue,string transaction)
    {
        switch(transaction)
        {
            case "+":
                if(currentHealt < 100)
                {
                    currentHealt += healtValue;
                    uiManager.CharacterUI.CharacterHealtBar_Function(.2f,"+",currentHealt);
                }
            break;
            case "-":
                if(currentHealt > 0)
                {
                    currentHealt -= healtValue;
                    uiManager.CharacterUI.CharacterHealtBar_Function(.2f, "-", currentHealt);
                }
                break;
        }
    }

    public void CharacterShieldTransaction_Function(int shieldValue,string transaction)
    {
        switch(transaction)
        {
            case "+":
                if (shieldCurrent < 100)
                {
                    shieldCurrent += shieldValue;
                    uiManager.CharacterUI.CharacterShild_Function(shieldCurrent);
                }
                break;
            case "-":
                if (shieldCurrent > 0)
                {
                    shieldCurrent -= shieldValue;
                    uiManager.CharacterUI.CharacterShild_Function(shieldCurrent);
                }
            break;

        }
    }

    public void CharacterEnergyTransaction_Function(int energyValue,string transaction)
    {
        switch(transaction)
        {
            case "+":
                if (energyCurrent < 5)
                {
                    energyCurrent += energyValue;
                    uiManager.CharacterUI.CharacterEnergy_Function(energyCurrent);
                }
                break;
            case "-":
                if (energyCurrent > 0)
                {
                    energyCurrent -= energyValue;
                    uiManager.CharacterUI.CharacterEnergy_Function(energyCurrent);
                }
            break;

        }
    }

    
}
