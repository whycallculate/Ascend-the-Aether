using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public  class EnemyAI : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int shield;
    public int damage;
    public int power;

    public int minDamage;
    public int maxDamage;
    public bool enemyMech = false;

    public float enemyDiff = 1.2f;


    public int stepInt;

    public int CalculateEnemyDiff(int value)
    {
        float diffEnemy = enemyDiff * value;

        return (int)diffEnemy;
    }
    public void CalculateEnemyDamage(float extraDmg = 1) 
    {
        float newDamage = Random.Range(minDamage, maxDamage);
        if(power <= 0)
        {
            damage = (int)(newDamage * enemyDiff * extraDmg);

        }
        else
        {
            damage = (int)(newDamage * enemyDiff * power * extraDmg);
        }
        Debug.Log(damage);

        if (GameManager.Instance.character.shieldCurrent <= 0)
        {
            GameManager.Instance.CardCharacterInteraction("healtbar", "-", damage);
        }
        else if (GameManager.Instance.character.shieldCurrent > 0)
        {
            int shieldDamage = GameManager.Instance.character.shieldCurrent - damage;
            GameManager.Instance.CardCharacterInteraction("shield", "-", damage);
            if (GameManager.Instance.character.shieldCurrent <= 0)
            {
                GameManager.Instance.CardCharacterInteraction("healtbar", "-", -shieldDamage);

            }
        }

    }
    public void SetHealth(int value) => health = value;
    public void AddHealth(int value) => health += value;
    public void TakeDamage(int value) => health -= value;
    public void OnShield(int value) => shield += value;
    public void TakeShield(int value)
    {
        shield -= value;
        if (shield < 0)
        {
            shield = 0;
        }
    }
    public void AddPower(int value) => power += value;
    public void TakePower(int value) => power -= value;
    public void GetEnemyStatInit(int maxHealth ,int health,int shield,int damage,int power,int minDamage,int maxDamage)
    {
        this.maxHealth = maxHealth;
        this.health = health;
        this.shield = shield;
        this.damage = damage;
        this.power = power;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
    }
    public virtual void GetMechanic()
    {

    }
}