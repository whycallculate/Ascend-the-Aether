using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDates
{
    [CreateAssetMenu(fileName = "Game Dates", menuName = "Create GameDates Scriptable Object", order = 3)]
    public class GameDate : ScriptableObject
    {
        [SerializeField] private List<string> deckCards = new List<string>();
        public List<string> DecCards { get { return deckCards; } }

    }
}
    
