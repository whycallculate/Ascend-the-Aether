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
    public void CharacterValueTransaction_Function(string characterFeature,string transaction,ref bool isCharacterAlive,ref int targetFeatureValue,int targetFeatureMaxValue,int integerValue = 0,float floatValue=0)
    {
        switch(characterFeature)
        {
            case "healtbar":
                CharacterHealtBarTransaction_Function(ref targetFeatureValue,ref isCharacterAlive,targetFeatureMaxValue,integerValue,floatValue,transaction);
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

            case "damage":
                CharacterDamageTransaction_Function(ref targetFeatureValue,targetFeatureMaxValue,integerValue,transaction);
            break;

            default:
            break;
        }
    }

    //Adjusting the character's healtbar
    public int CharacterHealtBarTransaction_Function(ref int currentHealt,ref bool isCharacterAlive ,int maxHealt,int healtValue,float healtbarValue,string transaction)
    {
        switch (transaction)
        {
            case "+":
                if ((currentHealt + healtbarValue) < maxHealt)
                {
                    currentHealt += healtValue;
                    
                    if(currentHealt >= maxHealt)
                    {
                        currentHealt = maxHealt;
                    }
                    
                    UIManager.Instance.CharacterUI.CharacterHealtBar_Function(healtbarValue, "+", currentHealt);
                }
                else
                {
                    currentHealt = maxHealt;
                    UIManager.Instance.CharacterUI.CharacterHealtBar_Function(maxHealt, "", currentHealt);
                }
                break;

            case "-":
                if ((currentHealt - healtbarValue) > 0)
                {
                    currentHealt -= healtValue;
                    if(currentHealt < 0)
                    {
                        currentHealt = 0;
                    }
                    UIManager.Instance.CharacterUI.CharacterHealtBar_Function(healtbarValue, "-", currentHealt);
                }
                else if ((currentHealt - healtbarValue) < 0)
                {
                    currentHealt = 0;
                    UIManager.Instance.CharacterUI.CharacterHealtBar_Function(0, "", 0);
                }
                break;
        }

        if (currentHealt > 0)
        {
            return currentHealt;
        }
        else 
        {
            return 0;
        }
    }
    //Adjusting the character's shield
    public void CharacterShieldTransaction_Function(ref int currentShield,int maxShield,int shieldValue,string transaction)
    {
        switch(transaction)
        {
            case "+":
                if ((currentShield + shieldValue) < maxShield)
                {
                    currentShield += shieldValue;
                    
                    if(currentShield >= maxShield)
                    {
                        currentShield = maxShield;
                    }

                    UIManager.Instance.CharacterUI.CharacterShild_Function(currentShield);
                }
                else if((currentShield + shieldValue) >= maxShield)
                {
                    currentShield = maxShield;
                    UIManager.Instance.CharacterUI.CharacterShild_Function(currentShield);
                }
                break;
            case "-":
                if ((currentShield - shieldValue) > 0)
                {
                    currentShield -= shieldValue;

                    if(currentShield < 0)
                    {
                        currentShield = 0;
                    }

                    UIManager.Instance.CharacterUI.CharacterShild_Function(currentShield);
                }
                else if((currentShield - shieldValue) <= 0)
                {
                    currentShield = 0;
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
                if ((currentEnergy + energyValue ) < maxEnergy)
                {
                    currentEnergy += energyValue;

                    if(currentEnergy >= maxEnergy)
                    {
                        currentEnergy = maxEnergy;
                    }

                    UIManager.Instance.CharacterUI.CharacterEnergy_Function(currentEnergy);
                }
                else if((currentEnergy + energyValue) >= maxEnergy)
                {
                    currentEnergy = maxEnergy;
                    UIManager.Instance.CharacterUI.CharacterEnergy_Function(currentEnergy);
                }
                break;
            case "-":
                if ((currentEnergy - energyValue) > 0)
                {
                    currentEnergy -= energyValue;

                    if(currentEnergy < 0)
                    {
                        currentEnergy = 0;
                    }

                    UIManager.Instance.CharacterUI.CharacterEnergy_Function(currentEnergy);
                }
                else if((currentEnergy - energyValue) <= 0)
                {
                    currentEnergy = 0;
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
                if((currentPower + powerValue) < maxPower)
                {
                    currentPower += powerValue;

                    if(currentPower >= maxPower)
                    {
                        currentPower = maxPower;
                    }
                    
                    UIManager.Instance.CharacterUI.CharacterPowerUI_Function(currentPower);
                }
                else if((currentPower + powerValue ) <= maxPower)
                {
                    currentPower = maxPower;
                    UIManager.Instance.CharacterUI.CharacterPowerUI_Function(currentPower);
                }
            break;
            case "-":
                if((currentPower - powerValue) > 0)
                {
                    currentPower -= powerValue;

                    if(currentPower < 0)
                    {
                        currentPower = 0;
                    }

                    UIManager.Instance.CharacterUI.CharacterPowerUI_Function(currentPower);
                }
                else if((currentPower - powerValue) <= 0)
                {
                    currentPower = 0;
                    UIManager.Instance.CharacterUI.CharacterPowerUI_Function(currentPower);
                }
            break;
        }
    }

    public void CharacterDamageTransaction_Function(ref int currentDamage,int maxDamage,int damageValue,string transaction)
    {
        switch(transaction)
        {
            case "+":
                
                if((currentDamage + damageValue) < maxDamage)
                {
                    currentDamage += damageValue;
                    if(currentDamage >= maxDamage)
                    {
                        currentDamage = maxDamage;
                    }
                    UIManager.Instance.CharacterUI.CharacterDamageUI_Function(currentDamage);
                }
                else if((currentDamage + damageValue) >= maxDamage)
                {
                    currentDamage = maxDamage;
                    UIManager.Instance.CharacterUI.CharacterDamageUI_Function(currentDamage);
                }

            break;

            case "-":
                if((currentDamage - damageValue) > 0)
                {
                    currentDamage -= damageValue;
                    if(currentDamage <= 0)
                    {
                        currentDamage = 0;
                    }
                    UIManager.Instance.CharacterUI.CharacterDamageUI_Function(currentDamage);
                }
                else if((currentDamage - damageValue) <= 0)
                {
                    currentDamage = 0;
                    UIManager.Instance.CharacterUI.CharacterDamageUI_Function(currentDamage);
                }
            break;

            default:
            break;

        }
    }
}
