using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterType : MonoBehaviour
{
    protected bool featureDisables = false;
    public bool FeatureDisables {get {return featureDisables;} set {featureDisables = value;} }

    protected bool featureUsed = false;
    public bool FeatureUsed { get { return featureUsed; }  set { featureUsed = value; } }
    public virtual void CharacterSpecialFeature(ref int healt, ref int shield,ref int energy,ref int power,ref int toursCount)
    {

    }
    public virtual void CharacterSpecialFeature(ref int healt, ref int shield,ref int energy,ref int power,ref int damage,ref int toursCount)
    {

    }

    public virtual void CharacterSpecialFeature(ref bool isCharacterAlive)
    {

    }
}
