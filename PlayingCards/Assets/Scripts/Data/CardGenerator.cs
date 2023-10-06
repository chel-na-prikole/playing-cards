using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CardGenerator", menuName = "ScriptableObjects/CardGenerator")]
    public class CardGenerator : ScriptableObject
    {
        [field: SerializeField] public DataStorage DataStorage { get; private set; }
        [field: SerializeField] public Object TargetFolder { get; private set; }
    }
}