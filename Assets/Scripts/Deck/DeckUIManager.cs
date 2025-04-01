using System.Collections;
using System.Collections.Generic;
using CardObjectCommon_Features;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Deck
{
    public class DeckUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject deckItemPositionPrefab;
        [SerializeField] private Transform deckItenPositionContent;
        [SerializeField] private Transform deckItemScrollRectContent;
        [SerializeField] private Button DeckBackReturnButton;
        [SerializeField] private Queue<DeckItemPosition> itemPositions = new Queue<DeckItemPosition>();
        public Queue<DeckItemPosition> ItemPositions => itemPositions;

        public void DeckItemPositionAbjust(int deckItemPositionCount)
        {
            for (int i = 0; i < deckItemPositionCount; i++)
            {
                DeckItemPosition newItemPosition = Instantiate(deckItemPositionPrefab,deckItenPositionContent).GetComponent<DeckItemPosition>();
                itemPositions.Enqueue(newItemPosition);
                newItemPosition.name = newItemPosition.name.Replace("_Prefab(Clone)","");
                newItemPosition.name = newItemPosition.name + $" {i}";
            }
        }
        
        //Bu durum da hata oluşabilir GameManager.Instance.Equipments listesi dolu olmadığı durumda bunu önüne geçilmeli.
        
        public void DeckAllItemObject()
        {
            RectTransform content = deckItemScrollRectContent.GetComponent<RectTransform>();
            Vector2 offset = content.offsetMax;
            offset.x = (GameManager.Instance.Equipments.Count * 150) - (GameManager.Instance.Equipments.Count * 50);

            content.offsetMax = offset;

            for (int i = 0; i < GameManager.Instance.Equipments.Count; i++)
            {
                GameObject item = GameManager.Instance.Equipments[i];
                item.GetComponent<CardMovement>().enabled = false;
                item.SetActive(true);
                item.transform.SetParent(deckItemScrollRectContent);
                Button itemButton = item.GetComponent<Button>();
                if(itemButton.onClick == null)
                {
                    itemButton.onClick = new Button.ButtonClickedEvent();
                }

                itemButton.onClick.RemoveAllListeners();
                itemButton.onClick.AddListener(()=>
                {
                    if(itemPositions.Count > 0)
                    {
                        CardObjectCommonFeatures card = GameManager.Instance.FindCardType(itemButton.gameObject);
                        
                        DeckItemPosition deckItemPosition = DequeueDeckItemPosition();
                        card.DeckPosition = deckItemPosition;
                        print(card.DeckPosition);

                        deckItemPosition.SetDeckItemPosition();
                        RectTransform itemReckTransform = itemButton.GetComponent<RectTransform>();
                        itemReckTransform.sizeDelta = deckItemPosition.GetDeckItemPositionSizeDelta();
                        item.transform.SetParent(deckItemPosition.transform);
                        item.transform.position = deckItemPosition.transform.position;

                        OutItemButtonFromDeck(itemButton,itemReckTransform,card.DeckPosition);
                    }
                });
            }
        }



        private void OutItemButtonFromDeck(Button itemButton,RectTransform itemReckTransform,DeckItemPosition deckPosition)
        {
            ItemButtonReset(itemButton);
            itemButton.onClick.AddListener(delegate
            {
                itemButton.transform.SetParent(deckItemScrollRectContent);
                AddItemToDeck(itemButton,itemReckTransform,deckPosition);
            });
        }

        private void AddItemToDeck(Button itemButton,RectTransform itemReckTransform,DeckItemPosition deckItemPosition)
        {
            ItemButtonReset(itemButton);
            itemButton.onClick.AddListener(()=>
            {
                itemButton.transform.SetParent(deckItemPosition.transform);
                itemButton.transform.position = deckItemPosition.transform.position;
                itemReckTransform.sizeDelta =deckItemPosition.GetDeckItemPositionSizeDelta();
            });
        }

        private void ItemButtonReset(Button itemButton)
        {
            if(itemButton.onClick == null)
            {
                itemButton.onClick = new Button.ButtonClickedEvent();
            }

            itemButton.onClick.RemoveAllListeners();
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                foreach (var item in itemPositions)
                {
                    print(item.name);
                }
            }
        }

        public void DeckBackReturnButtonFunction()
        {
            gameObject.SetActive(false);
            foreach (DeckItemPosition item in itemPositions)
            {
                Destroy(item.gameObject);
            }
        }


        public DeckItemPosition DequeueDeckItemPosition()
        {
            if(itemPositions.Count > 0)
            {
                DeckItemPosition deckItemPosition = itemPositions.Dequeue();
                print(deckItemPosition.name);
                return deckItemPosition;
            }
            return null;
        }

        public void SetDeckItenPositionFull()
        {
            if(itemPositions.Dequeue() != null)
            {
                DeckItemPosition deckItemPosition = itemPositions.Dequeue();
                if(deckItemPosition != null)
                {
                    if(!deckItemPosition.IsPositionFull)
                    {
                        
                        deckItemPosition.SetDeckItemPosition();
                    }
                    else
                    {
                        print(deckItemPosition.name + " dolu");
                    }

                }

            }
        }

        public void RemoveDeckItenPositionFull()
        {
            for (int i = 0; i < itemPositions.Count; i++)
            {
                DeckItemPosition deckItemPosition = itemPositions.Dequeue();
                deckItemPosition.RemoveDeckItemPosition();
            }
        }

       

    }

}
