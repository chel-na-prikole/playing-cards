using UnityEngine;

namespace Views
{
    public class ValueView : MonoBehaviour
    {
        [field: SerializeField] public SpriteView SpriteView { get; private set; }

        public void SetPosition(Vector2 position)
        {
            this.UpdatePosition(position);
        }
    }
}