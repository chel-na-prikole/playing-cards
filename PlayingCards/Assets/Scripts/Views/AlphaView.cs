using UnityEngine;

namespace Views
{
    public class AlphaView : MonoBehaviour
    {
        private const float MinAlpha = 0.2f;
        private const float MaxAlpha = 1f;
        private const float MinSin = -1f;
        private const float MaxSin = 1f;
        
        private float _offset;
        private Transform _transform;
        private SpriteRenderer[] _spriteRenderers;

        private void Awake()
        {
            _transform = transform;
            var position = _transform.position;
            _offset = Mathf.Abs(position.x) + Mathf.Abs(position.y);
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            var sin = Mathf.Sin(Time.time + _offset);
            var normalizedScale = Extensions.GetNormalized(sin, MinSin, MaxSin);
            var alpha = Extensions.GetDenormalized(normalizedScale, MinAlpha, MaxAlpha);
            UpdateAlpha(alpha);
        }

        private void UpdateAlpha(float alpha)
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                var color = spriteRenderer.color;
                color.a = alpha;
                spriteRenderer.color = color;
            }
        }
    }
}