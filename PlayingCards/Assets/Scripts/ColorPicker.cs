using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    [SerializeField, Header("Settings")] private CardColorData.ColorType _colorType;
    [SerializeField, Header("References")] private CardColorData _cardColorData;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        if (_spriteRenderer.color != _cardColorData[_colorType])
        {
            SetColor();
        }
    }
    
    private void SetColor()
    {
        if (_cardColorData != null && _spriteRenderer != null)
        {
            _spriteRenderer.color = _cardColorData[_colorType];
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() => SetColor();
    private void OnValidate() => SetColor();
#endif
}