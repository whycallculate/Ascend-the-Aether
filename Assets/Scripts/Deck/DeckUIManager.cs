using System;
using System.Collections;
using System.Collections.Generic;
using CardObjectCommon_Features;
using GameDates;
using Item;
using UnityEngine;
using UnityEngine.UI;

namespace Deck
{
    public class DeckUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject deckItemPositionPrefab;
        [SerializeField] private GameDate gameDateScriptableObject;
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
                DeckItemPosition newItemPosition = Instantiate(deckItemPositionPrefab, deckItenPositionContent).GetComponent<DeckItemPosition>();
                itemPositions.Add(newItemPosition);
                newItemPosition.name = newItemPosition.name.Replace("_Prefab(Clone)", "");
                newItemPosition.name = newItemPosition.name + $" {i}";
            }
        }

        //Bu durum da hata oluşabilir GameManager.Instance.Equipments listesi dolu olmadığı durumda bunu önüne geçilmeli.
        //Equipment olan objeleri oluşturmamizi ve oluşturulan objelerin button'larına method tanımlamamızı sağlıyor.
        public void DeckAllItemObject()
        {
            RectTransform content = deckItemScrollRectContent.GetComponent<RectTransform>();
            Vector2 offset = content.offsetMax;
            offset.x = (GameManager.Instance.Equipments.Count * 150) - (GameManager.Instance.Equipments.Count * 50);

            content.offsetMax = offset;

            for (int i = 0; i < GameManager.Instance.Equipments.Count; i++)
            {
                GameObject item = GameManager.Instance.Equipments[i];
                GameManager.Instance.CardAnimationController.CardMovementAnimationClose(item);
               
                if (item != null)
                {
                    SaveCardControlPositionAdjust(item);
                }
            }

            DecItemsInformationUIClose();
            positionIndex =0;
        
        }

        public void DecItemsInformationUIClose()
        {
            for (int i = 0; i < GameManager.Instance.Equipments.Count; i++)
            {
                GameManager.Instance.Equipments[i].GetComponent<ItemUI>().CloseItemUIActive();
            }
        }


        int positionIndex = 0;
        private void SaveCardControlPositionAdjust(GameObject card)
        {
            bool query = gameDateScriptableObject.DecCards.Contains(card.transform.name);
            if(query)
            {
                if(positionIndex < itemPositions.Count)
                {
                    PlacementToEquipmentCardDeckPosition(card);
                }
                else
                {
                    card.SetActive(true);
                    card.GetComponent<CardMovement>().enabled = false;
                    card.transform.SetParent(deckItemScrollRectContent);
                    Button cardButton = card.GetComponent<Button>();

                    if (cardButton.onClick == null) cardButton.onClick = new Button.ButtonClickedEvent();

                    cardButton.onClick.RemoveAllListeners();

                    cardButton.onClick.AddListener(()=>
                    {
                        AddItemToDeck(cardButton);
                    });
                    cardButton.onClick.Invoke();
                }

            }
            else
            {
                card.GetComponent<CardMovement>().enabled = false;
                card.SetActive(true);
                card.transform.SetParent(deckItemScrollRectContent);

                Button itemButton = card.GetComponent<Button>();
                if (itemButton.onClick == null)
                {
                    itemButton.onClick = new Button.ButtonClickedEvent();
                }

                itemButton.onClick.RemoveAllListeners();
                itemButton.onClick.AddListener(() =>
                {
                    if (itemPositions.Count > 0)
                    {
                        CardObjectCommonFeatures card = FindItemScriptType(itemButton.gameObject);

                        var tuple = DequeueDeckItemPosition();
                        DeckItemPosition deckItemPosition = tuple.Item2;

                        if (tuple.Item1)
                        {
                            card.DeckPosition = deckItemPosition;

                            if (deckItemPosition != null)
                            {
                                if (!deckItemPosition.IsPositionFull)
                                {
                                    deckItemPosition.SetDeckItemPosition();
                                    RectTransform itemReckTransform = itemButton.GetComponent<RectTransform>();
                                    itemReckTransform.sizeDelta = deckItemPosition.GetDeckItemPositionSizeDelta();
                                    card.transform.SetParent(deckItemPosition.transform);
                                    card.transform.position = deckItemPosition.transform.position;
                                    OutItemButtonFromDeck(itemButton, deckItemPosition);

                                }

                            }

                        }
                    }
                });
            }
        }

        private void PlacementToEquipmentCardDeckPosition(GameObject card)
        {
            DeckItemPosition deckItemPosition = itemPositions[FindCardForDeckPositionIndex(card)];
            deckItemPosition.SetDeckItemPosition();

            card.SetActive(true);
            card.GetComponent<CardMovement>().enabled = false;

            card.transform.SetParent(deckItemPosition.transform);
            card.transform.position = deckItemPosition.transform.position;

            Button cardButton = card.GetComponent<Button>();
            card.transform.localPosition = Vector3.zero;

            if (cardButton.onClick == null) cardButton.onClick = new Button.ButtonClickedEvent();

            cardButton.onClick.RemoveAllListeners();

            cardButton.onClick.AddListener(() =>
            {
                if (deckItemPosition != null)
                {
                    deckItemPosition.RemoveDeckItemPosition();
                    cardButton.transform.SetParent(deckItemScrollRectContent);
                    AddItemToDeck(cardButton);

                }
            });

            positionIndex++;
        }

        public int FindCardForDeckPositionIndex(GameObject card)
        {
            for (int i = 0; i < gameDateScriptableObject.DecCards.Count; i++)
            {
                if(card.name == gameDateScriptableObject.DecCards[i])
                {
                    if(!itemPositions[i].IsPositionFull) return i;
                }
            }
            return -1;
        }


        private CardObjectCommonFeatures FindItemScriptType(GameObject item)
        {
            return GameManager.Instance.FindCardType(item);
        }


        private void OutItemButtonFromDeck(Button itemButton, DeckItemPosition deckItemPosition)
        {
            if (itemButton.onClick == null)
            {
                itemButton.onClick = new Button.ButtonClickedEvent();
            }

            itemButton.onClick.RemoveAllListeners();
            itemButton.onClick.AddListener(() =>
            {
                if (deckItemPosition != null)
                {
                    if (deckItemPosition.IsPositionFull)
                    {
                        deckItemPosition.RemoveDeckItemPosition();
                        itemButton.transform.SetParent(deckItemScrollRectContent);
                        AddItemToDeck(itemButton);
                    }
                }
            });

        }

        private void AddItemToDeck(Button itemButton)
        {
            ItemButtonReset(itemButton);
            itemButton.onClick.AddListener(() =>
            {
                var tuple = DequeueDeckItemPosition();
                DeckItemPosition deckItemPosition = tuple.Item2;

                if (tuple.Item1)
                {
                    if (deckItemPosition != null)
                    {
                        if (!deckItemPosition.IsPositionFull)
                        {
                            deckItemPosition.SetDeckItemPosition();
                            RectTransform itemReckTransform = itemButton.GetComponent<RectTransform>();
                            itemReckTransform.sizeDelta = deckItemPosition.GetDeckItemPositionSizeDelta();
                            itemButton.transform.SetParent(deckItemPosition.transform);
                            itemButton.transform.position = deckItemPosition.transform.position;


                            OutItemButtonFromDeck(itemButton, deckItemPosition);
                        }
                    }
                }
            });

        }

        private void ItemButtonReset(Button itemButton)
        {
            if (itemButton.onClick == null)
            {
                itemButton.onClick = new Button.ButtonClickedEvent();
            }

            itemButton.onClick.RemoveAllListeners();
        }

       

        public void DeckBackReturnButtonFunction()
        {
            
            for (int j = 0; j < itemPositions.Count; j++)
            {
               if (itemPositions[j] != null)
                {
                    if (itemPositions[j].transform.childCount > 0)
                    {
                        itemPositions[j].RemoveDeckItemPosition();
                        GameObject card = itemPositions[j].transform.GetChild(0).gameObject;
                        card.transform.SetParent(UIManager.Instance.DectGameObject.transform);
                        card.transform.position = Vector3.zero;
                        card.SetActive(false);
                        gameDateScriptableObject.DecCards[j] = card.name; 
                    }
                    Destroy(itemPositions[j].gameObject);
                } 
            }

            for (int i = 0; i < deckItemScrollRectContent.childCount; i++)
            {
                Transform card = deckItemScrollRectContent.transform.GetChild(i);
                card.SetParent(UIManager.Instance.DectGameObject.transform);
                card.gameObject.SetActive(false);
            }

            
            positionIndex = 0;
            itemPositions.Clear();
            gameObject.SetActive(false);

        }


        public Tuple<bool, DeckItemPosition> DequeueDeckItemPosition()
        {
            if (index < itemPositions.Count)
            {
                DeckItemPosition deckItemPosition = SetAndFindNotFullDeckPosition();

                return Tuple.Create(true, deckItemPosition);
            }
            else
            {
                DeckItemPosition deckItemPosition = null;
                return Tuple.Create(false, deckItemPosition);
            }
        }

        private DeckItemPosition SetAndFindNotFullDeckPosition()
        {
            DeckItemPosition deckItemPosition = null;
            for (int i = 0; i < itemPositions.Count; i++)
            {
                if (itemPositions[i].IsPositionFull) continue;
                else
                {
                    deckItemPosition = itemPositions[i];
                    break;
                }
            }
            return deckItemPosition;
        }

        public bool ControlDeckPositionFull()
        {
            for (int i = 0; i < itemPositions.Count; i++)
            {
                if(itemPositions[i].IsPositionFull) return true;
            }

            return false;
        }

    }

}
