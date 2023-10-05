using UnityEngine;
using Views;

namespace Data
{
    [CreateAssetMenu(fileName = "CardComponentsData", menuName = "ScriptableObjects/CardComponentsData")]
    public class CardComponentsData : ScriptableObject
    {
        [field: SerializeField] public CardBodyView CardBody { get; private set; }
        [field: SerializeField] public SpriteView SpriteView { get; private set; }
    }
}