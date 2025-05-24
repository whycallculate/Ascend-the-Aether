using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CardAnimationPositions;
using UnityEngine.EventSystems;
public class CardAnimationControl : MonoBehaviour,IPointerClickHandler
{
    private RectTransform rectTransform;
    private int carAnimationPositionIndex = 0;
    public int CardAnimationPositionIndex { get { return carAnimationPositionIndex;} set { carAnimationPositionIndex = value;}}

    [SerializeField] private bool isStartAnimation = true;
    public bool IsStartAnimation { get { return isStartAnimation; } set { isStartAnimation = value; } }
     
    private bool isDeckAnimation = false;
    public bool IsDeckAnimation { get { return isDeckAnimation; }}
    private bool isReturnBagAnimation = false;

    void Start()
    {
        DOTween.SetTweensCapacity(1000, 1000);
        rectTransform = GetComponent<RectTransform>();
    }


    private Sequence currentSequence;

    public void CarMovementAnimation(CardAnimationPositionData cardAnimationPositionData)
    {
        if (isStartAnimation)
        {
            isDeckAnimation = false;

            if (currentSequence != null && currentSequence.IsActive())
            {
                currentSequence.Kill(); // Önceki sequence'ı sil
            }

            currentSequence = DOTween.Sequence();

            for (int i = 0; i < cardAnimationPositionData.CardAnimationPositions.Length; i++)
            {
                if (cardAnimationPositionData.CardAnimationPositions[i] == null)
                {
                    return;
                }
                Vector2 target = cardAnimationPositionData.CardAnimationPositions[i].localPosition;


                if (i != cardAnimationPositionData.CardAnimationPositions.Length - 1)
                {
                    currentSequence.Append(rectTransform.DOAnchorPos(target, .1f).SetEase(Ease.InSine));
                }
                else
                {
                    currentSequence.Append(rectTransform.DOAnchorPos(target, .1f).SetEase(Ease.InSine)
                        .OnComplete(() => isDeckAnimation = true));
                }
            }
            
        }
    }

   


    /// <summary>
    /// Hand listesinde ki kartların deck'e geri dönme animasyonu kontrol etmemizi sağlayan method.
    /// </summary>
    public void CardReturnMovementAnimation(CardAnimationPositionData cardAnimationPositionData)
    {
        if (isStartAnimation)
        {
            isDeckAnimation = false;

            if (currentSequence != null && currentSequence.IsActive())
            {
                currentSequence.Kill(); // Önceki sequence'ı sil
            }

            RectTransform rectTransform = GetComponent<RectTransform>();
            currentSequence = DOTween.Sequence();

            for (int i = 0; i < cardAnimationPositionData.CardAnimationPositions.Length; i++)
            {
                Vector2 target = cardAnimationPositionData.CardAnimationPositions[i].localPosition;
                if (i != cardAnimationPositionData.CardAnimationPositions.Length - 1)
                    currentSequence.Append(rectTransform.DOAnchorPos(target, .1f).SetEase(Ease.InSine));
                else
                    currentSequence.Append(rectTransform.DOAnchorPos(target, .1f).SetEase(Ease.InSine)).OnComplete(delegate
                    {
                        GameManager.Instance.ReturnCardObjectOldPosition(gameObject);
                        gameObject.SetActive(false);
                    });
                

            }


            
        }
    }

    


    private bool ListLastControl(int index,int arrayLength)
    {
        return index != arrayLength - 1;
    }
    
    private bool isClick = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isStartAnimation)
        {
            isClick = !isClick;
            if (isClick)
            {
                rectTransform.DOAnchorPosY(100, .1f);
            }
            else
            {
                rectTransform.DOAnchorPosY(0, .1f);
            }
        }
    }
}
