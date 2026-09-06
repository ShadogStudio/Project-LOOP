using UnityEngine;

namespace ProjectLOOP
{
    /// <summary>
    /// Survives scene loads. Owns wallets, meta upgrades, save port, and online stub.
    /// </summary>
    public sealed class GameSession : MonoBehaviour
    {
        public const int BaseMaxHp = 50;
        public const int BaseMeleeDamage = 14;
        public const int VitalityCost = 25;
        public const int VitalityBonus = 10;
        public const int PowerCost = 35;
        public const int PowerBonus = 3;
        public const int MaxVitalityPurchases = 5;
        public const int MaxPowerPurchases = 5;

        public static GameSession Instance { get; private set; }

        public TownWallet TownWallet { get; private set; }
        public RunInventory RunInventory { get; private set; }
        public ISaveService SaveService { get; private set; }
        public IOnlineServices OnlineServices { get; private set; }

        public int BonusMaxHp { get; private set; }
        public int BonusMeleeDamage { get; private set; }

        public int EffectiveMaxHp => BaseMaxHp + BonusMaxHp;
        public int EffectiveMeleeDamage => BaseMeleeDamage + BonusMeleeDamage;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Bootstrap()
        {
            EnsureExists();
        }

        public static GameSession EnsureExists()
        {
            if (Instance != null)
            {
                return Instance;
            }

            var existing = Object.FindFirstObjectByType<GameSession>();
            if (existing != null)
            {
                Instance = existing;
                return Instance;
            }

            var go = new GameObject(nameof(GameSession));
            return go.AddComponent<GameSession>();
        }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            TownWallet = new TownWallet();
            RunInventory = new RunInventory();
            SaveService = new LocalPlayerPrefsSaveService();
            OnlineServices = new NullOnlineServices();
            LoadMetaFromSave();
        }

        void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public void LoadMetaFromSave()
        {
            if (SaveService == null)
            {
                SaveService = new LocalPlayerPrefsSaveService();
            }

            TownWallet ??= new TownWallet();
            RunInventory ??= new RunInventory();

            TownWallet.SetGold(SaveService.LoadTownGold());
            BonusMaxHp = SaveService.LoadBonusMaxHp();
            BonusMeleeDamage = SaveService.LoadBonusMeleeDamage();
        }

        public void PersistMeta()
        {
            if (SaveService == null)
            {
                SaveService = new LocalPlayerPrefsSaveService();
            }

            SaveService.SaveMeta(TownWallet.Gold, BonusMaxHp, BonusMeleeDamage);
            _ = OnlineServices.TryCloudSaveMetaAsync(TownWallet.Gold);
        }

        public void PersistTownWallet()
        {
            PersistMeta();
        }

        public void GetEffectiveCombatStats(out int maxHp, out int meleeDamage)
        {
            maxHp = EffectiveMaxHp;
            meleeDamage = EffectiveMeleeDamage;
        }

        public bool TryBuyVitality(out string message)
        {
            var purchases = BonusMaxHp / VitalityBonus;
            if (purchases >= MaxVitalityPurchases)
            {
                message = "Vitality already maxed.";
                return false;
            }

            if (!TownWallet.TrySpend(VitalityCost))
            {
                message = $"Need {VitalityCost} town gold.";
                return false;
            }

            BonusMaxHp += VitalityBonus;
            PersistMeta();
            message = $"+{VitalityBonus} Max HP purchased.";
            return true;
        }

        public bool TryBuyPower(out string message)
        {
            var purchases = BonusMeleeDamage / PowerBonus;
            if (purchases >= MaxPowerPurchases)
            {
                message = "Power already maxed.";
                return false;
            }

            if (!TownWallet.TrySpend(PowerCost))
            {
                message = $"Need {PowerCost} town gold.";
                return false;
            }

            BonusMeleeDamage += PowerBonus;
            PersistMeta();
            message = $"+{PowerBonus} Melee Damage purchased.";
            return true;
        }

        public void DepositRunLootToTown()
        {
            if (RunInventory.Loot <= 0)
            {
                PersistMeta();
                return;
            }

            TownWallet.Add(RunInventory.Loot);
            RunInventory.Clear();
            PersistMeta();
            OnlineServices.UnlockAchievement("first_deposit");
        }

        public void HandlePlayerDeath()
        {
            RunInventory.Clear();
            PersistMeta();
        }
    }
}
