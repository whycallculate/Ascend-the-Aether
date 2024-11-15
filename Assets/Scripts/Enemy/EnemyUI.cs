using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    EnemyController enemyController;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Image damageImage;
    [SerializeField] private Image shieldImage;
    [SerializeField] private Image buffImage;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        
    }

    public void Start()
    {
        StartCoroutine(EnemyInitializeUI());
    }

    IEnumerator EnemyInitializeUI()
    {
        while (enemyController.enemyIsAlive)
        {
            healthBarImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = enemyController.HEALTH.ToString();
            shieldImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = enemyController.SHIELD.ToString();
            damageImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = enemyController.DAMAGE.ToString();
            buffImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = enemyController.POWER.ToString();
            if (!enemyController.enemyIsAlive)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
        } 
    }

}
