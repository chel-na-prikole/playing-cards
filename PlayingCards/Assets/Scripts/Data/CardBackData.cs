using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CardBackData", menuName = "ScriptableObjects/CardBackData")]
    public class CardBackData : ScriptableObject
    {
        public const string CardBackName = "CardBack";
        
        [field: SerializeField] public Color CardBackColor { get; private set; }
        [field: SerializeField] public Sprite CardBackSprite { get; private set; }
    }
}