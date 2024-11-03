using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CalculateCharacterValues : MonoBehaviour
{
    /// <summary>
    /// Bu metod karakterin verilerini azaltıp artırma olaylarını ayarlayan bir işlevdir.
    /// </summary>
    /// <param name="characterFeature">Karakter özelliği</param>
    /// <param name="transaction">İşlem türü</param>
    /// <param name="integerValue">Karakterin tam değerlerinde kullanılan bir alan</param>
    /// <param name="floatValue">Karakterin noktalı değerlerinde kullanılan bir alan</param>
    public void CharacterValueTransaction_Function(string characterFeature,string transaction,ref int targetFeatureValue,int targetFeatureMaxValue,int integerValue = 0,float floatValue=0)
    {
        switch(characterFeature)
        {
            case "healtbar":
                CharacterHealtBarTransaction_Function(ref targetFeatureValue,targetFeatureMaxValue,integerValue,floatValue,transaction);
            break;
            case "shield":
                CharacterShieldTransaction_Function(ref targetFeatureValue,targetFeatureMaxValue,integerValue,transaction);
            break;
            case "energy":
                CharacterEnergyTransaction_Function(ref targetFeatureValue,targetFeatureMaxValue,integerValue,transaction);
            break;
            case "power":
                CharacterPowerTransaction_Function(ref targetFeatureValue,targetFeatureMaxValue,integerValue,transaction);
            break;
            default:
            break;
        }
    }

    //Adjusting the character's healtbar
    public int CharacterHealtBarTransaction_Function(ref int currentHealt,int maxHealt,int healtValue,float healtbarValue,string transaction)
    {
        switch(transaction)
        {
            case "+":
                if(currentHealt < maxHealt)
                {
                    currentHealt += healtValue;
                    UIManager.Instance.CharacterUI.CharacterHealtBar_Function(healtbarValue,"+",currentHealt);
                }
            break;
            case "-":
                if(currentHealt > 0)
                {
                    currentHealt -= healtValue;
                    UIManager.Instance.CharacterUI.CharacterHealtBar_Function(healtbarValue, "-", currentHealt);
                }
                break;
        }
        return currentHealt;
    }
    //Adjusting the character's shield
    public void CharacterShieldTransaction_Function(ref int currentShield,int maxShield,int shieldValue,string transaction)
    {
        switch(transaction)
        {
            case "+":
                if (currentShield < maxShield)
                {
                    currentShield += shieldValue;
                    UIManager.Instance.CharacterUI.CharacterShild_Function(currentShield);
                }
                break;
            case "-":
                if (currentShield > 0)
                {
                    currentShield -= shieldValue;
                    UIManager.Instance.CharacterUI.CharacterShild_Function(currentShield);
                }
            break;

        }
    }

    //Adjusting the character's energy
    public void CharacterEnergyTransaction_Function(ref int currentEnergy,int maxEnergy,int energyValue,string transaction)
    {
        switch(transaction)
        {
            case "+":
                if (currentEnergy < maxEnergy)
                {
                    currentEnergy += energyValue;
                    UIManager.Instance.CharacterUI.CharacterEnergy_Function(currentEnergy);
                }
                break;
            case "-":
                if (currentEnergy > 0)
                {
                    currentEnergy -= energyValue;
                    UIManager.Instance.CharacterUI.CharacterEnergy_Function(currentEnergy);
                }
            break;

        }
    }

    //Adjusting the character's power
    public void CharacterPowerTransaction_Function(ref int currentPower,int maxPower,int powerValue,string transaction)
    {
        switch (transaction)
        {
            case "+":
                if(currentPower < 5)
                {
                    currentPower += powerValue;
                    UIManager.Instance.CharacterUI.CharacterPowerUI_Function(currentPower);
                }
            break;
            case "-":
                if(currentPower > 0)
                {
                    currentPower -= powerValue;
                    UIManager.Instance.CharacterUI.CharacterPowerUI_Function(currentPower);
                }
            break;
        }
    }
}
