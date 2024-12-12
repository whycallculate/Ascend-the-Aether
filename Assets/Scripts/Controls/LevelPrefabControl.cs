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
    public LevelType_Enum LevelType_Enum { get { return levelTypeEnum; }  set { levelTypeEnum = value; } }
    [SerializeField] private Button levelButton;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private int levelIndex;
    public int LevelIndex { get { return levelIndex; }  set { levelIndex = value; } }

    #region  Enemy
    [SerializeField] private EnemyController levelEnemyPrefab;
    public EnemyController LevelEnemyPrefab { get {return levelEnemyPrefab;} set { levelEnemyPrefab = value;}}

    [SerializeField] private Vector3[]  levelEnemyPosition;
    public Vector3[] LevelEnemyPositions {get {return levelEnemyPosition;} set{levelEnemyPosition = value;}}

    [SerializeField] private EnemyFeature[] enemy;
    public EnemyFeature[] EnemyFeatures { get {return enemy;} set { enemy = value;}}
    [SerializeField] private int levelEnemyCount;


    #endregion

    #region  Character
    [SerializeField] private CharacterControl levelCharacterPrefab;
    public CharacterControl LevelCharacterPrefab { get {return levelCharacterPrefab;} set { levelCharacterPrefab = value;}}

    [SerializeField] private int levelCharacterCount;
    public int LevelCharacterCount { get {return levelCharacterCount;} set {levelCharacterCount = value;}}
    
    [SerializeField] private Vector3[]  levelCharacterPosition;
    public Vector3[] LevelCharacterPositions {get {return levelCharacterPosition;} set {levelCharacterPosition = value;}}


    #endregion

    #region  Crystal
    [SerializeField] private int levelCrystalCount;
    public int CrystalCount { get { return levelCrystalCount;}  set { levelCrystalCount = value;}}
    #endregion

    private void OnValidate() 
    {
    }

    private void Awake() 
    {
        levelButton = GetComponent<Button>();    
        levelText = GetComponentInChildren<TextMeshProUGUI>();
        
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

    private void Start() 
    {
        levelText.text = levelTypeEnum.ToString();
    }

    public void LevelInformationIdentification(LevelType_Enum levelType,CharacterControl levelCharacterPrefab,int levelCharacterCount,Vector3[] levelCharacterPositions,EnemyController levelEnemyPrefab,int levelEnemyCount,Vector3[] levelEnemyPositions,EnemyFeature[] enemyFeatures)
    {
        levelTypeEnum = levelType;
        this.levelCharacterPrefab = levelCharacterPrefab;
        this.levelCharacterCount = levelCharacterCount;
        levelCharacterPosition = levelCharacterPositions;
        this.levelEnemyPrefab = levelEnemyPrefab;
        this.levelEnemyCount = levelEnemyCount;
        levelEnemyPosition = levelEnemyPositions; 
        enemy = enemyFeatures;
    }

    //level butonuna basınca düşman ve karakteri üreten method
    private void LevelButtonFunction(bool value)
    {
        if(levelCharacterPrefab == null)
        {
            print("karakter boş");
        }
        if(levelEnemyPrefab == null)
        {
            print("düşman boş");
        }

        print("çalişiyor");
        if(levelIndex == 1 && GameManager.Instance.character == null)
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
            GameManager.Instance.IsLevelOver =false;
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
        if(GameManager.Instance.character == null)
        {
            GameManager.Instance.CreatingCharacter(levelCharacterCount,levelCharacterPrefab,levelCharacterPosition);
        }
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
