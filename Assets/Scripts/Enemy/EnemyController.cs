using System.Collections;
using System.Collections.Generic;
using EnemyFeatures;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int HEALTH;
    public int SHIELD;
    public int DAMAGE;
    public int POWER;
    public bool enemyIsAlive = true;
    public float difficultyMultiplier = 1.2f;
    private int spawnCount = 0;

    public int minDamage = 10; 
    public int maxDamage = 20;
    private void SpawnEnemy()
    {
        float currentMultiplier = Mathf.Pow(difficultyMultiplier, spawnCount);

        // Yeni düşman özelliklerini çarpanla hesaplayın
        HEALTH = Mathf.RoundToInt(HEALTH * currentMultiplier);
        SHIELD = Mathf.RoundToInt(SHIELD * currentMultiplier);
        DAMAGE = Mathf.RoundToInt(DAMAGE * currentMultiplier);
        POWER = Mathf.RoundToInt(POWER * currentMultiplier);
        minDamage = Mathf.RoundToInt(minDamage * currentMultiplier);
        maxDamage = Mathf.RoundToInt(maxDamage * currentMultiplier);


        spawnCount++;
    }


    public void EnemyInitialize(EnemyFeature enemies)
    {
        HEALTH = enemies.Health;
        SHIELD = enemies.Shield;
        DAMAGE = enemies.Damage;
        POWER = enemies.Power;
    }

    public void SetHealth(int value) => HEALTH = value;
    public void AddHealth(int value) => HEALTH += value;
    public void TakeDamage(int value) => HEALTH -= value;
    public void OnShield(int value) => SHIELD += value;
    public void TakeShield(int value) 
    {
        SHIELD -= value;
        if(SHIELD < 0)
        {
            SHIELD = 0;
        }
    }
    public void AddPower(int value) => POWER += value;
    public void TakePower(int value) => POWER -= value;



    private void Update()
    {
        if(HEALTH <= 0 )
        {
            GameManager.Instance.IsEnemyAlive(gameObject);
        }
    }


    public void MakeMove()
    {
        if (!enemyIsAlive) return;

        int action = Random.Range(0, 3); // 0 = hasar ver , 1 = kalkan , 2 = guc arttirma


        switch (action)
        {
            case 0:
                int randomDamage = Random.Range(minDamage, maxDamage);
                DAMAGE = randomDamage;
                if (GameManager.Instance.character.shieldCurrent <= 0)
                {
                    GameManager.Instance.CardCharacterInteraction("healtbar", "-", randomDamage);
                }
                else if(GameManager.Instance.character.shieldCurrent > 0)
                {
                    int newDamage = GameManager.Instance.character.shieldCurrent - randomDamage;
                    GameManager.Instance.CardCharacterInteraction("shield", "-", randomDamage);
                    if(GameManager.Instance.character.shieldCurrent <= 0)
                    {
                        GameManager.Instance.CardCharacterInteraction("healtbar", "-", -newDamage);

                    }
                }
                break;
            case 1:
                OnShield(10);
                break;
            case 2:
                AddPower(3);
                break;
        }

        if (HEALTH <= 0) enemyIsAlive = false;

        GameManager.Instance.SwitchTurnToPlayer();
    }


}
