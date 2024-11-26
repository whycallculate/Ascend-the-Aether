using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : EnemyAI
{

    public override void GetMechanic()
    {
        
        switch (stepInt)
        {
            case 0:
                ClassMechanic(gameObject);
                OnShield(CalculateEnemyDiff(5));
                stepInt++;
                break;
            case 1:
                ClassMechanic(gameObject);
                CalculateEnemyDamage();
                stepInt++;
                break;
            case 2:
                ClassMechanic(gameObject);
                CalculateEnemyDamage();
                stepInt++;
                break;
            case 3:
                ClassMechanic(gameObject);
                OnShield(CalculateEnemyDiff(5));

                stepInt++;
                break;
            case 4:
                ClassMechanic(gameObject);
                CalculateEnemyDamage();
                stepInt = 0;
                break;
        }
    }
    public void ClassMechanic(GameObject enemyPrefab)
    {
        if (enemyMech == false)
        {
            if (health > 0)
            {
                if (health <= maxHealth / 2)
                {
                    GameObject enemy1 = Instantiate(enemyPrefab, transform.position + Vector3.left, Quaternion.identity);
                    GameObject enemy2 = Instantiate(enemyPrefab, transform.position + Vector3.right, Quaternion.identity);

                    enemy1.GetComponent<EnemyController>().HEALTH = maxHealth / 2;
                    enemy2.GetComponent<EnemyController>().HEALTH = maxHealth / 2;
                    enemy1.GetComponent<EnemyController>().GetEnemyType();
                    enemy2.GetComponent<EnemyController>().GetEnemyType();
                    enemy1.GetComponent<EnemyController>().EnemyAI.enemyMech = true;
                    enemy2.GetComponent<EnemyController>().EnemyAI.enemyMech = true;
                    GameManager.Instance.enemys.Add(enemy1.GetComponent<EnemyController>());
                    GameManager.Instance.enemys.Add(enemy2.GetComponent<EnemyController>());


                    Destroy(gameObject);
                }
            }
        }
        else if(enemyMech == true)
        {
            return;
        }
       
        
    }


}
