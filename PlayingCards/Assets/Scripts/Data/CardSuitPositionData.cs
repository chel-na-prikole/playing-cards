using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CardSuitPositionData", menuName = "ScriptableObjects/CardSuitPositionData")]
    public class CardSuitPositionData : ScriptableObject
    {
        [SerializeField] private List<Vector2> _two;
        [SerializeField] private List<Vector2> _three;
        [SerializeField] private List<Vector2> _four;
        [SerializeField] private List<Vector2> _five;
        [SerializeField] private List<Vector2> _six;
        [SerializeField] private List<Vector2> _seven;
        [SerializeField] private List<Vector2> _eight;
        [SerializeField] private List<Vector2> _nine;
        [SerializeField] private List<Vector2> _ten;
        [SerializeField] private List<Vector2> _jack;
        [SerializeField] private List<Vector2> _queen;
        [SerializeField] private List<Vector2> _king;
        [SerializeField] private List<Vector2> _ace;

        public IReadOnlyCollection<Vector2> this[CardValue cardValue] => cardValue switch
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