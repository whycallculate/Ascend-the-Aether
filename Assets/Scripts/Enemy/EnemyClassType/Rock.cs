using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : EnemyAI
{
    public override void GetMechanic()
    {

        switch (stepInt)
        {
            case 0:
                ClassMechanic();
                stepInt++;
                break;
            case 1:
                CalculateEnemyDamage();
                stepInt++;
                break;
            case 2:
                ClassMechanic();
                stepInt++;
                break;
            case 3:
                CalculateEnemyDamage();
                stepInt++;
                break;
            case 4:
                CalculateEnemyDamage();
                stepInt++;
                break;
            case 5:
                ClassMechanic();
                stepInt=0;
                break;
        }
    }
    public void ClassMechanic()
    {
        if (shield > 0)
        {
            OnShield(CalculateEnemyDiff(10));
        }
        else if(shield < 0)
        {
            CalculateEnemyDamage();
        }

    }
}
