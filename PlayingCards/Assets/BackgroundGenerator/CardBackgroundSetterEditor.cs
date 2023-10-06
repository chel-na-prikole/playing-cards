using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BackgroundGenerator
{
    [CustomEditor(typeof(CardBackgroundSetter))]
    public class CardBackgroundSetterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var script = (CardBackgroundSetter) target;
        
            DrawDefaultInspector();
            DrawButtonPlaceCards(script);
            DrawButtonMakeSceneDirty(script);
        }

        private static void DrawButtonPlaceCards(CardBackgroundSetter script)
        {
            if (GUILayout.Button($"{nameof(script.PlaceCards)}"))
            {
                script.PlaceCards();
            }
        }
    
        private static void DrawButtonMakeSceneDirty(CardBackgroundSetter script)
        {
            if (GUILayout.Button("Make scene dirty"))
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }
        }
    }
}