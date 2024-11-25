using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : EnemyAI
{
    public override void GetMechanic()
    {

        switch (stepInt)
        {
            case 0:
                AddPower(CalculateEnemyDiff(2));
                stepInt++;
                break;
            case 1:
                CalculateEnemyDamage(1.15f);
                stepInt++;
                break;
            case 2:
                AddPower(CalculateEnemyDiff(4));

                stepInt++;
                break;
            case 3:
                CalculateEnemyDamage(1.25f);
                stepInt++;
                break;
            case 4:
                AddPower(CalculateEnemyDiff(6));
                stepInt++;
                break;
            case 5:
                CalculateEnemyDamage(1.35f);
                stepInt = 0;
                break;
        }
    }

}

