using System;
using Enums;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CardValueData", menuName = "ScriptableObjects/CardValueData")]
    public class CardValueData : ScriptableObject
    {
        [SerializeField] private Sprite _two;
        [SerializeField] private Sprite _three;
        [SerializeField] private Sprite _four;
        [SerializeField] private Sprite _five;
        [SerializeField] private Sprite _six;
        [SerializeField] private Sprite _seven;
        [SerializeField] private Sprite _eight;
        [SerializeField] private Sprite _nine;
        [SerializeField] private Sprite _ten;
        [SerializeField] private Sprite _jack;
        [SerializeField] private Sprite _queen;
        [SerializeField] private Sprite _king;
        [SerializeField] private Sprite _ace;

        public Sprite this[CardValue cardValue] => cardValue switch
        {
            CardValue.Two => _two,
            CardValue.Three => _three,
            CardValue.Four => _four,
            CardValue.Five => _five,
            CardValue.Six => _six,
            CardValue.Seven => _seven,
            CardValue.Eight => _eight,
            CardValue.Nine => _nine,
            CardValue.Ten => _ten,
            CardValue.Jack => _jack,
            CardValue.Queen => _queen,
            CardValue.King => _king,
            CardValue.Ace => _ace,
            _ => throw new ArgumentOutOfRangeException(nameof(cardValue), cardValue, null)
        };
    }
}