using UnityEngine;

namespace Views
{
    public class SpriteView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public void UpdateView(Sprite sprite, Color color)
        {
            _spriteRenderer.sprite = sprite;
            UpdateView(color);
        }

        public void UpdateView(Color color)
        {
            _spriteRenderer.color = color;
        }
    }
}