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
        [Range(0,10)]
        public float Damage;

        [Range(0,10)]
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
            
            GameObject attackCard = new GameObject(cardName,typeof(RectTransform),typeof(CanvasRenderer),typeof(Image),typeof(Button),typeof(AttackCardController));
            

            string folderPath = "Assets/Resources/Prefabs/Cards/AttackCards";
            string prefabPath = Path.Combine(folderPath,attackCard.name + ".prefab");
            
            cardType = CardTypeEnum.Attack;
            
            Image attacCardImage =attackCard.GetComponent<Image>();
            attacCardImage.sprite = cardSprite;
            attacCardImage.type = Image.Type.Sliced;
            
            Button attackCardButton = attackCard.GetComponent<Button>();
            attackCardButton.targetGraphic = attacCardImage;


            attackCard.GetComponent<AttackCardController>().CardInitialize(cardType,cardLegendary,energyCost,duration,cardCombine.combineCardLegendary);

            cardLegendary = CardLegendaryEnum.None;
            cardCombine.combineCardLegendary = null;
            
            cardSprite = null;
            cardName = "";
            energyCost = 0;
            duration = 0;
            ArmorPiercing = 0;

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
        
        [Range(0,10)]
        public float defence;

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
            GameObject defenceCard = new GameObject(cardName,typeof(RectTransform),typeof(CanvasRenderer),typeof(Image),typeof(Button),typeof(DefenceCardController));
    

            string folderPath = "Assets/Resources/Prefabs/Cards/DefenceCards";
            string prefabPath = Path.Combine(folderPath,defenceCard.name + ".prefab");
            cardType = CardTypeEnum.Defence;

            Image defenceCardImage =defenceCard.GetComponent<Image>();
            defenceCardImage.sprite = cardSprite;
            defenceCardImage.type = Image.Type.Sliced;
            
            Button defenceCardButton = defenceCard.GetComponent<Button>();
            defenceCardButton.targetGraphic = defenceCardImage;


            defenceCard.GetComponent<DefenceCardController>().CardInitialize(cardType,cardLegendary,energyCost,duration,cardCombine.combineCardLegendary);
            
            cardCombine.combineCardLegendary = null;
            cardLegendary = CardLegendaryEnum.None;
            cardSprite = null;
            cardName = "";
            energyCost = 0;
            duration = 0;


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
        [Range(0,10)]
        public float ability;

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
            GameObject abilityCard = new GameObject(cardName,typeof(RectTransform),typeof(CanvasRenderer),typeof(Image),typeof(Button),typeof(AbilityCardController));

            string folderPath = "Assets/Resources/Prefabs/Cards/AbilityCards";
            string prefabPath = Path.Combine(folderPath,abilityCard.name + ".prefab");

            cardType = CardTypeEnum.Ability;
            Image defenceCardImage =abilityCard.GetComponent<Image>();
            defenceCardImage.sprite = cardSprite;
            defenceCardImage.type = Image.Type.Sliced;
            
            Button defenceCardButton = abilityCard.GetComponent<Button>();
            defenceCardButton.targetGraphic = defenceCardImage;


            abilityCard.GetComponent<AbilityCardController>().CardInitialize(cardType,cardLegendary,energyCost,duration,cardCombine.combineCardLegendary);
            
            cardLegendary = CardLegendaryEnum.None;
            cardCombine.combineCardLegendary = null;

            cardSprite = null;
            cardName = "";
            energyCost = 0;
            duration = 0;

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
        [Range(0,10)]
        public float strength;

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
            GameObject strengthCard = new GameObject(cardName,typeof(RectTransform),typeof(CanvasRenderer),typeof(Image),typeof(Button),typeof(StrengthCardController));

            string folderPath = "Assets/Resources/Prefabs/Cards/StrengthCards";
            string prefabPath = Path.Combine(folderPath,strengthCard.name + ".prefab");

            cardType = CardTypeEnum.Strength;

            Image defenceCardImage =strengthCard.GetComponent<Image>();
            defenceCardImage.sprite = cardSprite;
            defenceCardImage.type = Image.Type.Sliced;
            
            Button defenceCardButton = strengthCard.GetComponent<Button>();
            defenceCardButton.targetGraphic = defenceCardImage;


            strengthCard.GetComponent<StrengthCardController>().CardInitialize(cardType,cardLegendary,energyCost,duration,cardCombine.combineCardLegendary);

            cardLegendary = CardLegendaryEnum.None;
            cardCombine.combineCardLegendary = null;

            cardSprite = null;
            cardName = "";
            energyCost = 0;
            duration = 0;
            
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