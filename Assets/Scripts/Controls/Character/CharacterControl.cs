using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : CalculateCharacterValues
{
    private UIManager uiManager;
    private const int healt = 100;
    private const int shield = 100;
    private const int energy = 5;
    private const int power = 5;
    private int currentHealt;
    private int shieldCurrent;
    private int energyCurrent;
    private int powerCurrent;
    
    private void Awake() 
    {
        currentHealt = healt;   
        energyCurrent = energy;
        uiManager = UIManager.Instance; 
        
    }
   
    
}
