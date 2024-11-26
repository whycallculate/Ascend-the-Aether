using System.Collections;
using System.Collections.Generic;
using Card_Enum;
using EnemyFeatures;
using LevelTypeEnums;
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
    [SerializeField] private GameObject levelsObject;
    [SerializeField] private List<LevelPrefabControl> levelObjects = new List<LevelPrefabControl>();

    public delegate void LevelProgressDelegate(bool value);

    public LevelProgressDelegate levelProgress; 

    private bool finishLevel = false;
    public bool FinishLevel {get {return finishLevel;} set {finishLevel = value;}}

    private int currentLevelIndex;
    public int CurrentLevelIndex {get {return currentLevelIndex;} set {currentLevelIndex = value;}}

    #endregion

    #region  Enemy
    
    [Space]
    [Space]
    [Header("Enemy")]

    [SerializeField] public List<EnemyController> enemys = new List<EnemyController>();
    private int deadEnemyCount = 0;
        
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
            UIManager.Instance.NextTourButton.interactable = false;
            for (int i = 0; i < deck.Count; i++)
            {
                deck[i].GetComponent<Button>().interactable = false;
                deck[i].GetComponent<CardMovement>().enabled = false;
            }
        }
        
        LevelIndexAdjust();

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
        
    }

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
        
        if(Input.GetKeyDown(KeyCode.V))
        {
            CardCharacterInteraction("healtbar","-",20);

        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            CardCharacterInteraction("healtbar","+",20);
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
        
       
    }

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
        if(numberCardeUsed !=4)
        {
            numberCardeUsed++;
        }
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

        CardPositionReset();

        CardButtonInteractableControl();
        for (int i = 0; i < enemys.Count; i++)
        {
            if(enemys[i] != null)
            {
                Destroy(enemys[i].gameObject);
            }
        }
        enemys.Clear();

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
            if(finishLevel)
            {
                characterCurrentLevelIndex = levelObjects[0].LevelIndex;
                SaveSystem.DataSave("levelIndex",characterCurrentLevelIndex);
                characterCurrentLevelType = levelObjects[0].LevelType_Enum.ToString();
                SaveSystem.DataSave("levelType",characterCurrentLevelType);
            }
            else
            {
                characterCurrentLevelIndex += currentLevelIndex;
                SaveSystem.DataSave("levelIndex",characterCurrentLevelIndex);
            }
            
            
            LevelOpening();
            CreateCardWinFromEnemy();
            UIManager.Instance.MapPrefab.SetActive(true);
            deadEnemyCount = 0;
            enemys.Clear();
        }
        

        CardButtonInteractableControl();
    }


    
    #endregion 
    
    


    #region  Character

    //karakter oluşturmamizi sağliyor
    public void CreatingCharacter(int charactersCount,CharacterControl levelCharacterPrefab,Vector3[] _characterPosition)
    {
        for (int i = 0; i < charactersCount; i++)
        {
            CharacterControl characterClone = Instantiate(levelCharacterPrefab,_characterPosition[i],Quaternion.identity);
            character = characterClone;

            CharacterUI characterUI =character.GetComponent<CharacterUI>();
            
            UIManager.Instance.CharacterUI = characterUI;
            characterUI.EnergyNumber_Text = UIManager.Instance.EnergyNumber_Text;
            
            characters.Add(characterClone);
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


    //düşman yok edildiğinde kazanilan kartlari oluşturan method    
    public void CreateEarnedCard()
    {
        string _a = SaveSystem.DataExtraction("cardsName","");
        string[] _b = _a.Split(",");
        for (int i = 0; i < _b.Length; i++)
        {
            print(_b[i]);
            resourcesCard = Resources.Load<GameObject>(_b[i]);
            if(resourcesCard != null)
            {
                GameObject _object = Instantiate(resourcesCard,UIManager.Instance.EarnedGameObject.transform);
                
                _object.name = _b[i].Split("/")[3];
                _object.transform.localScale = Vector3.one;
                _object.gameObject.SetActive(false);
                //yeni eklenen kod
                _object.GetComponent<Button>().onClick.AddListener(()=>CardDevelopment.Instance.SelectCardDevelopment(_object));
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
        int number = Random.Range(0,4);
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
