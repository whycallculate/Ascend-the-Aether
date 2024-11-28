using System.Collections;
using System.Collections.Generic;
using CharacterType_Enum;
using UnityEngine;

public class CharacterControl : CalculateCharacterValues
{

    [SerializeField]public Object[] CardDeck;

    private UIManager uiManager;
    private CharacterUI cardUI;
    private Rigidbody rb;
    public const int healt = 100;
    public const int shield = 100;
    public const int energy = 5;
    public const int power = 5;
    
    public int currentHealt;
    public int shieldCurrent;
    public int energyCurrent;
    public int powerCurrent;
    
    
    [SerializeField]private CharacterTypeEnum characterTypeEnum;
    
    [SerializeField] private CharacterType characterType;
    public CharacterType CharacterType { get { return characterType;}}
    
    [SerializeField] private int characterDamage;
    public int CharacterDamage {get {return characterDamage;} set {characterDamage = value;}}
    private int toursCount = 0;

    [SerializeField] private bool isCharacterAlive = false;


    private void Awake() 
    {
        currentHealt = healt;   
        energyCurrent = energy;
        powerCurrent = power;
        shieldCurrent = shield;

        uiManager = UIManager.Instance; 
        cardUI = GetComponent<CharacterUI>();

        rb = GetComponent<Rigidbody>();
        
        isCharacterAlive = false;
        
        CharacterTypeScriptAdd();

        cardUI.CharacterInitialize(healt,shield,power,characterDamage);

    }

    private void Update()
    {
        //Deneme();

    }

    private void Deneme()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (characterTypeEnum == CharacterTypeEnum.AttackCharacter)
            {
                characterType.CharacterSpecialFeature(ref currentHealt, ref shieldCurrent, ref energyCurrent, ref powerCurrent, ref characterDamage, ref toursCount);
                print("characterDamage : " + characterDamage + " - " + "shieldCurrent : " + shieldCurrent);
            }
            if (characterTypeEnum == CharacterTypeEnum.DampingCharacter)
            {
                characterType.CharacterSpecialFeature(ref currentHealt, ref shieldCurrent, ref energyCurrent, ref powerCurrent, ref characterDamage, ref toursCount);
                print("current healt : " + currentHealt + " - " + "character damage : " + characterDamage);
            }

            if (characterTypeEnum == CharacterTypeEnum.TankCharacter)
            {
                //characterType.CharacterSpecialFeature(ref currentHealt,ref shieldCurrent,ref powerCurrent,ref isHalfImmortal);
                int value = 30;
                characterType.CharacterSpecialFeature(ref currentHealt, ref shieldCurrent, ref energyCurrent, ref powerCurrent, ref value, ref toursCount);
                print("current shield : " + shieldCurrent);
            }

            if (characterTypeEnum == CharacterTypeEnum.SorcererCharacter)
            {
                characterType.CharacterSpecialFeature(ref currentHealt, ref shieldCurrent, ref energyCurrent, ref powerCurrent, ref toursCount);
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            CharacterTraits_Function("healtbar", "-", 15);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CharacterTraits_Function("healtbar", "+", 15);
        }
    }

    public void CharacterTraits_Function(string traits,string transaction,int value)
    {
        switch(traits)
        {
            case "healtbar":
                float _healt = value;
                float Healt = _healt / 100;
                StartCoroutine(CharacterAliveControl());
                CharacterValueTransaction_Function(traits, transaction, ref isCharacterAlive, ref currentHealt, healt, value, Healt);
            break;

            case "shield":
            
                if(transaction == "-" && shieldCurrent > 0 && characterTypeEnum == CharacterTypeEnum.TankCharacter)
                {
                    characterType.CharacterSpecialFeature(ref currentHealt,ref shieldCurrent,ref energyCurrent,ref powerCurrent,ref value,ref toursCount);
                }

                CharacterValueTransaction_Function(traits,transaction,ref isCharacterAlive,ref shieldCurrent,shield,value);
            break;
            
            case "energy":
                CharacterValueTransaction_Function(traits,transaction,ref isCharacterAlive,ref energyCurrent,energy,value);
            break;

            case "power":
                CharacterValueTransaction_Function(traits,transaction,ref isCharacterAlive,ref powerCurrent,power,value);
            break;

            default:
            break;
        }
    }

    private IEnumerator CharacterAliveControl()
    {
        while(!isCharacterAlive && gameObject.activeSelf)
        {
            CharacterDestroy();
            yield return null;
        }
    }

    private void CharacterDestroy()
    {
        if(currentHealt <= 0)
        {
            if(characterTypeEnum != CharacterTypeEnum.HalfImmortalCharacter)
            {
                Destroy(gameObject);
                GameManager.Instance.LevelReset();
                isCharacterAlive = true;
            }
            if(characterTypeEnum == CharacterTypeEnum.HalfImmortalCharacter) 
            {
                characterType.CharacterSpecialFeature(ref isCharacterAlive);
            }

        }
    }
    
    
    public void CharacterTypeScriptAdd()
    {
        switch(characterTypeEnum)
        {
            case CharacterTypeEnum.AttackCharacter:
                AttackCharacter attackCharacter = gameObject.AddComponent<AttackCharacter>();
                characterType = attackCharacter;
            break;
            
            case CharacterTypeEnum.TankCharacter:
                TankCharacter tankCharacter=  gameObject.AddComponent<TankCharacter>();
                characterType = tankCharacter;
            break;

            case CharacterTypeEnum.DampingCharacter:
                DampingCharacter dampingCharacter =  gameObject.AddComponent<DampingCharacter>();
                characterType = dampingCharacter;
            break;

            case CharacterTypeEnum.HalfImmortalCharacter:
                HalfImmortalCharacter halfImmortalCharacter = gameObject.AddComponent<HalfImmortalCharacter>();
                characterType = halfImmortalCharacter;
            break;

            case CharacterTypeEnum.SorcererCharacter :
                SorcererCharacter sorcererCharacter = gameObject.AddComponent<SorcererCharacter>();
                characterType = sorcererCharacter;
            break;

            default:
            break;

        }
    }


}
