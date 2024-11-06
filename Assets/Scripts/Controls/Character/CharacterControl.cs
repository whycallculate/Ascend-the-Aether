using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : CalculateCharacterValues
{
    private UIManager uiManager;
    private const int health = 100;
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
        
    }
   
    
}
