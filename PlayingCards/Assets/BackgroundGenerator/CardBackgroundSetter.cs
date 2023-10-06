using UnityEngine;
using Random = UnityEngine.Random;

public class CardBackgroundSetter : MonoBehaviour
{
    private const string CardsParentName = "Cards";
    private const float CoefOffsetZ = 0.5f;

    [SerializeField] private Mode _mode = Mode.Default;
    [SerializeField] private Vector2Int _size = new(20, 10);
    [SerializeField] private float _offsetX = 4.25f;
    [SerializeField] private float _offsetY = 5.25f;
    [SerializeField] private bool _needOffsetZ;
    
    [SerializeField] private GameObject[] _cardPrefabs;
    
    public void PlaceCards()
    {
        if (_cardPrefabs == null || _cardPrefabs.Length == 0)
        {
            Debug.LogError($"<color=red>[{nameof(CardBackgroundSetter)}]</color> Check {nameof(_cardPrefabs)} field. It is null or no elements");
            return;
        }

        var cardParent = GameObject.Find(CardsParentName);

        if (cardParent != null)
        {
            DestroyImmediate(cardParent);
        }
        
        var startPosition = GetStartPosition();
        cardParent = new GameObject(CardsParentName);
        var count = 0;

        for (var x = 0; x < _size.x; x++)
        for (var y = 0; y < _size.y; y++)
        {
            var randomCard = _cardPrefabs[Random.Range(0, _cardPrefabs.Length)];
            var rotation = _mode == Mode.Default ? Quaternion.identity : Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f));
            var offsetZ = _needOffsetZ ? count * CoefOffsetZ : 0f;
            
            Instantiate(randomCard, new Vector3(startPosition.x + x * _offsetX, startPosition.y + y * _offsetY, offsetZ), rotation, cardParent.transform);
            count++;
        }
    }

    private Vector2 GetStartPosition() => new(-(_size.x / 2) * _offsetX, -(_size.y / 2) * _offsetY);
    
    [System.Serializable]
    public enum Mode
    {
        Default,
        RandomRotation
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (name != nameof(CardBackgroundSetter))
        {
            name = nameof(CardBackgroundSetter);
        }
    }
#endif
}