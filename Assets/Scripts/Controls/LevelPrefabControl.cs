using System.Collections;
using System.Collections.Generic;
using EnemyFeatures;
using LevelTypeEnums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPrefabControl : MonoBehaviour
{
    [SerializeField] private LevelType_Enum levelTypeEnum;
    public LevelType_Enum LevelType_Enum { get { return levelTypeEnum; } }
    [SerializeField] private Button levelButton;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private int levelIndex;
    public int LevelIndex { get { return levelIndex; }  set { levelIndex = value; } }

    #region  Enemy
    [SerializeField] private EnemyController levelEnemyPrefab;
    [SerializeField] private Vector3[]  levelEnemyPosition;
    [SerializeField] private EnemyFeature[] enemy;
    [SerializeField] private int levelEnemyCount;

    #endregion

    #region  Character
    [SerializeField] private CharacterControl levelCharacterPrefab;
    [SerializeField] private int levelCharacterCount;
    [SerializeField] private Vector3[]  levelCharacterPosition;

    #endregion

    private void OnValidate() 
    {
        levelButton = GetComponent<Button>();    
        levelText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Awake() 
    {
        levelText.text = levelTypeEnum.ToString();
        if(gameObject.tag != "ChangeLevel" && gameObject.tag != "BossLevel")
        {
            if(levelTypeEnum != LevelType_Enum.None)
            {
                gameObject.tag = levelTypeEnum.ToString();
            }
            levelButton.onClick.AddListener(()=>LevelButtonFunction(true));
        }
        else if(gameObject.tag == "ChangeLevel")
        {
            levelButton.onClick.AddListener(ChangeLevelType);
        }
        else if(gameObject.tag == "BossLevel")
        {
            levelButton.onClick.AddListener(BossLevel_Function);
        }

        
    }

    //level butonuna basınca düşman ve karakteri üreten method
    private void LevelButtonFunction(bool value)
    {
        if(levelIndex == 1)
        {
            GameManager.Instance.CreatingCharacter(levelCharacterCount,levelCharacterPrefab,levelCharacterPosition);
        }
        else
        {
            if(GameManager.Instance.character == null)
            {
                GameManager.Instance.CreatingCharacter(levelCharacterCount,levelCharacterPrefab,levelCharacterPosition);
            }
        }



        GameManager.Instance.CurrentLevelIndex = levelIndex;

        if(value)
        {
            GameManager.Instance.CharacterCurrentLevelType = levelTypeEnum.ToString();
            
            SaveSystem.DataSave("levelType",GameManager.Instance.CharacterCurrentLevelType);

            GameManager.Instance.CreatingEnemies(levelEnemyCount,levelEnemyPrefab,levelEnemyPosition,enemy);
            UIManager.Instance.MapPrefab.SetActive(false);
            UIManager.Instance.CardAndHealtButtonUIOpenOrClose(false);
        }

        levelButton.interactable  =false;
        GameManager.Instance.LevelOpening();
        GameManager.Instance.SetActiveCardMovement();
    }
    
    // ileri veya geri level geçişi yapabilip yapamiyacağimizi kontrol ediyor
    public void NextBackLevelOpen(int _levelIndex)
    {
        if(GameManager.Instance.CharacterCurrentLevelType != LevelType_Enum.None.ToString())
        {
            if (levelTypeEnum.ToString() == GameManager.Instance.CharacterCurrentLevelType || levelTypeEnum == LevelType_Enum.Change || levelTypeEnum == LevelType_Enum.Boss)
            {
                if (levelIndex == _levelIndex)
                {
                    levelButton.interactable = true;
                }
                else
                {
                    levelButton.interactable = false;
                }
            }
            else
            {
                levelButton.interactable = false;
            }

        }
        else
        {
            if (levelIndex == _levelIndex)
            {
                levelButton.interactable = true;
            }
            else
            {
                levelButton.interactable = false;
            }
           
        }
    }

    //this function for change level button 
    public void ChangeLevelType()
    {
        GameManager.Instance.CharacterCurrentLevelType = LevelType_Enum.None.ToString();

        SaveSystem.DataSave("levelType",GameManager.Instance.CharacterCurrentLevelType);

        levelButton.interactable =false;
        UIManager.Instance.CardAndHealtButtonUIOpenOrClose(true);

        GameManager.Instance.CurrentLevelIndex = levelIndex;

        GameManager.Instance.levelProgress += LevelButtonFunction;
    }

    //this function for boss level button 
    public void BossLevel_Function()
    {
        GameManager.Instance.FinishLevel = true;
        levelButton.interactable = false;

        if (GameManager.Instance.character == null)
        {
            GameManager.Instance.CreatingCharacter(levelCharacterCount, levelCharacterPrefab, levelCharacterPosition);
        }

        GameManager.Instance.CreatingEnemies(levelEnemyCount,levelEnemyPrefab,levelEnemyPosition,enemy);
        GameManager.Instance.SetActiveCardMovement();
        UIManager.Instance.MapPrefab.SetActive(false);
    
    }

}
