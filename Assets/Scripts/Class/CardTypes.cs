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
        protected List<GameObject> cards = new List<GameObject>();
        protected CardTypeEnum cardType;
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
            
            GameObject attackCard = new GameObject(cardName,typeof(RectTransform),typeof(CanvasRenderer),typeof(Image),typeof(Button),typeof(CardController));
            
            cards.Add(attackCard);

            string folderPath = "Assets/Prefabs/Cards/AttackCards";
            string prefabPath = Path.Combine(folderPath,attackCard.name + ".prefab");
            
            cardType = CardTypeEnum.Attack;
            
            Image attacCardImage =attackCard.GetComponent<Image>();
            attacCardImage.sprite = cardSprite;
            attacCardImage.type = Image.Type.Sliced;
            
            Button attackCardButton = attackCard.GetComponent<Button>();
            attackCardButton.targetGraphic = attacCardImage;

            attackCard.GetComponent<CardController>().CardInitialize(cardType,cardLegendary,energyCost,duration,cardCombine.combineCardLegendary);

            cardLegendary = CardLegendaryEnum.None;
            cardCombine.combineCardLegendary = null;
            
            cardSprite = null;
            cardName = "";
            energyCost = 0;
            duration = 0;
            ArmorPiercing = 0;

            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            
            PrefabUtility.SaveAsPrefabAsset(attackCard, prefabPath);
            
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
            GameObject defenceCard = new GameObject(cardName,typeof(RectTransform),typeof(CanvasRenderer),typeof(Image),typeof(Button),typeof(CardController));
    
            cards.Add(defenceCard);

            string folderPath = "Assets/Prefabs/Cards/DefenceCards";
            string prefabPath = Path.Combine(folderPath,defenceCard.name + ".prefab");
            
            cardType = CardTypeEnum.Defence;

            Image defenceCardImage =defenceCard.GetComponent<Image>();
            defenceCardImage.sprite = cardSprite;
            defenceCardImage.type = Image.Type.Sliced;
            
            Button defenceCardButton = defenceCard.GetComponent<Button>();
            defenceCardButton.targetGraphic = defenceCardImage;

            defenceCard.GetComponent<CardController>().CardInitialize(cardType,cardLegendary,energyCost,duration,cardCombine.combineCardLegendary);
            
            cardLegendary = CardLegendaryEnum.None;
            cardSprite = null;
            cardName = "";
            energyCost = 0;
            duration = 0;


            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            PrefabUtility.SaveAsPrefabAsset(defenceCard, prefabPath);
            
            
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
            GameObject abilityCard = new GameObject(cardName, typeof(SpriteRenderer),typeof(CardController));
            cards.Add(abilityCard);

            string folderPath = "Assets/Prefabs/Cards/AbilityCards";
            string prefabPath = Path.Combine(folderPath,abilityCard.name + ".prefab");

            cardType = CardTypeEnum.Ability;
            abilityCard.GetComponent<SpriteRenderer>().sprite = cardSprite;
            abilityCard.GetComponent<CardController>().CardInitialize(cardType,cardLegendary,energyCost,duration,cardCombine.combineCardLegendary);
            
            cardLegendary = CardLegendaryEnum.None;

            cardSprite = null;
            cardName = "";
            energyCost = 0;
            duration = 0;

            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            PrefabUtility.SaveAsPrefabAsset(abilityCard, prefabPath);
            
           
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
            GameObject strengthCard = new GameObject(cardName, typeof(SpriteRenderer),typeof(CardController));
            cards.Add(strengthCard);

            string folderPath = "Assets/Prefabs/Cards/Strength";
            string prefabPath = Path.Combine(folderPath,strengthCard.name + ".prefab");

            cardType = CardTypeEnum.Strength;
            strengthCard.GetComponent<SpriteRenderer>().sprite = cardSprite;
            strengthCard.GetComponent<CardController>().CardInitialize(cardType,cardLegendary,energyCost,duration,cardCombine.combineCardLegendary);

            cardLegendary = CardLegendaryEnum.None;
            cardSprite = null;
            cardName = "";
            energyCost = 0;
            duration = 0;
            
            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            PrefabUtility.SaveAsPrefabAsset(strengthCard, prefabPath);
            
            #if UNITY_EDITOR
            Object.DestroyImmediate(strengthCard);
            #endif

        }

    }

    #endregion




}