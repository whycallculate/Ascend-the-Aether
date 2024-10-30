using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card_Enum;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CardTypes
{
    public abstract class CardBase
    {
        protected List<GameObject> cards = new List<GameObject>();
        public Sprite cardSprite;
        public string cardName;
        public string cardDescription;
        public CardLegendaryEnum cardLegendary;
        
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
   

    #region  Attack Card Class

    [System.Serializable]
    public class AttackCard : CardBase
    {
        [Range(0,10)]
        public float ArmorPiercing; //Zırh Delici


        // value of the card do control null or full
        public override bool IsDataFilled()
        {
            return cardSprite != null && cardName != ""; 
        }

        //the attack card have been creating
        public override void CardCreated()
        {
            GameObject attackCard = new GameObject(cardName,typeof(SpriteRenderer),typeof(CardController));
            
            cards.Add(attackCard);


            string folderPath = "Assets/Prefabs/Cards/AttackCards";
            string prefabPath = Path.Combine(folderPath,attackCard.name + ".prefab");
            
            attackCard.GetComponent<SpriteRenderer>().sprite = cardSprite;
            attackCard.GetComponent<CardController>().CardInitialize(cardLegendary,energyCost,duration);

            cardLegendary = CardLegendaryEnum.None;
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
            return cardSprite != null && cardName != "";
        }

        //the attack card have been creating
        public override void CardCreated()
        {
            GameObject defenceCard = new GameObject(cardName, typeof(SpriteRenderer),typeof(CardController));

            cards.Add(defenceCard);

            string folderPath = "Assets/Prefabs/Cards/DefenceCards";
            string prefabPath = Path.Combine(folderPath,defenceCard.name + ".prefab");
            
            defenceCard.GetComponent<SpriteRenderer>().sprite = cardSprite;
            defenceCard.GetComponent<CardController>().CardInitialize(cardLegendary,energyCost,duration);
            
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
            return cardSprite != null && cardName != "";
        }

        //the attack card have been creating
        public override void CardCreated()
        {
            GameObject abilityCard = new GameObject(cardName, typeof(SpriteRenderer),typeof(CardController));
            cards.Add(abilityCard);

            string folderPath = "Assets/Prefabs/Cards/AbilityCards";
            string prefabPath = Path.Combine(folderPath,abilityCard.name + ".prefab");

            abilityCard.GetComponent<SpriteRenderer>().sprite = cardSprite;
            abilityCard.GetComponent<CardController>().CardInitialize(cardLegendary,energyCost,duration);
            
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
            return cardSprite != null && cardName != "";
        }

        //the attack card have been creating
        public override void CardCreated()
        {
            GameObject strengthCard = new GameObject(cardName, typeof(SpriteRenderer),typeof(CardController));
            cards.Add(strengthCard);

            string folderPath = "Assets/Prefabs/Cards/Strength";
            string prefabPath = Path.Combine(folderPath,strengthCard.name + ".prefab");

            strengthCard.GetComponent<SpriteRenderer>().sprite = cardSprite;
            strengthCard.GetComponent<CardController>().CardInitialize(cardLegendary,energyCost,duration);

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