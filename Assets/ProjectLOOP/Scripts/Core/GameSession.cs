using UnityEngine;

namespace ProjectLOOP
{
    /// <summary>
    /// Survives scene loads. Owns wallets, save port, and online stub.
    /// </summary>
    public sealed class GameSession : MonoBehaviour
    {
        public static GameSession Instance { get; private set; }

        public TownWallet TownWallet { get; private set; }
        public RunInventory RunInventory { get; private set; }
        public ISaveService SaveService { get; private set; }
        public IOnlineServices OnlineServices { get; private set; }

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
            SaveService = new LocalPlayerPrefsSaveService();
            OnlineServices = new NullOnlineServices();
            TownWallet.SetGold(SaveService.LoadTownGold());
        }

        public void PersistTownWallet()
        {
            SaveService.SaveTownGold(TownWallet.Gold);
            _ = OnlineServices.TryCloudSaveMetaAsync(TownWallet.Gold);
        }

        public void DepositRunLootToTown()
        {
            if (RunInventory.Loot <= 0)
            {
                PersistTownWallet();
                return;
            }

            TownWallet.Add(RunInventory.Loot);
            RunInventory.Clear();
            PersistTownWallet();
            OnlineServices.UnlockAchievement("first_deposit");
        }

        public void HandlePlayerDeath()
        {
            RunInventory.Clear();
            PersistTownWallet();
        }
    }
}
