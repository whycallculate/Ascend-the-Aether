using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckItemPosition : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private bool isPositionFull = false;
    public bool IsPositionFull => isPositionFull;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetDeckItemPosition()
    {
        isPositionFull = true;
    }

    public void RemoveDeckItemPosition()
    {
        isPositionFull = false;
    }

    public Vector2 GetDeckItemPositionSizeDelta()
    {
        return rectTransform.sizeDelta;

    }

}


