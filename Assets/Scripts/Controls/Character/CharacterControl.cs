using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : CalculateCharacterValues
{

    [SerializeField]public Object[] CardDeck;

    private UIManager uiManager;
    private Rigidbody rb;
    [SerializeField] private Transform[] points;
    public const int healt = 100;
    public const int shield = 100;
    public const int energy = 5;
    public const int power = 5;
    public int currentHealt;
    public int shieldCurrent;
    public int energyCurrent;
    public int powerCurrent;

    
    private void Awake() 
    {
        currentHealt = healt;   
        energyCurrent = energy;
        uiManager = UIManager.Instance; 
        rb = GetComponent<Rigidbody>();
    }



    public void CharacterTraits_Function(string traits,string transaction,int value)
    {
        switch(traits)
        {
            case "healtbar":
                float _healt = 20;
                float Healt = _healt / 100;
                CharacterValueTransaction_Function(traits,transaction,ref currentHealt,healt,value,Healt);
            break;

            case "shield":
                CharacterValueTransaction_Function(traits,transaction,ref shieldCurrent,shield,value);
            break;
            
            case "energy":
                CharacterValueTransaction_Function(traits,transaction,ref energyCurrent,energy,value);
            break;

            case "power":
                CharacterValueTransaction_Function(traits,transaction,ref powerCurrent,power,value);
            break;

            default:
            break;
        }
    }

    
    
}
