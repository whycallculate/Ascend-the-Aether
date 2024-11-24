using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : EnemyAI
{
    public override void GetMechanic()
    {

        switch (stepInt)
        {
            case 0:
                CalculateEnemyDamage(1.1f);
                stepInt++;
                break;
            case 1:
                CalculateEnemyDamage(1.15f);
                stepInt++;
                break;
            case 2:
                CalculateEnemyDamage(1.20f);
                stepInt++;
                break;
            case 3:
                CalculateEnemyDamage(1.25f);
                stepInt++;
                break;
            case 4:
                CalculateEnemyDamage(1.30f);
                stepInt++;
                break;
            case 5:
                CalculateEnemyDamage(1.35f);
                stepInt = 0;
                break;
        }
    }

}
