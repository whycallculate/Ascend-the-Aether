using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardObjectCommon_Features 
{
    public abstract class CardObjectCommonFeatures : MonoBehaviour
    {
        private bool _isClickCard = false;
        
        //method to make the card move
        public bool CardObject_Movement(GameObject card,string characterTraits,string transaction,int value)
        {
            if(!_isClickCard)
            {
                GameManager.Instance.CardCharacterInteraction(characterTraits,transaction,value);
            }

            _isClickCard = !_isClickCard;
            StartCoroutine(CardMovementIEnumerator(card));
            return _isClickCard;
        }


        private IEnumerator CardMovementIEnumerator(GameObject card)
        {
            while(_isClickCard)
            {
                float x = Mathf.Clamp(Input.mousePosition.x,-Screen.width,Screen.width);
                float y = Mathf.Clamp(Input.mousePosition.y,-Screen.height,Screen.height);

                    card.transform.position = new Vector2(x,y);
                Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height/2);
                if(transform.position == screenCenter)
                {
                    print("ortada");
                }   
                yield return null;
            }
        }
    }

}
