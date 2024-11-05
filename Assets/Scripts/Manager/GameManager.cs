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

    private void OnValidate()
    {
        character = FindAnyObjectByType<CharacterControl>();
    }

    public void CardCharacterInteraction(string characterTraits,string transaction,int value)
    {
        if(numberCardeUsed !=4)
        {
            numberCardeUsed++;
        }
        character.CharacterTraits_Function(characterTraits,transaction,value);
    }
}
