using UnityEngine;

namespace Views
{
    public class QuitView : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}