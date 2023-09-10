using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CardColorData", menuName = "ScriptableObjects/CardColorData")]
public class CardColorData : ScriptableObject
{
    [SerializeField] private Color _cardWhite;
    [SerializeField] private Color _cardBlack;
    [SerializeField] private Color _cardRed;

    public Color this[ColorType colorType] => colorType switch
    {
        ColorType.None => Color.white,
        ColorType.CardWhite => _cardWhite,
        ColorType.CardBlack => _cardBlack,
        ColorType.CardRed => _cardRed,
        _ => throw new ArgumentOutOfRangeException(nameof(colorType), colorType, null)
    };

    public enum ColorType
    {
        None,
        CardWhite,
        CardBlack,
        CardRed
    }
}