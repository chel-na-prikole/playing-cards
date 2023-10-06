using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "DataStorage", menuName = "ScriptableObjects/DataStorage")]
    public class DataStorage : ScriptableObject
    {
        [field: SerializeField] public CardValuePositionData CardValuePositionData { get; private set; }
        [field: SerializeField] public CardColorData CardColorData { get; private set; }
        [field: SerializeField] public CardValueData CardValueData { get; private set; }
        [field: SerializeField] public CardSuitData CardSuitData { get; private set; }
        [field: SerializeField] public CardSuitPositionData CardSuitPositionData { get; private set; }
        [field: SerializeField] public CardComponentsData CardComponentsData { get; private set; }
        [field: SerializeField] public HighRankData HighRankData { get; private set; }
        [field: SerializeField] public CardBackData CardBackData { get; private set; }
    }
}