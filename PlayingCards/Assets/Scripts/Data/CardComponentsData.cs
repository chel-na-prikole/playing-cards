using UnityEngine;
using Views;

namespace Data
{
    [CreateAssetMenu(fileName = "CardComponentsData", menuName = "ScriptableObjects/CardComponentsData")]
    public class CardComponentsData : ScriptableObject
    {
        [field: SerializeField] public CardBodyView CardBody { get; private set; }
        [field: SerializeField] public ValueView ValueView { get; private set; }
        [field: SerializeField] public SuitView SuitView { get; private set; }
        [field: SerializeField] public HighRankView HighRankView { get; private set; }
    }
}