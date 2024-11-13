using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card_Enum;
using System.IO;
using UnityEngine.UI;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CardTypes
{
    #region  common features of the cards
    
    public abstract class CardBase
    {
        [HideInInspector]public List<GameObject> cards = new List<GameObject>();
        [HideInInspector]public CardTypeEnum cardType;
        public Sprite cardSprite;
        public string cardName;
        public string cardDescription;
        public CardLegendaryEnum cardLegendary;
        public CardLegendary cardCombine;

        [Range(0,10)]
        public int energyCost;
        
        [Range(0,10)]
        public int duration;

        public int cardIndex;

        public GameObject effect;
        public AudioClip effectSound;

        public abstract bool IsDataFilled();
        public abstract void CardCreated();

    }
    
    #endregion


    #region  Attack Card Class

    [System.Serializable]
    public class AttackCard : CardBase
    {
        [Range(0,100)]
        public int damage;

        [Range(0,100)]
        public float ArmorPiercing; //Zırh Delici


        // value of the card do control null or full
        public override bool IsDataFilled()
        {
            if(cardCombine.combineCardLegendary.Length > 0)
            {
                if(cardLegendary != CardLegendaryEnum.LegendaryCard)
                {
                    return false;
                }
               
            }
            return cardSprite != null && cardName != "" && cardLegendary != CardLegendaryEnum.None; 
        }

        //the attack card have been creating
        public override void CardCreated()
        {
            
            GameObject cardPrefab = Resources.Load<GameObject>("Prefabs/Card");
            GameObject attackCard = GameObject.Instantiate(cardPrefab);
            
            attackCard.tag = "AttackCard";
            attackCard.name = cardName; 
            
            attackCard.AddComponent(typeof(AttackCardController));
            attackCard.AddComponent(typeof(CardMovement));


            string folderPath = "Assets/Resources/Prefabs/Cards/AttackCards";
            string prefabPath = Path.Combine(folderPath,attackCard.name + ".prefab");
            
            cardType = CardTypeEnum.Attack;
            
            
          


            attackCard.GetComponent<AttackCardController>().CardInitialize(cardSprite,cardName,cardDescription,cardType,cardLegendary,energyCost,duration,cardCombine.combineCardLegendary,damage);

            cardLegendary = CardLegendaryEnum.None;
            cardCombine.combineCardLegendary = null;
            
            cardSprite = null;
            cardName = "";
            cardDescription = "";
            energyCost = 0;
            duration = 0;
            ArmorPiercing = 0;
            damage = 0;

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                PrefabUtility.SaveAsPrefabAsset(attackCard, prefabPath);
            }
            catch (System.Exception)
            {
                
            }
            
            #if UNITY_EDITOR
            Object.DestroyImmediate(attackCard);
            #endif

        }

        
    }

    #endregion



    

    #region  Defence Card Class

    [System.Serializable]
    public class DefenceCard : CardBase
    {
        
        [Range(0,100)]
        public int defence;

        public bool targetsEnemy; //Is it targeting the enemy
        public bool targetsSelf; //Is it targeting your own player
        public bool isAoE; //  is the area effective

        

        // value of the card do control null or full
        public override bool IsDataFilled()
        {
            if(cardCombine.combineCardLegendary.Length > 0)
            {
                if(cardLegendary != CardLegendaryEnum.LegendaryCard)
                {
                    return false;
                }
               
            }
            return cardSprite != null && cardName != "" && cardLegendary != CardLegendaryEnum.None; 
        }

        //the attack card have been creating
        public override void CardCreated()
        {
            GameObject cardPrefab = Resources.Load<GameObject>("Prefabs/Card");
            GameObject defenceCard = GameObject.Instantiate(cardPrefab);

            defenceCard.tag = "DefenceCard";
            defenceCard.name = cardName; 

            defenceCard.AddComponent(typeof(DefenceCardController));
            defenceCard.AddComponent(typeof(CardMovement));


            string folderPath = "Assets/Resources/Prefabs/Cards/DefenceCards";
            string prefabPath = Path.Combine(folderPath,defenceCard.name + ".prefab");
            cardType = CardTypeEnum.Defence;

            
            
            defenceCard.GetComponent<DefenceCardController>().CardInitialize(cardSprite,cardName,cardDescription,cardType,cardLegendary,energyCost,duration,cardCombine.combineCardLegendary,defence);

            
            cardCombine.combineCardLegendary = null;
            cardLegendary = CardLegendaryEnum.None;

            cardSprite = null;
            cardName = "";
            cardDescription = "";
            energyCost = 0;
            duration = 0;
            defence = 0;

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);  
                }

                PrefabUtility.SaveAsPrefabAsset(defenceCard, prefabPath);
            }
            catch (System.Exception)
            {
                
            }
            
            
            #if UNITY_EDITOR
            Object.DestroyImmediate(defenceCard);
            #endif
        }

    }

    #endregion


    

    #region  Ability Card Class

    [System.Serializable]
    public class AbilityCard : CardBase
    {
        [Range(0,100)]
        public int ability;

        public bool targetsEnemy; //Is it targeting the enemy
        public bool targetsSelf; //Is it targeting your own player
        public bool isAoE; //  is the area effective


        // value of the card do control null or full
        public override bool IsDataFilled()
        {
            if(cardCombine.combineCardLegendary.Length > 0)
            {
                if(cardLegendary != CardLegendaryEnum.LegendaryCard)
                {
                    return false;
                }
               
            }
            return cardSprite != null && cardName != "" && cardLegendary != CardLegendaryEnum.None; 
        }

        //the attack card have been creating
        public override void CardCreated()
        {
            GameObject abilityCardPrefab = Resources.Load<GameObject>("Prefabs/Card");
            GameObject abilityCard = GameObject.Instantiate(abilityCardPrefab);

            
            abilityCard.tag = "AbilityCard";
            abilityCard.name = cardName; 

            abilityCard.AddComponent(typeof(AbilityCardController));
            abilityCard.AddComponent(typeof(CardMovement));


            string folderPath = "Assets/Resources/Prefabs/Cards/AbilityCards";
            string prefabPath = Path.Combine(folderPath,abilityCard.name + ".prefab");

            cardType = CardTypeEnum.Ability;


            abilityCard.GetComponent<AbilityCardController>().CardInitialize(cardSprite,cardName,cardDescription,cardType,cardLegendary,energyCost,duration,cardCombine.combineCardLegendary,ability);
            
            cardLegendary = CardLegendaryEnum.None;
            cardCombine.combineCardLegendary = null;

            cardSprite = null;
            cardName = "";
            cardDescription = "";
            energyCost = 0;
            duration = 0;
            ability = 0;

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                PrefabUtility.SaveAsPrefabAsset(abilityCard, prefabPath);
            }
            catch (System.Exception)
            {
                
                throw;
            }
            
           
            #if UNITY_EDITOR
            Object.DestroyImmediate(abilityCard);
            #endif
           
        }

       
    }

    #endregion


    #region  Strength Card Class

    [System.Serializable]
    public class StrengthCard : CardBase
    {
        [Range(0,100)]
        public int strength;

        public int PowerBoost; // Verilen güç artışı miktarı
        public int HealthBoost; // Sağlık artışı miktarı
        public int DefenseBoost;// Savunma artışı miktarı
        public int AttackBoost;// Saldırı gücü artışı miktarı

        //related knowleges with the strength card 
        public bool TargetsEnemy; //Is it targeting the enemy
        public bool TargetsSelf; //Is it targeting your own player
        public bool IsAoE; // is the area effective




        public int StrengthEffectRadius; 





        // value of the card do control null or full
        public override bool IsDataFilled()
        {
            if(cardCombine.combineCardLegendary.Length > 0)
            {
                if(cardLegendary != CardLegendaryEnum.LegendaryCard)
                {
                    return false;
                }
               
            }
            return cardSprite != null && cardName != "" && cardLegendary != CardLegendaryEnum.None; 
        }

        //the attack card have been creating
        public override void CardCreated()
        {
            GameObject cardPrefab = Resources.Load<GameObject>("Prefabs/Card");
            GameObject strengthCard = GameObject.Instantiate(cardPrefab);


            strengthCard.tag = "StrenghCard";
            strengthCard.name = cardName; 

            strengthCard.AddComponent(typeof(StrengthCardController));
            strengthCard.AddComponent(typeof(CardMovement));

            
            string folderPath = "Assets/Resources/Prefabs/Cards/StrengthCards";
            string prefabPath = Path.Combine(folderPath,strengthCard.name + ".prefab");

            cardType = CardTypeEnum.Strength;



            strengthCard.GetComponent<StrengthCardController>().CardInitialize(cardSprite,cardName,cardDescription,cardType,cardLegendary,energyCost,duration,cardCombine.combineCardLegendary,strength);

            cardLegendary = CardLegendaryEnum.None;
            cardCombine.combineCardLegendary = null;

            
            cardSprite = null;
            cardDescription = "";
            cardName = "";
            energyCost = 0;
            duration = 0;
            strength = 0;
            
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                PrefabUtility.SaveAsPrefabAsset(strengthCard, prefabPath);
            }
            catch (System.Exception)
            {
                
                throw;
            }
            
            #if UNITY_EDITOR
            Object.DestroyImmediate(strengthCard);
            #endif

        }

    }

    #endregion




}