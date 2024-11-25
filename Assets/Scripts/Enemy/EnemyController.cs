using System.Collections;
using System.Collections.Generic;
using EnemyFeatures;
using Unity.VisualScripting;
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
    public int maxHealth;
    public int HEALTH;
    public int SHIELD;
    public int DAMAGE;
    public int POWER;
    public bool enemyIsAlive = true;
    public float difficultyMultiplier = 1.2f;
    public int minDamage = 10;
    public int maxDamage = 20;
    public int enemyMech = 0;


    public EnemyType enemyType;
    public EnemyAI EnemyAI;

    public void EnemyInitialize(EnemyFeature enemies)
    {
        maxHealth = enemies.maxHealth;
        HEALTH = enemies.Health;
        SHIELD = enemies.Shield;
        DAMAGE = enemies.Damage;
        POWER = enemies.Power;
        EnemyAI.GetEnemyStatInit(maxHealth, HEALTH, SHIELD, DAMAGE, POWER, minDamage, maxDamage, enemyMech);
    }
    public void GetEnemyAiStat()
    {
        HEALTH = EnemyAI.health;
        SHIELD = EnemyAI.shield;
        DAMAGE = EnemyAI.damage;
        POWER = EnemyAI.power;
        minDamage = EnemyAI.minDamage;
        maxDamage = EnemyAI.maxDamage;
    }


    private void Awake()
    {
        GetEnemyType();
        if (enemyType == EnemyType.Slime)
        {
            Slime[] slimeComponents = EnemyAI.GetComponents<Slime>();
            for (int j = 1; j < slimeComponents.Length; j++)
            {
                Destroy(slimeComponents[j]);
            }
        }

        HEALTH = maxHealth;
    }


    private void Update()
    {
        if (HEALTH <= 0)
        {
            GameManager.Instance.IsEnemyAlive(gameObject);
        }
    }
    public void GetEnemyType()
    {
        switch (enemyType)
        {
            case EnemyType.Slime:
                Slime slime = gameObject.AddComponent<Slime>();
                EnemyAI = slime;
                EnemyAI.GetEnemyStatInit(maxHealth, HEALTH, SHIELD, DAMAGE, POWER, minDamage, maxDamage, enemyMech);

                break;

            case EnemyType.Rock:
                Rock rock = gameObject.AddComponent<Rock>();
                EnemyAI = rock;
                EnemyAI.GetEnemyStatInit(maxHealth, HEALTH, SHIELD, DAMAGE, POWER, minDamage, maxDamage, enemyMech);
                break;
            case EnemyType.Assassin:
                Assassin assassin = gameObject.AddComponent<Assassin>();
                EnemyAI = assassin;
                EnemyAI.GetEnemyStatInit(maxHealth, HEALTH, SHIELD, DAMAGE, POWER, minDamage, maxDamage, enemyMech);
                break;
            case EnemyType.Thief:
                Thief thief = gameObject.AddComponent<Thief>();
                EnemyAI = thief;
                EnemyAI.GetEnemyStatInit(maxHealth, HEALTH, SHIELD, DAMAGE, POWER, minDamage, maxDamage, enemyMech);
                break;
            case EnemyType.Mage:
                Mage mage = gameObject.AddComponent<Mage>();
                EnemyAI = mage;
                EnemyAI.GetEnemyStatInit(maxHealth, HEALTH, SHIELD, DAMAGE, POWER, minDamage, maxDamage, enemyMech);
                break;
            case EnemyType.KingSlime:
                KingSlime kingSlime = gameObject.AddComponent<KingSlime>();
                EnemyAI = kingSlime;
                EnemyAI.GetEnemyStatInit(maxHealth, HEALTH, SHIELD, DAMAGE, POWER, minDamage, maxDamage, enemyMech);
                break;
            case EnemyType.KingMage:
                KingMage kingMage = gameObject.AddComponent<KingMage>();
                EnemyAI = kingMage;
                EnemyAI.GetEnemyStatInit(maxHealth, HEALTH, SHIELD, DAMAGE, POWER, minDamage, maxDamage, enemyMech);
                break;
        }



        

    }

    public IEnumerator MakeMove()
    {
        yield return new WaitForSeconds(3);
        EnemyAI.GetMechanic();
        GetEnemyAiStat();


        GameManager.Instance.SwitchTurnToPlayer();
    }
    

}
