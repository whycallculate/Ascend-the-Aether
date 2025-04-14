using System;
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
        [SerializeField] private List<DeckItemPosition> itemPositions = new List<DeckItemPosition>();
        public List<DeckItemPosition> ItemPositions => itemPositions;
        private int index = 0;

        public void DeckItemPositionAbjust(int deckItemPositionCount)
        {
            for (int i = 0; i < deckItemPositionCount; i++)
            {
                DeckItemPosition newItemPosition = Instantiate(deckItemPositionPrefab,deckItenPositionContent).GetComponent<DeckItemPosition>();
                itemPositions.Add(newItemPosition);
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
                        CardObjectCommonFeatures card = FindItemScriptType(itemButton.gameObject);
                        
                        var tuple = DequeueDeckItemPosition();
                        DeckItemPosition deckItemPosition = tuple.Item2;

                        if(tuple.Item1)
                        {
                            card.DeckPosition = deckItemPosition;
                            
                            deckItemPosition.SetDeckItemPosition();
                            RectTransform itemReckTransform = itemButton.GetComponent<RectTransform>();
                            itemReckTransform.sizeDelta = deckItemPosition.GetDeckItemPositionSizeDelta();
                            item.transform.SetParent(deckItemPosition.transform);
                            item.transform.position = deckItemPosition.transform.position;
                            OutItemButtonFromDeck(itemButton);

                        }
                    }
                });
            }
        }
        
        private CardObjectCommonFeatures FindItemScriptType(GameObject item)
        {
            return GameManager.Instance.FindCardType(item);
        }


        private void OutItemButtonFromDeck(Button itemButton)
        {
            if (itemButton.onClick == null)
            {
                itemButton.onClick = new Button.ButtonClickedEvent();
            }

            itemButton.onClick.RemoveAllListeners();
            itemButton.onClick.AddListener(()=>
            {
                print(index);
                itemButton.transform.SetParent(deckItemScrollRectContent);
                if(index > 0)
                {
                    index--;
                }
                AddItemToDeck(itemButton);
            });

        }

        private void AddItemToDeck(Button itemButton)
        {
            ItemButtonReset(itemButton);
            itemButton.onClick.AddListener(()=>
            {
                var tuple = DequeueDeckItemPosition();
                DeckItemPosition deckItemPosition = tuple.Item2;

                if (tuple.Item1)
                {

                    deckItemPosition.SetDeckItemPosition();
                    RectTransform itemReckTransform = itemButton.GetComponent<RectTransform>();
                    itemReckTransform.sizeDelta = deckItemPosition.GetDeckItemPositionSizeDelta();
                    itemButton.transform.SetParent(deckItemPosition.transform);
                    itemButton.transform.position = deckItemPosition.transform.position;
                    
                   
                    OutItemButtonFromDeck(itemButton);
                }
            });
            
            /*
            ItemButtonReset(itemButton);
            var tuple = DequeueDeckItemPosition();
            DeckItemPosition itemPosition = tuple.Item2;
            itemButton.transform.SetParent( itemPosition.transform);
            itemButton.transform.position = itemPosition.transform.position;
            print(itemPosition + "-" + index);
            print(itemPositions[index].name);
            index++;
            */
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


        public Tuple<bool,DeckItemPosition> DequeueDeckItemPosition()
        {
            /*
            if(itemPositions.Count > 0)
            {
                DeckItemPosition deckItemPosition = itemPositions.Dequeue();
                return deckItemPosition;
            }
            */
            if(index < itemPositions.Count)
            {
                DeckItemPosition deckItemPosition = itemPositions[index];
                index++;
                print(index);
                return Tuple.Create(true,deckItemPosition);
            }
            else
            {
                DeckItemPosition deckItemPosition = null;
                return Tuple.Create(false,deckItemPosition);
            }
        }

        public void SetDeckItenPositionFull()
        {
            /*
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
            */
        }

        public void RemoveDeckItenPositionFull()
        {
            /*
            for (int i = 0; i < itemPositions.Count; i++)
            {
                DeckItemPosition deckItemPosition = itemPositions.Dequeue();
                deckItemPosition.RemoveDeckItemPosition();
            }
            */
        }

       

    }

}
