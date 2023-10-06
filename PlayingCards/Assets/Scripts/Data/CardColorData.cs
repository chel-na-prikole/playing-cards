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
        
        [SerializeField] private Color _cardBodyColor;
        [SerializeField] private Color _darkSuitColor;
        [SerializeField] private Color _lightSuitColor;

        public Color this[CardColor cardColor] => cardColor switch
        {
            CardColor.CardBody => _cardBodyColor,
            CardColor.DarkSuit => _darkSuitColor,
            CardColor.LightSuit => _lightSuitColor,
            _ => throw new ArgumentOutOfRangeException(nameof(cardColor), cardColor, null)
        };
        
        public CardColor this[CardSuit cardSuit] => cardSuit is CardSuit.Clubs or CardSuit.Spades ? CardColor.DarkSuit : CardColor.LightSuit;
    }
}