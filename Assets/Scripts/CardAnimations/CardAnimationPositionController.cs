using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardAnimationPositions
{
    public class CardAnimationPositionController : MonoBehaviour
    {

        [SerializeField] private List<CardAnimationPositionData> cardPositionDates = new List<CardAnimationPositionData>();
        public List<CardAnimationPositionData> CardPositionDates => cardPositionDates;

        [SerializeField] private List<CardAnimationPositionData> cardReturnPositionDates = new List<CardAnimationPositionData>();
        public List<CardAnimationPositionData> CardReturnPositionDates => cardReturnPositionDates;
        
    }


    [Serializable]
    public class CardAnimationPositionData
    {
        public Transform cardPositionParent;
        [SerializeField] private Transform[] cardAnimationPositions;
        public Transform[] CardAnimationPositions => cardAnimationPositions;
    }

}