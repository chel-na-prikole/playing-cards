using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    [SerializeField, Header("Settings")] private CardColorData.ColorType _colorType;
    [SerializeField, Header("References")] private CardColorData _cardColorData;
    [SerializeField] private SpriteRenderer _spriteRenderer;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() => SetColor();
    
    private void OnValidate() => SetColor();

    private void SetColor()
    {
        if (_cardColorData != null && _spriteRenderer != null)
        {
            _spriteRenderer.color = _cardColorData[_colorType];
        }
    }
#endif
}