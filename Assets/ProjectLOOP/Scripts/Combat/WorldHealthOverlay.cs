using UnityEngine;

namespace ProjectLOOP
{
    /// <summary>
    /// Screen-space HP readout above a Health owner (enemies).
    /// </summary>
    [RequireComponent(typeof(Health))]
    public sealed class WorldHealthOverlay : MonoBehaviour
    {
        [SerializeField] Vector3 worldOffset = new Vector3(0f, 2.2f, 0f);
        [SerializeField] float barWidth = 56f;
        [SerializeField] float barHeight = 8f;

        Health _health;

        void Awake()
        {
            _health = GetComponent<Health>();
        }

        void OnGUI()
        {
            if (_health == null || _health.IsDead)
            {
                return;
            }

            var cam = Camera.main;
            if (cam == null)
            {
                return;
            }

            var screen = cam.WorldToScreenPoint(transform.position + worldOffset);
            if (screen.z <= 0f)
            {
                return;
            }

            var x = screen.x - barWidth * 0.5f;
            var y = Screen.height - screen.y - barHeight * 0.5f;
            var ratio = _health.MaxHealth <= 0
                ? 0f
                : Mathf.Clamp01((float)_health.CurrentHealth / _health.MaxHealth);

            GUI.color = new Color(0f, 0f, 0f, 0.65f);
            GUI.DrawTexture(new Rect(x - 1f, y - 1f, barWidth + 2f, barHeight + 2f), Texture2D.whiteTexture);

            GUI.color = new Color(0.25f, 0.25f, 0.25f, 0.9f);
            GUI.DrawTexture(new Rect(x, y, barWidth, barHeight), Texture2D.whiteTexture);

            GUI.color = Color.Lerp(new Color(0.85f, 0.2f, 0.15f), new Color(0.25f, 0.85f, 0.3f), ratio);
            GUI.DrawTexture(new Rect(x, y, barWidth * ratio, barHeight), Texture2D.whiteTexture);

            GUI.color = Color.white;
            var label = $"{_health.CurrentHealth}/{_health.MaxHealth}";
            var style = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 12,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.white }
            };
            GUI.Label(new Rect(x - 10f, y - 18f, barWidth + 20f, 18f), label, style);
        }
    }
}
