using System;
using Enums;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CardColorData", menuName = "ScriptableObjects/CardColorData")]
    public class CardColorData : ScriptableObject
    {
        [field: SerializeField] public CardColor CardBodyColor { get; private set; }
        [field: SerializeField] public CardColor BorderColor { get; private set; }
        
        [SerializeField] private Color _cardWhite;
        [SerializeField] private Color _cardBlack;
        [SerializeField] private Color _cardRed;

        public Color this[CardColor cardColor] => cardColor switch
        {
            CardColor.White => _cardWhite,
            CardColor.Black => _cardBlack,
            CardColor.Red => _cardRed,
            _ => throw new ArgumentOutOfRangeException(nameof(cardColor), cardColor, null)
        };
        
        public CardColor this[CardSuit cardSuit] => cardSuit is CardSuit.Clubs or CardSuit.Spades ? CardColor.Black : CardColor.Red;
    }
}