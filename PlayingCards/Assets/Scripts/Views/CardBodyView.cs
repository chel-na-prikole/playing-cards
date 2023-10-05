using UnityEngine;

namespace Views
{
    public class CardBodyView : MonoBehaviour
    {
        [SerializeField] private SpriteView _body;
        [SerializeField] private SpriteView[] _borders;

        public void UpdateView(Color bodyColor, Color borderColor)
        {
            _body.UpdateView(bodyColor);

            foreach (var border in _borders)
            {
                border.UpdateView(borderColor);
            }
        }
    }
}