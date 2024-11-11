using System.Collections;
using System.Collections.Generic;
using EnemyFeatures;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region skeleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    #endregion

    private int numberCardeUsed = 0;
    public int NumberCardeUsed {get {return numberCardeUsed;} set {numberCardeUsed = value;} }

    public List<GameObject> deck = new List<GameObject>();
    public List<GameObject> hand = new List<GameObject>();
    public int handSize = 5;

    public CharacterControl character;
    public EnemyController enemy;
    public bool isPlayerTurn = true;

    #region  Level fields
    
    private int characterCurrentLevelIndex =1;
    public int CharacterCurrentLevelIndex {get {return characterCurrentLevelIndex;} set {characterCurrentLevelIndex = value;}}
    private string characterCurrentLevelType ;
    public string CharacterCurrentLevelType {get {return characterCurrentLevelType;} set {characterCurrentLevelType = value;}}
    [SerializeField] private GameObject mapPrefab;
    [SerializeField] private GameObject levelsObject;
    [SerializeField] private List<LevelPrefabControl> levelObjects = new List<LevelPrefabControl>();

    #endregion

    #region  Enemy
    
    [Space]
    [Space]
    [Header("Enemy")]

    [SerializeField] private List<EnemyController> enemys = new List<EnemyController>();
    private int deadEnemyCount = 0;
        
    #endregion

    #region  Character

    [Space]
    [Space]
    [Header("Character")]
    
    [SerializeField] private List<CharacterControl> characters = new List<CharacterControl>();

    #endregion

    
    private void Awake()
    {
        levelsObject = GameObject.FindGameObjectWithTag("Levels");
        if (levelsObject != null)
        {
            for (int i = 0; i < levelsObject.transform.childCount; i++)
            {
                levelObjects.Add(levelsObject.transform.GetChild(i).GetComponent<LevelPrefabControl>());
            }
        }

        if (enemy == null)
        {
            for (int i = 0; i < deck.Count; i++)
            {
                deck[i].GetComponent<Button>().interactable = false;
            }
        }

        LevelIndexAdjust();
    }

   

    private void Start()
    {
        DrawCards();
    }

    


    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider.gameObject.GetComponent<EnemyController>() != null)
                {
                    enemy = hit.collider.gameObject.GetComponent<EnemyController>();
                    for (int i = 0; i < hand.Count; i++)
                    {
                        hand[i].GetComponent<Button>().interactable = true;
                    }
                }
            }          
        }

    }

    public void DrawCards()
    {
        hand.Clear();

        for (int i = 0; i < handSize; i++)
        {
            if(deck.Count > 0) 
            {
                int randomIndex = Random.Range(0, deck.Count);
                GameObject drawnCard = deck[randomIndex];
                drawnCard.SetActive(true);
                hand.Add(drawnCard);
                deck.Remove(drawnCard);
            
            }
        }

        for(int i = 0;i < hand.Count;i++)
        {
            hand[i].GetComponent<RectTransform>().anchoredPosition = UIManager.Instance.cardPos[i].anchoredPosition;

        }

    }
    public void HandToDeck()
    {
        foreach(GameObject card  in hand)
        {
            deck.Add(card);
            card.SetActive(false);
        }
    }
    public void SwitchTurnToEnemy()
    {
        HandToDeck();
        isPlayerTurn = false;
        enemy.MakeMove();
        
    }

    public void SwitchTurnToPlayer()
    {
        CardCharacterInteraction("energy", "+", 5);
        DrawCards();
        isPlayerTurn = true;
        SelectableCard(true);
    }
    public void CardCharacterInteraction(string characterTraits,string transaction,int value)
    {
        if(numberCardeUsed !=4)
        {
            numberCardeUsed++;
        }
        character.CharacterTraits_Function(characterTraits,transaction,value);
    }



    #region  Level Function
    public void LevelOpening()
    {
        for (int i = 0; i < levelObjects.Count; i++)
        {
            levelObjects[i].NextBackLevelOpen(characterCurrentLevelIndex);
        }
    }

    //karakter ölünce mevcut level index'ni sifirliyor
    public void LevelReset()
    {
        mapPrefab.SetActive(true);
        characterCurrentLevelType = levelObjects[0].LevelType_Enum.ToString();
        characterCurrentLevelIndex = 1;
        LevelOpening();
        SwitchTurnToEnemy();
        CardButtonInteractableControl();

    }

    #endregion



    #region  Enemy Function

    //Düşman karakteri oluşturmamizi sağliyor
    public void CreatingEnemies(int enemysCount,EnemyController levelEnemyPrefab,Vector3[] _enemyPosition,EnemyFeature[] enemies)
    {
        mapPrefab.SetActive(false);
        for (int i = 0; i < enemysCount; i++)
        {
            EnemyController enemyClone = Instantiate(levelEnemyPrefab, _enemyPosition[i], Quaternion.identity);
            enemyClone.EnemyInitialize(enemies[i]);
            enemys.Add(enemyClone);

        }
    }

    //Düşmanlarin yaşamadiğini kontrol edip map haritasını aktif durumunu tetikliyor
    public  void IsEnemyAlive(GameObject enemy)
    {
        InitializeDeck();
        Destroy(enemy);
        CardButtonInteractableControl();
        deadEnemyCount++;
        
        if(deadEnemyCount == enemys.Count)
        {
            mapPrefab.SetActive(true);
            deadEnemyCount = 0;
            enemys.Clear();
            SwitchTurnToEnemy();
            CardButtonInteractableControl();
        }

    }

    #endregion 
    //kart objelerin buttonunu tıklanamamasını sağliyor
    private void CardButtonInteractableControl()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].GetComponent<Button>().interactable = false;
        }
    }


    #region  Character

    //karakter oluşturmamizi sağliyor
    public void CreatingCharacter(int charactersCount,CharacterControl levelCharacterPrefab,Vector3[] _characterPosition)
    {
        for (int i = 0; i < charactersCount; i++)
        {
            CharacterControl characterClone = Instantiate(levelCharacterPrefab,_characterPosition[i],Quaternion.identity);
            character = characterClone;
            characters.Add(characterClone);
        }
    }

    #endregion



    #region  Card
    //karakterin enerjisine göre elindeki kartlarin görünürlüğünü ayarlamamizi sağliyan method
    public void SelectableCard(bool value)
    {
        for (int i = 0; i < hand.Count; i++)
        {

            Button button = hand[i].GetComponent<Button>();
            if(!value)
            {
                if(hand[i].GetComponent<AttackCardController>() != null)
                {
                    if(hand[i].GetComponent<AttackCardController>().energyCost > character.energyCurrent)
                    {
                        button.interactable = false;
                    }
                }
                else if(hand[i].GetComponent<DefenceCardController>() != null)
                {
                    if(hand[i].GetComponent<DefenceCardController>().energyCost > character.energyCurrent)
                    {
                        button.interactable = false;
                    }
                }
                else if(hand[i].GetComponent<AbilityCardController>() != null)
                {
                    if(hand[i].GetComponent<AbilityCardController>().energyCost > character.energyCurrent)
                    {
                        button.interactable = false;
                    }
                }
                else if(hand[i].GetComponent<StrengthCardController>() != null)
                {
                    if(hand[i].GetComponent<StrengthCardController>().energyCost > character.energyCurrent)
                    {
                        button.interactable = false;
                    }
                }

            }
            else
            {
                button.interactable = true;
            }
        }
    }

    public void InitializeDeck()
    {
        print("Karakter yeni kart kazandi");
        // yeni kart eklme için kullanulıyor
        //yeni kart oluşturunca oluşturulan kart dect  listesine ekle
    }

    
    #endregion

    #region  Map
    //levelerin indexi otamatik tanımlayan bir method
    private void LevelIndexAdjust()
    {
        int levelIndexResult = 0;
        for (int i = 0; i < levelObjects.Count; i++)
        {
            if (i == 0)
            {
                levelObjects[i].LevelIndex = 1;
            }

            if (levelObjects[i].LevelType_Enum != LevelTypeEnums.LevelType_Enum.Change)
            {

                if(i % 2 == 1)
                {
                    if(i == 1)
                    {
                        levelIndexResult = levelObjects[i-1].LevelIndex * 2;
                        levelObjects[i].LevelIndex = levelIndexResult;
                        levelObjects[i+1].LevelIndex = levelIndexResult;
                    }
                    else
                    {
                        levelIndexResult = levelObjects[i-2].LevelIndex *2;
                        levelObjects[i-1].LevelIndex = levelIndexResult;
                        levelObjects[i].LevelIndex = levelIndexResult;
                    }
                }
                else
                {
                    if(i == levelObjects.Count-1)
                    {
                        levelObjects[i].LevelIndex = levelObjects[i-1].LevelIndex * 2;
                    }
                }
                
            }
            else
            {
                levelObjects[i].LevelIndex = levelObjects[i-1].LevelIndex *2;
            }
        }
    }

    #endregion
}
