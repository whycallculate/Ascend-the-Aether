using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class DeckManager : MonoBehaviour
    {
        private DeckUIManager deckUIManager;

        void OnEnable()
        {
            if(deckUIManager == null)
            {
                deckUIManager = GetComponent<DeckUIManager>();
            }

            deckUIManager.DeckItemPositionAbjust(6);

            deckUIManager.DeckAllItemObject();
        }
        


        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        

           
        }
    }

}
