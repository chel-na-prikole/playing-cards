using UnityEngine;

public static class Extensions
{
    public static Vector3 ToVector3(this Vector2 value) => new(value.x, value.y, 0f);
}