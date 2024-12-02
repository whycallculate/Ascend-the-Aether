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
    public const int damage = 100;
    public const int energy = 5;
    public const int power = 5;
    
    public int currentHealt;
    public int shieldCurrent;
    public int energyCurrent;
    public int powerCurrent;
    [SerializeField] private int characterDamage;
    public int CharacterDamage {get {return characterDamage;} set {characterDamage = value;}}
    
    
    [SerializeField]private CharacterTypeEnum characterTypeEnum;
    public CharacterTypeEnum CharacterTypeEnum {get {return characterTypeEnum;}}
    
    [SerializeField] private CharacterType characterType;
    public CharacterType CharacterType { get { return characterType;}}
    
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


    //karakter'in değerlerini veya özelliklerini artımamizi ve azaltmamizi sağlayan methodlar
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
                    characterType.CharacterSpecialFeature(ref currentHealt,ref shieldCurrent,ref energyCurrent,ref powerCurrent,ref toursCount,value);
                }

                CharacterValueTransaction_Function(traits,transaction,ref isCharacterAlive,ref shieldCurrent,shield,value);
            break;
            
            case "energy":
                CharacterValueTransaction_Function(traits,transaction,ref isCharacterAlive,ref energyCurrent,energy,value);
            break;

            case "power":
                CharacterValueTransaction_Function(traits,transaction,ref isCharacterAlive,ref powerCurrent,power,value);
            break;

            case "damage":
                CharacterValueTransaction_Function(traits,transaction,ref isCharacterAlive,ref characterDamage,damage,value);
            break;

            default:
            break;
        }

        GameManager.Instance.CharcterValueSave(new int[]{currentHealt,shieldCurrent,energyCurrent,powerCurrent,characterDamage});

    }

    //karakter'in yaşayip yaşamadiğini kontrol eden method
    private IEnumerator CharacterAliveControl()
    {
        while(!isCharacterAlive && gameObject.activeSelf)
        {
            CharacterDestroy();
            yield return null;
        }
    }

    //karakter'in healt değeri 0 olduğunda çalişicak method
    private void CharacterDestroy()
    {
        if(currentHealt <= 0)
        {
            if(characterTypeEnum != CharacterTypeEnum.HalfImmortalCharacter)
            {
                Destroy(gameObject);
                GameManager.Instance.LevelReset();
                isCharacterAlive = true;
                SaveSystem.DataRemove("characterFeatures");
                GameManager.Instance.ResetCharacterFeature();
            }
            if(characterTypeEnum == CharacterTypeEnum.HalfImmortalCharacter) 
            {
                characterType.CharacterSpecialFeature();
            }

        }
    }
    
    //karakter'in türünü bulup belirlediğimiz method    
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

    //karakter'in özel özelliği
    public void UseCharacterFeature(int enemyDamage)
    {
        switch(characterTypeEnum)
        {
            case CharacterTypeEnum.AttackCharacter:
                characterType.CharacterSpecialFeature(ref currentHealt,ref shieldCurrent,ref energyCurrent,ref powerCurrent,ref toursCount,characterDamage);
            break;
            
            case CharacterTypeEnum.TankCharacter:
                characterType.CharacterSpecialFeature(ref currentHealt,ref shieldCurrent,ref energyCurrent,ref powerCurrent,ref toursCount,enemyDamage);
            break;
            
            case CharacterTypeEnum.DampingCharacter:
                characterType.CharacterSpecialFeature(ref currentHealt,ref shieldCurrent,ref energyCurrent,ref powerCurrent,ref toursCount,characterDamage);
            break;
            
            case CharacterTypeEnum.SorcererCharacter : 
                characterType.CharacterSpecialFeature();
            break;

            default:
            break;

        }
    }

    //karakter'in kayıtlı olan özelliklerini karakterin özelliklerine tanımlatan method
    public void CharacterRegisteredFeature(int[] features)
    {
        currentHealt = features[0];
        uiManager.CharacterUI.CharacterHealtBar_Function(currentHealt,"",currentHealt);

        shieldCurrent = features[1];
        uiManager.CharacterUI.CharacterShild_Function(shieldCurrent);

        energyCurrent = features[2];
        uiManager.CharacterUI.CharacterEnergy_Function(energyCurrent);

        powerCurrent = features[3];
        uiManager.CharacterUI.CharacterEnergy_Function(energyCurrent);
        
        characterDamage = features[4];
        uiManager.CharacterUI.CharacterDamageUI_Function(characterDamage);

    }
}
