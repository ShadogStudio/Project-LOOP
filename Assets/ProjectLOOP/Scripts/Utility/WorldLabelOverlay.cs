using UnityEngine;

namespace ProjectLOOP
{
    /// <summary>
    /// Draws a label in screen space above a world position (supports Korean via IMGUI).
    /// </summary>
    public sealed class WorldLabelOverlay : MonoBehaviour
    {
        [SerializeField] string label = string.Empty;

        public void SetText(string text)
        {
            label = text ?? string.Empty;
        }

        void OnGUI()
        {
            if (string.IsNullOrEmpty(label))
            {
                return;
            }

            var cam = Camera.main;
            if (cam == null)
            {
                return;
            }

            var screen = cam.WorldToScreenPoint(transform.position);
            if (screen.z <= 0f)
            {
                return;
            }

            var style = new GUIStyle(GUI.skin.box)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.white }
            };

            var content = new GUIContent(label);
            var size = style.CalcSize(content);
            var width = size.x + 20f;
            var height = size.y + 10f;
            var x = screen.x - width * 0.5f;
            var y = Screen.height - screen.y - height * 0.5f;
            GUI.Box(new Rect(x, y, width, height), content, style);
        }
    }
}
