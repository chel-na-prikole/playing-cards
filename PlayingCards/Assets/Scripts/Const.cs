using Enums;
using UnityEngine;

public static class Const
{
    public static readonly string DefaultPath = $"{MainFolderName}/{FolderName}";
    public static readonly Vector3 Offset = new(0f, 0f, -0.01f);
    public static readonly CardValue[] HighRankValues = { CardValue.Ace };

    public const string FolderName = "Cards";
    public const string MainFolderName = "Assets";
}