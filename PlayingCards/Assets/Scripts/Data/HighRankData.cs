using System;
using Enums;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "HighRankData", menuName = "ScriptableObjects/HighRankData")]
    public class HighRankData : ScriptableObject
    {
        private const CardValue FirstHighRank = CardValue.King;
        private const CardValue SecondHighRank = CardValue.Queen;
        private const CardValue ThirdHighRank = CardValue.Jack;
        
        [field: SerializeField] public Vector2 HighRankSpritePosition { get; private set; }

        [SerializeField] private Sprite _firstHighRank;
        [SerializeField] private Sprite _secondHighRank;
        [SerializeField] private Sprite _thirdHighRank;

        public Sprite this[CardValue cardValue] => cardValue switch
        {
            FirstHighRank => _firstHighRank,
            SecondHighRank => _secondHighRank,
            ThirdHighRank => _thirdHighRank,
            _ => throw new ArgumentOutOfRangeException(nameof(cardValue), cardValue, null)
        };

        public bool GetIsHighRank(CardValue cardValue) => cardValue is FirstHighRank or SecondHighRank or ThirdHighRank;
    }
}