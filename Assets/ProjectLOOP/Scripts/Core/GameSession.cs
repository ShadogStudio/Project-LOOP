using UnityEngine;

namespace ProjectLOOP
{
    /// <summary>
    /// Survives scene loads. Owns town wallet and run inventory for the stub loop.
    /// </summary>
    public sealed class GameSession : MonoBehaviour
    {
        public const string TownSceneName = "Town";
        public const string DungeonSceneName = "Dungeon";

        public static GameSession Instance { get; private set; }

        public TownWallet TownWallet { get; private set; }
        public RunInventory RunInventory { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Bootstrap()
        {
            if (Instance != null)
            {
                return;
            }

            var go = new GameObject(nameof(GameSession));
            DontDestroyOnLoad(go);
            go.AddComponent<GameSession>();
        }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            TownWallet = new TownWallet();
            RunInventory = new RunInventory();
            TownWallet.Load();
        }

        public void DepositRunLootToTown()
        {
            if (RunInventory.Loot <= 0)
            {
                return;
            }

            TownWallet.Add(RunInventory.Loot);
            RunInventory.Clear();
            TownWallet.Save();
        }

        public void HandlePlayerDeath()
        {
            RunInventory.Clear();
            TownWallet.Save();
        }
    }
}
