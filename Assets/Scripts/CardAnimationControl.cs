using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardAnimationControl : MonoBehaviour
{

  
    public  void CardHareketAnimation(CardAnimationControl card,int positionIndex,float rotationValue,List<CardAnimationPosition> vectors)
    {
        Vector2 rotation = Vector2.zero;
        
        Sequence sequence = DOTween.Sequence();
        RectTransform rectTransform = card.GetComponent<RectTransform>(); 
        
        
        for (int i = 0; i < vectors[positionIndex].position.Length; i++)
        {
            if(ListLastControl(i,vectors[positionIndex].position.Length))
            {
                rotation.y += rotationValue;
                
                sequence.Append(rectTransform.DOAnchorPos(new Vector2(vectors[positionIndex].position[i].x, vectors[positionIndex].position[i].y), .1f).OnComplete(delegate
                {
                    rectTransform.DORotate(rotation,1f);
                }));
            }
            else
            {
                rotation.y += rotationValue;
                sequence.Append(rectTransform.DOAnchorPos(new Vector2(vectors[positionIndex].position[i].x, vectors[positionIndex].position[i].y), .5f).OnComplete(delegate
                {
                    rectTransform.DORotate(Vector2.zero,1f);
                    card.transform.SetParent(vectors[positionIndex].parent.transform);
                }));
            }

        }
    }


    /// <summary>
    /// Hand listesinde ki kartların deck'e geri dönme animasyonu kontrol etmemizi sağlayan method.
    /// </summary>
    public void CardReturnMovementAnimation(CardAnimationControl card,int positionIndex,float rotationValue,List<CardAnimationPosition> returnPosition)
    {
        card.transform.SetParent(returnPosition[positionIndex].parent.transform);
        Vector2 rotation = Vector2.zero;
        Sequence sequence = DOTween.Sequence();

        RectTransform rectTransform = card.GetComponent<RectTransform>();
        float y = 0;
        for (int i = 0; i < returnPosition[positionIndex].position.Length; i++)
        {
            y = returnPosition[positionIndex].position[i].y;

            if (ListLastControl(i, returnPosition[positionIndex].position.Length))
            {
                rotation.y += rotationValue;

                sequence.Append(rectTransform.DOAnchorPos(new Vector2(returnPosition[positionIndex].position[i].x, returnPosition[positionIndex].position[i].y), .5f).OnComplete(delegate
                {

                }));
            }
            else
            {
                rotation.y += rotationValue;
                sequence.Append(rectTransform.DOAnchorPos(new Vector2(returnPosition[positionIndex].position[i].x, returnPosition[positionIndex].position[i].y), .5f).OnComplete(delegate
                {
                    card.gameObject.SetActive(false);
                }));
            }


        }

    }

    private bool ListLastControl(int index,int arrayLength)
    {
        return index != arrayLength - 1;
    }
}
