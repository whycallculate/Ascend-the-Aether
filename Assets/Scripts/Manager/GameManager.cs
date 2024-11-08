using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

    #endregion

    private void OnValidate()
    {
        character = FindAnyObjectByType<CharacterControl>();
        

    }
    private void Awake() 
    {
        levelsObject = GameObject.FindGameObjectWithTag("Levels");
        for (int i = 0; i < levelsObject.transform.childCount; i++)
        {
            levelObjects.Add(levelsObject.transform.GetChild(i).GetComponent<LevelPrefabControl>());
        }
    }
    private void Start()
    {
        DrawCards();
    }
    public void InitializeDeck()
    {

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
    }
    public void CardCharacterInteraction(string characterTraits,string transaction,int value)
    {
        if(numberCardeUsed !=4)
        {
            numberCardeUsed++;
        }
        character.CharacterTraits_Function(characterTraits,transaction,value);
    }

    public void LevelOpening()
    {
        for (int i = 0; i < levelObjects.Count; i++)
        {
            levelObjects[i].NextBackLevelOpen(characterCurrentLevelIndex);
        }
    }

}
