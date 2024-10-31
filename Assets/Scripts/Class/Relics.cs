using Card_Enum;
using CardTypes;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

#region Abstract Class
public abstract class Relics
{

    protected List<GameObject> relics = new List<GameObject>();
    public Sprite relicSprite;
    public string relicName;
    public string relicDescription;
    public RelicEnum relicEnumType;


    public int upPower;
    public int downPower;

    public int upEnergy;
    public int downEnergy;

    public int upHealth;
    public int downHealth;

    public int upShield;
    public int downShield;

    public float attackPercent;
    public float shieldPercent;

    public abstract void RelicCreated();
    public abstract bool IsDataFilled();
    //Spesifik olarak istenilen ozellikler buraya eklenecektir...

}
#endregion

#region StatRelic Class
[System.Serializable]
public class StatRelics : Relics
{
    public override bool IsDataFilled()
    {
        return relicSprite != null && relicName != "";
    }

    public override void RelicCreated()
    {
        GameObject statRelics = new GameObject(relicName, typeof(SpriteRenderer), typeof(RelicController));

        relics.Add(statRelics);

        string folderPath = "Assets/Prefabs/Relic";
        string prefabPath = Path.Combine(folderPath, statRelics.name + ".prefab");

        statRelics.GetComponent<SpriteRenderer>().sprite = relicSprite;
        statRelics.GetComponent<RelicController>().
            RelicInitialize(upPower,downPower, upEnergy,downEnergy,upHealth,downHealth,upShield,downShield,attackPercent,shieldPercent);

        relicEnumType = RelicEnum.NONE;
        relicSprite = null;
        relicName = "";



        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        PrefabUtility.SaveAsPrefabAsset(statRelics, prefabPath);


#if UNITY_EDITOR
        Object.DestroyImmediate(statRelics);
#endif
    }

}
#endregion

#region StatusRelic
[System.Serializable]
public class StatusRelics : Relics
{



    public StatusEffect effectType; // Etki türünü belirler
    public int effectValue; 

    public override bool IsDataFilled()
    {
        return relicSprite != null && relicName != "";
    }
    public override void RelicCreated()
    {
        GameObject statusRelic = new GameObject(relicName, typeof(SpriteRenderer), typeof(RelicController));

        relics.Add(statusRelic);

        string folderPath = "Assets/Prefabs/Relic";
        string prefabPath = Path.Combine(folderPath, statusRelic.name + ".prefab");

        statusRelic.GetComponent<SpriteRenderer>().sprite = relicSprite;
        statusRelic.GetComponent<RelicController>().
            RelicInitialize(upPower, downPower, upEnergy, downEnergy, upHealth, downHealth, upShield, downShield, attackPercent, shieldPercent);
        statusRelic.GetComponent<RelicController>().InitializeStatusEffect(effectType, effectValue);

        relicEnumType = RelicEnum.NONE;
        relicSprite = null;
        relicName = "";



        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        PrefabUtility.SaveAsPrefabAsset(statusRelic, prefabPath);


#if UNITY_EDITOR
        Object.DestroyImmediate(statusRelic);
#endif
    }
}
#endregion