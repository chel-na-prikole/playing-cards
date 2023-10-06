using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CardValuePositionData", menuName = "ScriptableObjects/CardValuePositionData")]
    public class CardValuePositionData : ScriptableObject
    {
        [SerializeField] private List<Vector2> _cardValuePositions;

        public IReadOnlyList<Vector2> CardValuePositions => _cardValuePositions;
    }
}