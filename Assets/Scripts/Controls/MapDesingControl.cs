using System;
using System.Collections;
using System.Collections.Generic;
using EnemyFeatures;
using LevelTypeEnums;
using UnityEngine;
using UnityEngine.UI;

public class MapDesingControl : MonoBehaviour
{
    private static MapDesingControl  instance;
    public static MapDesingControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MapDesingControl>();
            }
            return instance;
        }
    }
    [SerializeField] private MapDesing[] mapDesing;


    float x = 24.50002f;
    float y = -681.9312f;


    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            MapPathCreate();
            
        }    
    }
    private Vector2 leftLevelPosition = new Vector2(24.50002f,-681.9312f);
    private Vector2 rightLevelPosition = new Vector2(24.50002f,-681.9312f);
    public void MapPathCreate()
    {
        for (int i = 0; i < mapDesing.Length; i++)
        {
            LevelPrefabControl obje = Instantiate(UIManager.Instance.LevelPrefab).GetComponent<LevelPrefabControl>();
            obje.LevelInformationIdentification(mapDesing[i].levelType,mapDesing[i].levelCharacter,mapDesing[i].levelCharacterCount,mapDesing[i].levelCharacterPositions,mapDesing[i].levelEnemy,mapDesing[i].levelEnemyCount,mapDesing[i].levelEnemyPositions,mapDesing[i].enemyFeatures);
            RectTransform transform = obje.GetComponent<RectTransform>();
            
            if(obje.LevelIndex != GameManager.Instance.CharacterCurrentLevelIndex)
            {
                obje.GetComponent<Button>().interactable = false;
            }
            else
            {
                obje.GetComponent<Button>().interactable = true;
            }

            transform.SetParent(UIManager.Instance.LevelPrefabContent.transform);
            transform.localScale = Vector3.one; 
            transform.anchoredPosition = Vector2.zero;
            GameManager.Instance.LevelObjects.Add(obje);

            

            if (i == 0)
            {
                transform.anchoredPosition = new Vector2(24.50002f, -681.9312f);
            }
            else if (i % 2 == 0)
            {
                leftLevelPosition.x -= 150;
                leftLevelPosition.y += 150;
                transform.anchoredPosition = leftLevelPosition;
            }
            else if (i % 2 == 1)
            {
                rightLevelPosition.x += 150;
                rightLevelPosition.y += 150;
                transform.anchoredPosition = rightLevelPosition;
            }



        }
        
        int previous = 0;

        for (int i = 0; i < GameManager.Instance.LevelObjects.Count; i++)
        {
            LevelPrefabControl levelPrefab = GameManager.Instance.LevelObjects[i];

            

            if(levelPrefab.LevelType_Enum != LevelType_Enum.Change)
            {
                if (i == 0)
                {
                    levelPrefab.LevelIndex = 1;
                    previous = levelPrefab.LevelIndex;
                }
                else if (i % 2 == 0)
                {
                    levelPrefab.LevelIndex = previous;
                    previous = levelPrefab.LevelIndex;
                }
                else if (i % 2 == 1)
                {
                    levelPrefab.LevelIndex = previous * 2;
                    previous = levelPrefab.LevelIndex;
                }
            }
            else
            {
                levelPrefab.LevelIndex = previous * 2;
                previous = levelPrefab.LevelIndex;
            }
        }   



        foreach (var item in GameManager.Instance.LevelObjects)
        {
            if(item.LevelIndex != GameManager.Instance.CharacterCurrentLevelIndex)
            {
                item.GetComponent<Button>().interactable = false;
            }
            else if(item.LevelIndex == GameManager.Instance.CharacterCurrentLevelIndex)
            {
                item.GetComponent<Button>().interactable = true;
            }
        }
    }


}

[Serializable]
public class MapDesing
{
    public LevelType_Enum levelType;
    public CharacterControl levelCharacter;
    public int levelCharacterCount;
    public Vector3[] levelCharacterPositions;
    
    public EnemyController levelEnemy;
    public int levelEnemyCount;
    public Vector3[] levelEnemyPositions;
    public EnemyFeature[] enemyFeatures;

}