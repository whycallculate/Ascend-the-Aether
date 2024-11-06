using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int HEALTH;
    public int SHIELD;
    public int DAMAGE;
    public int POWER;
    public int ENERGY;
    public bool enemyIsAlive;

    
    public void SetHealth(int value)
    {
        HEALTH = value;
    }
    
    public void AddHealth(int value)
    {
        HEALTH += value;
    }
    public void TakeDamage(int value)
    {
        HEALTH -= value;
    }
    public void OnShield(int value)
    {
        POWER += value;
    }
    public void UseEnergy(int value)
    {
        ENERGY -= value;
    }
    public void AddPower(int value)
    {
        POWER += value;
    }
}
