using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardBackgroundSetter))]
public class CardBackgroundSetterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var script = (CardBackgroundSetter) target;
        
        DrawDefaultInspector();
        DrawButtonPlaceCards(script);
    }

    private static void DrawButtonPlaceCards(CardBackgroundSetter script)
    {
        if (GUILayout.Button($"{nameof(script.PlaceCards)}"))
        {
            script.PlaceCards();
        }
    }
}