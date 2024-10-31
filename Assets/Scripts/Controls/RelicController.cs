using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicController : MonoBehaviour
{
    public Sprite relicSprite;
    public string relicName;
    public string relicDescription;
    public RelicEnum relicEnumType;


    public int upPower;
    public int downPower;

    public int upEnergy;
    public int downEnergy;

    public int upHealth;
    public int downHealth;

    public int upShield;
    public int downShield;

    public float attackPercent;
    public float shieldPercent;

    //bu method ile olusturmus oldugumuz relicte ki ozellikleri relice veriyoruz.
    public void RelicInitialize(int upPower, int downPower, int upEnergy, int downEnergy, int upHealth,
        int downHealth, int upShield, int downShield, float attackPercent, float shieldPercent)
    {
        this.upPower = upPower;
        this.downPower = downPower;
        this.upEnergy = upEnergy;
        this.downEnergy = downEnergy;
        this.upHealth = upHealth;
        this.downHealth = downHealth;
        this.upShield = upShield;
        this.downShield = downShield;     
        this.attackPercent = attackPercent;
        this.shieldPercent = shieldPercent;
    }

    public void InitializeStatusEffect(StatusEffect effectType, int effectValue)
    {
        switch (effectType)
        {
            case StatusEffect.ReduceMultipleEnemiesHealth:
                ReduceMultipleEnemyHealth(effectValue);
                break;
            case StatusEffect.ExtraGoldReward:
                IncreaseGoldReward(effectValue);
                break;
            case StatusEffect.ReduceEnemyHealth:
                ReduceEnemyHealth(effectValue);
                break;
            case StatusEffect.GoBackTime:
                GoBackTime(effectValue);
                break;
            case StatusEffect.Reborn:
                Reborn(effectValue);
                break;
            default:
                Debug.Log("Status yok :(");
                break;

        }

    }
    public void ReduceMultipleEnemyHealth(int value)
    {
        //Savas mekanikleri, oyuncu ve dusman Getirildigi zaman tekrar duzenlenecek.
    }

    public void IncreaseGoldReward(int value)
    {
        //Savas mekanikleri, oyuncu ve dusman Getirildigi zaman tekrar duzenlenecek.
    }

    public void ReduceEnemyHealth(int value)
    {
        //Savas mekanikleri, oyuncu ve dusman Getirildigi zaman tekrar duzenlenecek.
    }
    public void GoBackTime(int value)
    {
        //Savas mekanikleri, oyuncu ve dusman Getirildigi zaman tekrar duzenlenecek.
    }
    public void Reborn(int value)
    {
        //Savas mekanikleri, oyuncu ve dusman Getirildigi zaman tekrar duzenlenecek.
    }



}                              




