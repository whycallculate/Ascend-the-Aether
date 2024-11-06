using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : CalculateCharacterValues
{
    private UIManager uiManager;
    private Rigidbody rb;
    [SerializeField] private Transform[] points;
    private const int healt = 100;
    private const int shield = 100;
    private const int energy = 5;
    private const int power = 5;
    private int currentHealth;
    private int shieldCurrent;
    private int energyCurrent;
    private int powerCurrent;

    
    private void Awake() 
    {
        currentHealth = health;   
        energyCurrent = energy;
        uiManager = UIManager.Instance; 
        rb = GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        
        
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
