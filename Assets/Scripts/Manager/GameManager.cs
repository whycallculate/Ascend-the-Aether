using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    private int numberCardeUsed = 0;
    public int NumberCardeUsed {get {return numberCardeUsed;} set {numberCardeUsed = value;} }
    public CharacterControl character;

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
