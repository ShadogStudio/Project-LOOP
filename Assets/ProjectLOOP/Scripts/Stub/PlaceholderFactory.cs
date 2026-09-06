using UnityEngine;

namespace ProjectLOOP
{
    /// <summary>
    /// Builds primitive placeholders at runtime so the public repo stays runnable without third-party art.
    /// </summary>
    public static class PlaceholderFactory
    {
        static Material _ground;
        static Material _player;
        static Material _portal;
        static Material _loot;
        static Material _hazard;

        public static GameObject CreateGround(string name, Vector3 position, Vector3 scale, Color color)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.position = position;
            go.transform.localScale = scale;
            ApplyColor(go, color, ref _ground);
            return go;
        }

        public static GameObject CreatePlayer(Vector3 position)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            go.name = "Player";
            go.tag = "Player";
            go.transform.position = position;

            Object.Destroy(go.GetComponent<CapsuleCollider>());
            var controller = go.AddComponent<CharacterController>();
            controller.height = 2f;
            controller.radius = 0.4f;
            controller.center = Vector3.up;

            go.AddComponent<TopDownPlayerMotor>();
            ApplyColor(go, new Color(0.25f, 0.7f, 1f), ref _player);
            return go;
        }

        public static GameObject CreatePortal(string name, Vector3 position, string targetScene, bool depositLoot, Color color)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.position = position;
            go.transform.localScale = new Vector3(2.2f, 0.3f, 2.2f);

            var col = go.GetComponent<BoxCollider>();
            col.isTrigger = true;

            var portal = go.AddComponent<ScenePortal>();
            portal.Configure(targetScene, depositLoot);
            ApplyColor(go, color, ref _portal);
            return go;
        }

        public static GameObject CreateLoot(Vector3 position, int amount)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.name = "RunLoot";
            go.transform.position = position;
            go.transform.localScale = Vector3.one * 0.7f;

            var col = go.GetComponent<SphereCollider>();
            col.isTrigger = true;

            var pickup = go.AddComponent<RunLootPickup>();
            pickup.Configure(amount);
            ApplyColor(go, new Color(1f, 0.85f, 0.2f), ref _loot);
            return go;
        }

        public static GameObject CreateDeathZone(Vector3 position)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = "DeathZone";
            go.transform.position = position;
            go.transform.localScale = new Vector3(2.5f, 0.3f, 2.5f);

            var col = go.GetComponent<BoxCollider>();
            col.isTrigger = true;
            go.AddComponent<PlayerDeathTrigger>();
            ApplyColor(go, new Color(0.85f, 0.2f, 0.2f), ref _hazard);
            return go;
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

        static void ApplyColor(GameObject go, Color color, ref Material cache)
        {
            if (cache == null)
            {
                var shader = Shader.Find("Universal Render Pipeline/Lit");
                if (shader == null)
                {
                    shader = Shader.Find("Sprites/Default");
                }

                cache = new Material(shader);
            }

            var instance = new Material(cache);
            if (instance.HasProperty("_BaseColor"))
            {
                instance.SetColor("_BaseColor", color);
            }
            else
            {
                instance.color = color;
            }

            var renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial = instance;
            }
        }
    }
}
