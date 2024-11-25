using System.Collections;
using System.Collections.Generic;
using EnemyFeatures;
using UnityEngine;

public enum EnemyType
{
    Slime,
    Rock,
    Assassin,
    Thief,
    Mage,
    KingSlime,
    KingMage
}

public class EnemyController : MonoBehaviour
{
    public int HEALTH;
    public int SHIELD;
    public int DAMAGE;
    public int POWER;
    public bool enemyIsAlive = true;
    public float difficultyMultiplier = 1.2f;

    public int minDamage = 10;
    public int maxDamage = 20;

    EnemyAI EnemyAI;

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
        if (SHIELD < 0)
        {
            SHIELD = 0;
        }
    }
    public void AddPower(int value) => POWER += value;
    public void TakePower(int value) => POWER -= value;



    private void Update()
    {
        if (HEALTH <= 0)
        {
            GameManager.Instance.IsEnemyAlive(gameObject);
        }
    }
    public void GetEnemyType()
    {
        //switch() {}

    }
    public IEnumerator MakeMove()
    {
        int action = Random.Range(0, 3); // 0 = hasar ver , 1 = kalkan , 2 = guc arttirma


        switch (action)
        {
            case 0:
                yield return new WaitForSeconds(2);
                int randomDamage = Random.Range(minDamage, maxDamage);
                randomDamage += POWER;
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
                POWER = 0;
                break;
            case 1:
                yield return new WaitForSeconds(2);

                OnShield(10);
                break;
            case 2:
                yield return new WaitForSeconds(2);

                AddPower(3);
                break;
        }

        if (HEALTH <= 0) enemyIsAlive = false;

        GameManager.Instance.SwitchTurnToPlayer();
    }


}
