using System;
using Enums;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CardSuitData", menuName = "ScriptableObjects/CardSuitData")]
    public class CardSuitData : ScriptableObject
    {
        [SerializeField] private Sprite _clubs;
        [SerializeField] private Sprite _spades;
        [SerializeField] private Sprite _hearts;
        [SerializeField] private Sprite _diamonds;

        public Sprite this[CardSuit cardSuit] => cardSuit switch 
        {
            CardSuit.Clubs => _clubs,
            CardSuit.Spades => _spades,
            CardSuit.Diamonds => _diamonds,
            CardSuit.Hearts => _hearts,
            _ => throw new ArgumentOutOfRangeException(nameof(cardSuit), cardSuit, null)
        };
    }
}