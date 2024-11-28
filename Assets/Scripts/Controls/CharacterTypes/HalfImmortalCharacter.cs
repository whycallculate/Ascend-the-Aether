using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfImmortalCharacter : CharacterType
{
    

    public override void CharacterSpecialFeature(ref bool isCharacterAlive)
    {

        if(GameManager.Instance.CharacterCurrentLevelIndex == 1 && !featureUsed)
        {
            Deneme(false);
            StartCoroutine(Dirilt());
            hasHealed = false;
        }
        else if(featureUsed)
        {
            Destroy(gameObject);
            GameManager.Instance.LevelReset();
        }

    }

    
    private void Deneme(bool value)
    {
        CharacterControl character = GameManager.Instance.character;
        character.GetComponent<MeshRenderer>().enabled  = value;
        character.GetComponent<CapsuleCollider>().enabled = value;
        character.transform.GetChild(0).gameObject.SetActive(value);
    }
    
    private bool hasHealed = false;
    private IEnumerator Dirilt()
    {
        yield return new WaitForSeconds(1);
        if(!hasHealed)
        {
            GameManager.Instance.character.CharacterTraits_Function("healtbar","+",50);
            hasHealed=  true;
        }
        Deneme(true);

        featureUsed = true;
    }
    
}
