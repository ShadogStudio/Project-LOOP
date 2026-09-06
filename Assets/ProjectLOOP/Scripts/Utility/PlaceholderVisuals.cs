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
            var go = CreateCapsuleActor(
                "Player",
                "Player",
                feetPosition,
                new Color(0.25f, 0.7f, 1f));

            var session = GameSession.EnsureExists();
            session.GetEffectiveCombatStats(out var maxHp, out var meleeDamage);

            var health = go.AddComponent<Health>();
            health.Configure(maxHp);

            var melee = go.AddComponent<MeleeAttack>();
            melee.Configure(1.7f, meleeDamage, 0.5f, "Enemy");

            go.AddComponent<TopDownPlayerMotor>();
            go.AddComponent<PlayerMeleeInput>();
            go.AddComponent<PlayerDeathHandler>();
            go.AddComponent<FallYKill>();
            return go;
        }

        public static GameObject CreateEnemy(Vector3 feetPosition)
        {
            var go = CreateCapsuleActor(
                "Enemy",
                "Enemy",
                feetPosition,
                new Color(0.85f, 0.25f, 0.25f));

            var health = go.AddComponent<Health>();
            health.Configure(30);
            go.AddComponent<WorldHealthOverlay>();

            var melee = go.AddComponent<MeleeAttack>();
            melee.Configure(1.55f, 8, 0.85f, "Player");

            go.AddComponent<SimpleChaseEnemy>();
            go.AddComponent<FallYKill>();
            return go;
        }

        public static GameObject CreateOutOfBoundsKillVolume(Transform parent, Vector3 center, Vector3 size)
        {
            var go = new GameObject("OutOfBoundsKillZone");
            go.transform.SetParent(parent, false);
            go.transform.position = center;
            var box = go.AddComponent<BoxCollider>();
            box.isTrigger = true;
            box.size = size;
            go.AddComponent<OutOfBoundsKillZone>();
            return go;
        }

        public static void AddWorldLabel(Transform parent, string text, float heightOffset = 1.6f)
        {
            var labelGo = new GameObject("Label");
            labelGo.transform.SetParent(parent, false);
            var sy = Mathf.Approximately(parent.localScale.y, 0f) ? 1f : parent.localScale.y;
            labelGo.transform.localPosition = new Vector3(0f, heightOffset / sy, 0f);
            labelGo.transform.localScale = Vector3.one;
            labelGo.AddComponent<WorldLabelOverlay>().SetText(text);
        }

        static GameObject CreateCapsuleActor(string name, string tag, Vector3 feetPosition, Color color)
        {
            var go = new GameObject(name);
            go.tag = tag;
            go.transform.position = feetPosition;

            var visual = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            visual.name = "Visual";
            visual.transform.SetParent(go.transform, false);
            visual.transform.localPosition = new Vector3(0f, 1f, 0f);
            Object.Destroy(visual.GetComponent<Collider>());
            ApplyColor(visual, color);

            var controller = go.AddComponent<CharacterController>();
            controller.height = 2f;
            controller.radius = 0.4f;
            controller.center = new Vector3(0f, 1f, 0f);
            controller.skinWidth = 0.08f;
            controller.stepOffset = 0.3f;
            controller.minMoveDistance = 0f;
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
