using UnityEngine;

namespace ProjectLOOP
{
    public static class PlaceholderVisuals
    {
        public static void ApplyColor(GameObject go, Color color)
        {
            var shader = Shader.Find("Universal Render Pipeline/Lit");
            if (shader == null)
            {
                shader = Shader.Find("Sprites/Default");
            }

            var material = new Material(shader);
            if (material.HasProperty("_BaseColor"))
            {
                material.SetColor("_BaseColor", color);
            }
            else
            {
                material.color = color;
            }

            var renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial = material;
            }
        }

        /// <param name="feetPosition">World position of the CharacterController feet (bottom).</param>
        public static GameObject CreatePlayer(Vector3 feetPosition)
        {
            var go = new GameObject("Player");
            go.tag = "Player";
            go.transform.position = feetPosition;

            // Default capsule mesh is centered on its transform; lift it to match CC center.
            var visual = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            visual.name = "Visual";
            visual.transform.SetParent(go.transform, false);
            visual.transform.localPosition = new Vector3(0f, 1f, 0f);
            Object.Destroy(visual.GetComponent<Collider>());
            ApplyColor(visual, new Color(0.25f, 0.7f, 1f));

            var controller = go.AddComponent<CharacterController>();
            controller.height = 2f;
            controller.radius = 0.4f;
            controller.center = new Vector3(0f, 1f, 0f);
            controller.skinWidth = 0.08f;
            controller.stepOffset = 0.3f;
            controller.minMoveDistance = 0f;

            go.AddComponent<TopDownPlayerMotor>();
            return go;
        }

        public static float FloorSurfaceY(float floorCenterY = 0f, float floorHeight = 0.2f)
        {
            return floorCenterY + floorHeight * 0.5f;
        }

        public static void SetupTopDownCamera(Transform target)
        {
            var cam = Camera.main;
            if (cam == null)
            {
                var camGo = new GameObject("Main Camera");
                cam = camGo.AddComponent<Camera>();
                cam.tag = "MainCamera";
                camGo.AddComponent<AudioListener>();
            }

            var follow = cam.GetComponent<TopDownCameraFollow>();
            if (follow == null)
            {
                follow = cam.gameObject.AddComponent<TopDownCameraFollow>();
            }

            follow.SetTarget(target);
            cam.transform.position = target.position + new Vector3(0f, 14f, -10f);
            cam.transform.rotation = Quaternion.Euler(50f, 0f, 0f);
        }
    }
}
