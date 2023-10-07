using UnityEngine;

public static class Extensions
{
    public static Vector3 ToVector3(this Vector2 value) => new(value.x, value.y, 0f);

    public static void UpdatePosition(this MonoBehaviour value, Vector2 position)
    {
        var valueTransform = value.transform;
        valueTransform.position = new Vector3(position.x, position.y, valueTransform.position.z);
    }

    public static float GetNormalized(float value, float min, float max) => (value - min) / (max - min);
    public static float GetDenormalized(float normalizedValue, float min, float max) => normalizedValue * (max - min) + min;
}