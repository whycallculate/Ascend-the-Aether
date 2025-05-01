using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardComboController 
{
    public void AttackCardCombo()
    {
        GameManager.Instance.enemy.EnemyAI.TakeShield(10);
        GameManager.Instance.enemy.EnemyAI.TakeDamage(10);
    }

    public void DefenceCardCombo()
    {
        Debug.Log("Defence combo");
    }

    public void AbilityCardCombo()
    {

        Debug.Log("Ability combo");
    }

    public void StrenghCardCombo()
    {
        Debug.Log("Strengh combo");
    }

}
