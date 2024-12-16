using System.Collections;
using System.Collections.Generic;
using Card_Enum;
using CharacterType_Enum;
using EnemyFeatures;
using UnityEngine;
using UnityEngine.UI;
using LevelTypeEnums;
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


    public List<GameObject> deck = new List<GameObject>();
    public List<GameObject> hand = new List<GameObject>();
    public int handSize = 5;

    public CharacterControl character;
    public EnemyController enemy;
    public bool isPlayerTurn = true;

    private bool isGameStart = false;

    #region  Level fields
    
    [SerializeField] private GameObject levelsObject;
    [SerializeField] private List<LevelPrefabControl> levelObjects = new List<LevelPrefabControl>();

    public delegate void LevelProgressDelegate(bool value);

    public LevelProgressDelegate levelProgress; 


    private bool finishLevel = false;
    public bool FinishLevel {get {return finishLevel;} set {finishLevel = value;}}

    [SerializeField]private bool isLevelOver = false;
    public bool IsLevelOver {get {return isLevelOver;} set {isLevelOver = value;}}

    private int characterCurrentLevelIndex =1;
    public int CharacterCurrentLevelIndex {get {return characterCurrentLevelIndex;} set {characterCurrentLevelIndex = value;}}
    private string characterCurrentLevelType ;
    public string CharacterCurrentLevelType {get {return characterCurrentLevelType;} set {characterCurrentLevelType = value;}}
    private int currentLevelIndex;
    public int CurrentLevelIndex {get {return currentLevelIndex;} set {currentLevelIndex = value;}}

    private int mapLevelIndex = 0;


    #endregion


    #region  Enemy
    
    [Space]
    [Space]
    [Header("Enemy")]

    [SerializeField] public List<EnemyController> enemys = new List<EnemyController>();
    private int deadEnemyCount = 0;
    
    [SerializeField] private bool isEnemysStan = false;
    public bool IsEnemysStan {get {return isEnemysStan;} set {isEnemysStan = value;}}

    #endregion


    #region  Character

    [Space]
    [Space]
    [Header("Character")]
    
    [SerializeField] private List<CharacterControl> characters = new List<CharacterControl>();

    #endregion


    #region  Card
    [SerializeField] private List<GameObject> gameAllCards;
    public List<GameObject> GameAllCards {get {return gameAllCards;}}

    [SerializeField]private List<GameObject> cards = new List<GameObject>();
    public List<GameObject> Cards {get {return cards;}}

    [SerializeField] private List<string> cardsName = new List<string>();
    public List<string> CardsName {get {return cardsName;}}
    
    private bool cardCombineStart = false;
    public bool CardCombineStart {get {return cardCombineStart;} set {cardCombineStart = value;}}
    
    private bool[] faceRatio = new bool[4]{true,true,true,true};
    private bool[] seventyFivePercent = new bool[4]{true,false,true,true};
    private bool[] fivetyPercent = new bool[4]{true,false,true,false};
    private bool[] twentyFivePercent = new bool[4]{false,false,true,false};
    private bool[] zeroFivePercent = new bool[4]{false,false,false,false};

    #endregion
    
    [SerializeField] private int crystalCount = 0;
    public int CrystalCount {get {return crystalCount;} set { crystalCount = value;}}
    private int refCrystalCount;
    public int RefCrystalCount {get {return refCrystalCount;} set {refCrystalCount = value;}}
    
    [SerializeField] private GameObject resourcesCard;

    private void Awake()
    {
        FindMapLevels();

        if (enemy == null)
        {
            UIManager.Instance.NextTourButton.interactable = false;
            for (int i = 0; i < deck.Count; i++)
            {
                deck[i].GetComponent<Button>().interactable = false;
                deck[i].GetComponent<CardMovement>().enabled = false;
            }
        }
        
        AllCardLoad();
        if(SaveSystem.DataQuery("crystalCount"))
        {
            crystalCount = SaveSystem.DataExtraction("crystalCount",0);
        }
        
        SaveSystem.DataSave("crystalCount",crystalCount);
        refCrystalCount = crystalCount;

        if(SaveSystem.DataExtraction("cardsName","") != "")
        {
            string[] _cardsNames = SaveSystem.DataExtraction("cardsName", "").Split(",");

            for (int i = 0; i < _cardsNames.Length; i++)
            {
                cardsName.Add(_cardsNames[i]);
            }

        }

        if(SaveSystem.DataQuery("levelIndex") && SaveSystem.DataQuery("levelType"))
        {
            characterCurrentLevelType = SaveSystem.DataExtraction("levelType","");
            characterCurrentLevelIndex = SaveSystem.DataExtraction("levelIndex",0);
            LevelOpening();
        }

        CreateEarnedCard();
    

        isGameStart = true;
        
    }


    #region Start And Update Function
    private void Start()
    {
        DrawCards();
        CardTypeFindPositionSet();
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
                    UIManager.Instance.NextTourButton.interactable = true;
                    
                    SelectableCard(false);
                }
            }          
        }
        
        
        

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.SetActiveUI(UIManager.Instance.LevelsPanel.name);
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].transform.SetParent(UIManager.Instance.EarnedGameObject);
                cards[i].transform.position = Vector3.zero;
                cards[i].SetActive(false);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.W))
        {
            NextMapLevel();
        }


        if(Input.GetKeyDown(KeyCode.S))
        {
            character.CharacterTraits_Function("healtbar","-",20);
        }

    }

    #endregion  


    #region 

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
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].GetComponent<Button>().interactable = false;
        }

        HandToDeck();
        isPlayerTurn = false;
        UIManager.Instance.NextTourButton.interactable = false;
        StartCoroutine(enemy.MakeMove());

        enemy = null;
    }

    public void SwitchTurnToPlayer()
    {
        CardCharacterInteraction("energy", "+", 5);
        DrawCards();
        isPlayerTurn = true;
    }
    public void CardCharacterInteraction(string characterTraits,string transaction,int value)
    {
        character.CharacterTraits_Function(characterTraits,transaction,value);
    }

    #endregion


    #region  Level Function
    public void LevelOpening()
    {
        for (int i = 0; i < levelObjects.Count; i++)
        {
            levelObjects[i].NextBackLevelOpen(characterCurrentLevelIndex);
        }
        CardPositionReset();
        if(character != null)
        {
            character.CharacterTraits_Function("energy","+",5);
        }
    }


    //karakter ölünce mevcut level index'ni sifirliyor
    public void LevelReset()
    {
        deadEnemyCount = 0;
        UIManager.Instance.MapPrefab.SetActive(true);
        characterCurrentLevelType = levelObjects[0].LevelType_Enum.ToString();
        characterCurrentLevelIndex = 1;
        LevelOpening();

        SaveSystem.DataSave("cardsName", "");

        CardsClear();

        CardPositionReset();

        CardButtonInteractableControl();

        EnemysClear();

    }

    private void EnemysClear()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] != null)
            {
                Destroy(enemys[i].gameObject);
            }
        }

        enemys.Clear();
    }

    private void CardsClear()
    {
        foreach (GameObject card in cards)
        {
            Destroy(card);
        }

        cards.Clear();
    }

    //kart'ın posizyonunu sıfılıyor
    private void CardPositionReset()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].GetComponent<RectTransform>().anchoredPosition = UIManager.Instance.cardPos[i].anchoredPosition;
            hand[i].SetActive(true);
        }
    }


    #endregion



    #region  Enemy Function


    //Düşman karakteri oluşturmamizi sağliyor
    public void CreatingEnemies(int enemysCount,EnemyController levelEnemyPrefab,Vector3[] _enemyPosition,EnemyFeature[] enemies)
    {
        UIManager.Instance.MapPrefab.SetActive(false);
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
        Destroy(enemy);
        deadEnemyCount++;
        UIManager.Instance.NextTourButton.interactable = false;
        if(deadEnemyCount == enemys.Count)
        {
        
            isLevelOver = true;

            if(finishLevel)
            {
            
                WhatOfKindCharacter(2,4);

                character.CharacterType.FeatureUsed = false;
                SaveSystem.DataSave("characterFeatureUsed", character.CharacterType.FeatureUsed.ToString());
                
            }
            else if(!finishLevel)
            {
            
                WhatOfKindCharacter(2,3);
            
            }
            
            deadEnemyCount = 0;
            enemys.Clear();
        }
        

        CardButtonInteractableControl();
    }

    private void LevelProgress()
    {
        characterCurrentLevelIndex += currentLevelIndex;
        SaveSystem.DataSave("levelIndex", characterCurrentLevelIndex);
        LevelOpening();
        UIManager.Instance.MapPrefab.SetActive(true);
    }

    #endregion




    #region  Character

    //karakter oluşturmamizi sağliyor
    public void CreatingCharacter(int charactersCount,CharacterControl levelCharacterPrefab,Vector3[] _characterPosition)
    {
        for (int i = 0; i < charactersCount; i++)
        {

            CharacterControl characterClone = Instantiate(levelCharacterPrefab,_characterPosition[i],Quaternion.identity);

            if(SaveSystem.DataQuery("characterFeatureUsed"))
            {
                string characterFeatureUsed = SaveSystem.DataExtraction("characterFeatureUsed","");
                print("characterFeatureUsed : " + characterFeatureUsed );
                switch(characterFeatureUsed)
                {
                    case "True":
                        characterClone.CharacterType.FeatureUsed = true;
                    break;
                    
                    case "False":
                        characterClone.CharacterType.FeatureUsed = false;
                    break;

                    default:
                    break;
                
                } 
                print (characterClone.CharacterType.FeatureUsed);
            }

            character = characterClone;
            CharacterUI characterUI =character.GetComponent<CharacterUI>();
            
            UIManager.Instance.CharacterUI = characterUI;
            characterUI.EnergyNumber_Text = UIManager.Instance.EnergyNumber_Text;
            
            characters.Add(characterClone);
        }
        if (isGameStart)
        {
            CharacterValueLoad();
        }
    }

    //karakter'in özel özelliği çalıştıran method
    public void RunCharacterTrait(int enemyDamage)
    {
        character.UseCharacterFeature(enemyDamage);
    }

    /// <summary>
    /// bu methodun düzgün çalişmasi için parametreleri healt,shield,energy,power,damage bu şekilde girilmesi gerekiyor
    /// </summary> <summary>
    /// 
    /// </summary>
    public void CharcterValueSave(int[] characterFeaturesValue)
    {
        //karakterin değerlerini,değişkenlerini kaydetmemizi sağliyan method
        string values = "";
        for (int i = 0; i < characterFeaturesValue.Length; i++)
        {
            if(i != characterFeaturesValue.Length-1)
            {
                values += characterFeaturesValue[i].ToString()+",";
            }
            else
            {
                values += characterFeaturesValue[i];
            }
        }
        SaveSystem.DataSave("characterFeatures",values);
    }

    //karakterin kaydedilmiş olan değerlerini karaktere tanımlamamizi sağliyan method
    public void CharacterValueLoad()
    {
        if(SaveSystem.DataQuery("characterFeatures"))
        {
            string characterFeaturesValue = SaveSystem.DataExtraction("characterFeatures", "");
            string[] _characterFeatures = characterFeaturesValue.Split(",");

            int[] characterFeatures = new int[_characterFeatures.Length];

            for (int i = 0; i < _characterFeatures.Length; i++)
            {
                characterFeatures[i] = int.Parse(_characterFeatures[i]);
            }


            character.CharacterRegisteredFeature(characterFeatures);
        }
    }

    public void ResetCharacterFeature()
    {
        characterCurrentLevelIndex = levelObjects[0].LevelIndex;
        SaveSystem.DataSave("levelIndex", characterCurrentLevelIndex);

        characterCurrentLevelType = levelObjects[0].LevelType_Enum.ToString();
        SaveSystem.DataSave("levelType", characterCurrentLevelType);

        mapLevelIndex = 0;
        SaveSystem.DataSave("mapLevelIndex",mapLevelIndex);
        MapLevelActive();

        if(character.IsCharacterAlive)
        {
            SaveSystem.DataRemove("cardsName");
            if (cards.Count > 0)
            {
                foreach (var item in cards)
                {
                    Destroy(item.gameObject);
                }
            }
        }

    
    }


    #endregion



    #region  Card
    private int allCardCount = 0;
    //dosyada ki bütün kartlari çekmemizi sağliyan fonksiyon
    private void AllCardLoad()
    {
        GameObject[] _gameAllCards = Resources.LoadAll<GameObject>("Prefabs/Cards/AbilityCards");
        gameAllCards.AddRange(_gameAllCards);
        GameObject[] _gameAllCards1 = Resources.LoadAll<GameObject>("Prefabs/Cards/AttackCards");
        gameAllCards.AddRange(_gameAllCards1);
        GameObject[] _gameAllCards2 = Resources.LoadAll<GameObject>("Prefabs/Cards/DefenceCards");
        gameAllCards.AddRange(_gameAllCards2);
        GameObject[] _gameAllCards3 = Resources.LoadAll<GameObject>("Prefabs/Cards/StrengthCards");
        gameAllCards.AddRange(_gameAllCards3);
        allCardCount = _gameAllCards.Length + _gameAllCards1.Length + _gameAllCards2.Length + _gameAllCards3.Length;

    }


    //karakterin enerjisine göre elindeki kartlarin görünürlüğünü ayarlamamizi sağliyan method
    //karakter'in enerjisine göre kartın tıklanabilir tıklanamaz olmasını sağlıyor
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
                        IsCardMovement(i, button,false);
                    }
                    else
                    {
                        IsCardMovement(i, button,true);
                    }
                }
                else if(hand[i].GetComponent<DefenceCardController>() != null)
                {
                    if(hand[i].GetComponent<DefenceCardController>().energyCost > character.energyCurrent)
                    {
                        IsCardMovement(i, button,false);

                    }
                    else
                    {
                        IsCardMovement(i, button,true);

                    }
                }
                else if(hand[i].GetComponent<AbilityCardController>() != null)
                {
                    if(hand[i].GetComponent<AbilityCardController>().energyCost > character.energyCurrent)
                    {
                        IsCardMovement(i, button,false);

                    }
                    else
                    {
                        IsCardMovement(i, button,true);

                    }
                }
                else if(hand[i].GetComponent<StrengthCardController>() != null)
                {
                    if(hand[i].GetComponent<StrengthCardController>().energyCost > character.energyCurrent)
                    {
                        IsCardMovement(i, button,false);

                    }
                    else
                    {
                        IsCardMovement(i, button,true);

                    }
                }

            }
            else
            {
                IsCardMovement(i, button,true);
            }
        }
    }

    public void HandCardPositionAdjust()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            if(hand[i].activeSelf)
            {
                hand[i].GetComponent<RectTransform>().anchoredPosition = UIManager.Instance.cardPos[i].anchoredPosition;

            }
        }


        SelectableCard(false);

    

    }

    //kart objelerin buttonunu tıklanilmamasını sağliyor
    private void CardButtonInteractableControl()
    {
        UIManager.Instance.NextTourButton.interactable = false;
        for (int i = 0; i < hand.Count; i++)
        {
            IsCardMovement(i,hand[i].GetComponent<Button>(),false);
        }
    }

    private void IsCardMovement(int i, Button button,bool isCardMovement)
    {
        button.interactable = isCardMovement;
        hand[i].GetComponent<CardMovement>().enabled = isCardMovement;
    }

    private int randomNumber = 0;
    private int  lastRandomNumber = 0;

    //create the cards we win from the enemy
    
    
    //levelde ki bütün düşmanlari öldürünce kart seçmeni sağliyan method
    [SerializeField] private List<GameObject> earnedCards = new List<GameObject>();
    public void WhatOfKindCharacter(int sameFeatureEarnedCardCount,int earnedCardsMaxCount)
    {
        #region elimizde ki kartlari ve next buttonunu tiklanabilmesini engelleyen kod satirlari
        
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].GetComponent<Button>().interactable = false;
        }
        UIManager.Instance.NextTourButton.interactable = false;
        
        #endregion

        string type = "";

        #region  Karakterin hangi türde olduğuu bulduğumuz kod satirlari

        if(character != null)
        {
            switch (character.CharacterTypeEnum)
            {

                case CharacterTypeEnum.AttackCharacter:
                    type = "Attack";
                    break;

                case CharacterTypeEnum.TankCharacter:
                    type = "Defence";
                    break;

                case CharacterTypeEnum.DampingCharacter:
                    type = "Attack";
                    break;

                case CharacterTypeEnum.HalfImmortalCharacter:
                    type = "Strengh";
                    break;

                case CharacterTypeEnum.SorcererCharacter:
                    type = "Ability";
                    break;

                default:
                    break;

            }

        }

        #endregion

        #region  Karakterin türünde kartlari şeçmemizi sağliyan kod satirlari 

        switch (type)
        {

            case "Attack":
                while(earnedCards.Count != sameFeatureEarnedCardCount)
                {
                    randomNumber = 0;
                    randomNumber = Random.Range(0,gameAllCards.Count-1);
                    GameObject obje = gameAllCards[randomNumber];
                    if (obje.GetComponent<AttackCardController>() != null)
                    {
                        if(!cards.Contains(obje) && !earnedCards.Contains(obje))
                        {
                            earnedCards.Add(obje);
                            
                        }
                    }
                }
                break;

            case "Defence":

                while(earnedCards.Count != sameFeatureEarnedCardCount)
                {
                    randomNumber = 0;
                    randomNumber = Random.Range(0,gameAllCards.Count-1);
                    GameObject obje = gameAllCards[randomNumber];
                    if (obje.GetComponent<DefenceCardController>() != null)
                    {
                        if(!cards.Contains(obje) && !earnedCards.Contains(obje))
                        {
                            earnedCards.Add(obje);
                            
                        }
                    }
                }

                break;

            case "Ability":
            
                while(earnedCards.Count != sameFeatureEarnedCardCount)
                {
                    randomNumber = 0;
                    randomNumber = Random.Range(0,gameAllCards.Count-1);
                    GameObject obje = gameAllCards[randomNumber];
                    if (obje.GetComponent<AbilityCardController>() != null)
                    {
                        if(!cards.Contains(obje) && !earnedCards.Contains(obje))
                        {
                            earnedCards.Add(obje);
                            
                        }
                    }
                }
                break;

            case "Strengh":
                while(earnedCards.Count != sameFeatureEarnedCardCount)
                {
                    randomNumber = 0;
                    randomNumber = Random.Range(0,gameAllCards.Count-1);
                    GameObject obje = gameAllCards[randomNumber];
                    if (obje.GetComponent<StrengthCardController>() != null)
                    {
                        if(!cards.Contains(obje) && !earnedCards.Contains(obje))
                        {
                            earnedCards.Add(obje);
                            
                        }
                    }
                }
                break;

            default:
                break;

        }

        #endregion
        

        
        #region geri kalan kartlari random bir şekilde seçilmesini sağliyan kod satirlari
        

        while(earnedCards.Count != earnedCardsMaxCount)
        {
            randomNumber = 0;
            randomNumber = Random.Range(0,gameAllCards.Count-1);
            GameObject obje = gameAllCards[randomNumber];
            if(!cards.Contains(obje) && !earnedCards.Contains(obje))
            {
                earnedCards.Add(obje);
            }
        }
        
        #endregion

        #region  seçilmiş olan kartlari oluşturmamizi ve o kartlara tiklamamzi saliyan kod satirleri
        
        for (int i = 0; i < earnedCards.Count; i++)
        {
            GameObject newCard = Instantiate(earnedCards[i].gameObject);
            newCard.transform.localScale = Vector3.one;
            newCard.name = earnedCards[i].name;
            newCard.transform.position = UIManager.Instance.EarnedCardsPositions[i].position;
            newCard.transform.SetParent(UIManager.Instance.EarnedGameObject);
            
            

            newCard.GetComponent<Button>().onClick.AddListener(delegate()
            {
                cards.Add(newCard);
                switch (newCard.tag)
                {
                    case "AttackCard":
                        string attackCardsName = $"Prefabs/Cards/AttackCards/{newCard.name}";

                        cardsName.Add(attackCardsName);
                        break;
                    case "DefenceCard":
                        string defenceCardsName = $"Prefabs/Cards/DefenceCards/{newCard.name}";


                        cardsName.Add(defenceCardsName);
                        break;
                    case "AbilityCard":
                        string abilityCardsName = $"Prefabs/Cards/AbilityCards/{newCard.name}";

                        cardsName.Add(abilityCardsName);
                        break;
                    case "StrenghCard":
                        string strenghCardsName = $"Prefabs/Cards/StrengthCards/{newCard.name}";

                        cardsName.Add(strenghCardsName);
                        break;
                    default:
                        break;
                }

                string _cardsName = string.Join(",", cardsName);
                SaveSystem.DataSave("cardsName", _cardsName);

                int siblingIndex = newCard.transform.GetSiblingIndex();
                for (int i = 0; i < UIManager.Instance.EarnedGameObject.transform.childCount; i++)
                {
                    if (i != siblingIndex)
                    {
                        if(UIManager.Instance.EarnedGameObject.transform.GetChild(i).gameObject.activeSelf)
                        {
                            Destroy(UIManager.Instance.EarnedGameObject.transform.GetChild(i).gameObject);
                        }
                    }
                    else
                    {
                        UIManager.Instance.EarnedGameObject.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }

                earnedCards.Clear();
                foreach (var card in cards)
                {
                    if(card != null)
                    {
                        Button cardButton = card.GetComponent<Button>();
                        if (cardButton != null)
                        {
                            if (cardButton.onClick == null)
                            {
                                cardButton.onClick = new Button.ButtonClickedEvent();
                            }
                            cardButton.onClick.RemoveAllListeners();
                            cardButton.onClick.AddListener(() => CardDevelopment.Instance.SelectCardDevelopment(card));
                        }
                    }
                }

                LevelProgress();

                if(finishLevel)
                {
                    ResetCharacterFeature();
                    LevelOpening();
                    finishLevel = false;
                }
            });
        }
        #endregion
        
        
    }


    


    //düşman yok edildiğinde kazanilan kartlari oluşturan method    
    public void CreateEarnedCard()
    {
        string _a = SaveSystem.DataExtraction("cardsName", "");
        string[] _b = _a.Split(",");
        for (int i = 0; i < _b.Length; i++)
        {
            resourcesCard = Resources.Load<GameObject>(_b[i]);
            if (resourcesCard != null)
            {
                GameObject _object = Instantiate(resourcesCard, UIManager.Instance.EarnedGameObject.transform);

                _object.name = _b[i].Split("/")[3];
                _object.transform.localScale = Vector3.one;
                _object.gameObject.SetActive(false);
                //yeni eklenen kod
                _object.GetComponent<Button>().onClick.AddListener(() => CardDevelopment.Instance.SelectCardDevelopment(_object));
                //deck.Add(_object);
                cards.Add(_object);

            }
        }
    }

   



    public void SetActiveCardMovement()
    {
        for (int i = 0; i < gameAllCards.Count; i++)
        {
            gameAllCards[i].GetComponent<CardMovement>().enabled = true;
        }
    }

    //kartlarin posizyonlarini sifirlayan method
    private void CardTypeFindPositionSet()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            switch(hand[i].tag)
            {
                case "AttackCard":
                    hand[i].GetComponent<AttackCardController>().cardPosition = hand[i].GetComponent<RectTransform>().anchoredPosition;
                break;
                case "DefenceCard":
                    hand[i].GetComponent<DefenceCardController>().cardPosition = hand[i].GetComponent<RectTransform>().anchoredPosition;
                break;
                case "AbilityCard":
                    hand[i].GetComponent<AbilityCardController>().cardPosition = hand[i].GetComponent<RectTransform>().anchoredPosition;
                break;
                case "StrenghCard":
                    hand[i].GetComponent<StrengthCardController>().cardPosition = hand[i].GetComponent<RectTransform>().anchoredPosition;
                break;
            }
        }
    }

    private int succesfull = 0;
    private int failed = 0;

    //düzeltme yapılabilir
    //kazandiğimiz kartlari geliştirilmesini sağliyan method
    public void CardDevelopmentRate()
    {
        if(crystalCount > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                bool[] percent = CardDevelopmentRatePercentSelect();
                int _randomNumber = Random.Range(0, percent.Length - 1);
                bool percentResult = percent[_randomNumber];
                if (percentResult)
                {
                    succesfull++;
                }
                else
                {
                    failed++;
                }
            }


            if (succesfull > failed)
            {
                print("Geliştirilme Başarili");
                CardDevelopment.Instance.CardUpgrade();
                UIManager.Instance.CardFeatureValueUpdate(true);
            }
            else if (failed > succesfull)
            {
                print("Geliştirileme Başarisiz");
                UIManager.Instance.CardFeatureValueUpdate(false);
                CardDevelopment.Instance.CardFeatureValues.Clear();
            }
            
            UIManager.Instance.CardFeatureValueButtonClose("Upgrade");

            succesfull = 0;
            failed = 0;
        }
        else if(crystalCount <= 0)
        {
            UIManager.Instance.CardFeatureValueUpdate(false);
            UIManager.Instance.CardFeatureValueButtonClose("All");

        }

    }

    //geliştirmek istediğimiz kartin türünü bulan method
    private CardDevelopmentRateEnum CardDevelopmentRatePercentTypeSelect()
    {
        int number = Random.Range(0,5);
        switch(number)
        {
            case 0:
            return CardDevelopmentRateEnum.FaceRatio;
            
            case 1:
            return CardDevelopmentRateEnum.SeventyFivePercent;
            
            case 2:
            return CardDevelopmentRateEnum.FivetyPercent;
            
            case 3:
            return CardDevelopmentRateEnum.TwentyFivePercent;
            
            default:
            return CardDevelopmentRateEnum.ZeroPercent;

        }
    }

    //kart geliştirme yüzdesi
    private bool[] CardDevelopmentRatePercentSelect()
    {

        switch(CardDevelopmentRatePercentTypeSelect())
        {
            case CardDevelopmentRateEnum.FaceRatio:
            return faceRatio;
            case CardDevelopmentRateEnum.SeventyFivePercent:
            return seventyFivePercent;
            case CardDevelopmentRateEnum.FivetyPercent:
            return fivetyPercent;
            case CardDevelopmentRateEnum.TwentyFivePercent:
            return twentyFivePercent;
            default:
            return zeroFivePercent;
        }
        
    }


    #region Kart Olayinda Kullanilana  Eski Methodlar
    public void CreateCardWinFromEnemy()
    {
        // yeni kart eklme için kullanulıyor
        //yeni kart oluşturunca oluşturulan kart dect  listesine ekle
        
        randomNumber = Random.Range(0,gameAllCards.Count-1);
        lastRandomNumber = randomNumber;
        StartCoroutine(RandomNumber());
    }
    
    //random bir şekilde kart oluşturmaı sağliyor
    private IEnumerator RandomNumber()
    {
        while(lastRandomNumber == randomNumber)
        {
            randomNumber = Random.Range(0,gameAllCards.Count-1);
            yield return null;
        }
        lastRandomNumber = randomNumber;

        GameObject newCard = Instantiate(gameAllCards[randomNumber]);
        newCard.name = gameAllCards[randomNumber].name;
        newCard.transform.SetParent(UIManager.Instance.EarnedGameObject);
        newCard.transform.localScale =Vector3.one;
        newCard.GetComponent<CardMovement>().enabled = false;
        newCard.GetComponent<Button>().onClick.AddListener(()=>CardDevelopment.Instance.SelectCardDevelopment(newCard));
        newCard.SetActive(false);
        cards.Add(newCard);


        switch(newCard.tag)
        {
            case "AttackCard":
                string attackCardsName = $"Prefabs/Cards/AttackCards/{newCard.name}";
                
                cardsName.Add(attackCardsName);
            break;
            case "DefenceCard":
                string defenceCardsName = $"Prefabs/Cards/DefenceCards/{newCard.name}";


                cardsName.Add(defenceCardsName);
            break;
            case "AbilityCard":
                string abilityCardsName = $"Prefabs/Cards/AbilityCards/{newCard.name}";

                cardsName.Add(abilityCardsName);
            break;
            case "StrenghCard":
                string strenghCardsName =$"Prefabs/Cards/StrengthCards/{newCard.name}";

                cardsName.Add(strenghCardsName);
            break;
            default:
            break;
        }
        
        string _cardsName = string.Join(",", cardsName);
        SaveSystem.DataSave("cardsName", _cardsName);
        



    }
    #endregion

    #endregion


    #region  Map
    
    private void FindMapLevels()
    {
        if(SaveSystem.DataQuery("mapLevelIndex"))
        {
            mapLevelIndex = SaveSystem.DataExtraction("mapLevelIndex",0);

        }
        else if(!SaveSystem.DataQuery("mapLevelIndex"))
        {
            mapLevelIndex = 0;
        }

        if (mapLevelIndex != UIManager.Instance.LevelsContent.transform.childCount)
        {
            Transform levels = UIManager.Instance.LevelsContent.transform.GetChild(mapLevelIndex);
            MapLevelActive();
            for (int i = 0; i < levels.childCount; i++)
            {
                levelObjects.Add(levels.GetChild(i).GetComponent<LevelPrefabControl>());
            }

            LevelIndexAdjust();
        }
        else
        {
            for (int i = 0; i < UIManager.Instance.LevelsContent.transform.childCount; i++)
            {
                UIManager.Instance.LevelsContent.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    //levelerin indexi otamatik tanımlayan bir method
   //levelerin indexi otamatik tanımlayan bir method
    private bool isLevelChange = false;
    public void LevelIndexAdjust()
    {
        for (int i = 0; i < levelObjects.Count; i++)
        {
            if (i == 0)
            {
                levelObjects[0].LevelIndex = 1;
                continue;
            }
            
            if(i == 1)
            {
                levelObjects[i].LevelIndex = levelObjects[i-1].LevelIndex *2;
                continue;
            }
            

            if (levelObjects[i].LevelType_Enum == LevelType_Enum.Change)
            {
                levelObjects[i].LevelIndex = levelObjects[i - 1].LevelIndex * 2;
                isLevelChange = !isLevelChange;
            }
            else
            {
                if (i % 2 == 0)
                {
                    if(levelObjects[i-1].LevelType_Enum == LevelType_Enum.Change)
                    {
                        levelObjects[i].LevelIndex = levelObjects[i-1].LevelIndex * 2;
                    }
                    else if(levelObjects[i-1].LevelType_Enum != LevelType_Enum.Change)
                    {
                        if(isLevelChange)
                        {
                            levelObjects[i].LevelIndex = levelObjects[i-1].LevelIndex * 2;
                        }
                        else if(!isLevelChange)
                        {
                            levelObjects[i].LevelIndex = levelObjects[i-1].LevelIndex;
                        }
                        
                    }
                }
                else
                {
                    if(isLevelChange)
                    {
                        levelObjects[i].LevelIndex = levelObjects[i-1].LevelIndex;
                    }
                    else if(!isLevelChange)
                    {
                        levelObjects[i].LevelIndex = levelObjects[i-1].LevelIndex *2;
                    }
                }
            }
        }

        // Butonların interaktifliği ayarlanıyor
        foreach (LevelPrefabControl item in levelObjects)
        {
            item.GetComponent<Button>().interactable = (item.LevelIndex == characterCurrentLevelIndex);
        }
    }

    //harita da ki ilerideki leveleri aktif etmemizi sağliyor
    public void NextMapLevel()
    {
        isLevelChange = false;
        int maxLevelIndexCount = UIManager.Instance.LevelsContent.childCount;
        if(mapLevelIndex != maxLevelIndexCount)
        {
            mapLevelIndex++;
            SaveSystem.DataSave("mapLevelIndex",mapLevelIndex);
        }
        else if(mapLevelIndex == maxLevelIndexCount) 
        {
            mapLevelIndex  = 0;
            SaveSystem.DataSave("mapLevelIndex",mapLevelIndex);
            return;
        }
        levelObjects.Clear();
        FindMapLevels();
        MapLevelActive();
    
    }

    //map de ki level haritalarini indexe eşit ise aktif etmemizi sağliyor
    private void MapLevelActive()
    {
        if(mapLevelIndex != UIManager.Instance.LevelsContent.transform.childCount)
        {
            RectTransform rectTransform = UIManager.Instance.LevelsContent.transform.GetChild(mapLevelIndex).GetComponent<RectTransform>();


            for (int i = 0; i < UIManager.Instance.LevelsContent.transform.childCount; i++)
            {
                if (i == mapLevelIndex)
                {
                    UIManager.Instance.LevelsContent.transform.GetChild(i).gameObject.SetActive(true);
                }
                else if (i != mapLevelIndex)
                {
                    UIManager.Instance.LevelsContent.transform.GetChild(i).gameObject.SetActive(false);
                }
            }

            int height = rectTransform.childCount * 280;

            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, height / 2);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
            RectTransform parent = UIManager.Instance.LevelsContent.GetComponent<RectTransform>();
            parent.anchoredPosition = new Vector2(parent.anchoredPosition.x,height / 2);
            parent.sizeDelta = new Vector2(parent.sizeDelta.x,height);
        }
    }


    #endregion


    #region  Ekonominest

    //oyun kristalini artmasini sağliyan method
    public void CrystalCoinWin(int winCrystalCount)
    {
        crystalCount += winCrystalCount;
        UIManager.Instance.CrystalCount_Text.text = crystalCount.ToString();
        SaveSystem.DataSave("crystalCount",crystalCount);
    }

    //oyun kristalini azalmasini sağliyan method

    public void CrystalCoinLose(int loseCrystalCount)
    {
        int numberControl = crystalCount - loseCrystalCount;
        if(numberControl > 0)
        {
            crystalCount-= loseCrystalCount;
            UIManager.Instance.CrystalCount_Text.text = crystalCount.ToString();
        }
        else
        {
            crystalCount = 0;
            UIManager.Instance.CrystalCount_Text.text = crystalCount.ToString();
        }

        SaveSystem.DataSave("crystalCount",crystalCount);
        refCrystalCount = crystalCount;
    }
    

    #endregion


}
