using UnityEngine;

namespace ProjectLOOP
{
    /// <summary>
    /// Persistent town currency. Survives death.
    /// </summary>
    public sealed class TownWallet
    {
        const string PrefsKey = "ProjectLOOP.TownGold";

        public int Gold { get; private set; }

        public void Add(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            Gold += amount;
        }

        public bool TrySpend(int amount)
        {
            if (amount <= 0 || Gold < amount)
            {
                return false;
            }

            Gold -= amount;
            return true;
        }

        public void Load()
        {
            Gold = PlayerPrefs.GetInt(PrefsKey, 0);
        }

        public void Save()
        {
            PlayerPrefs.SetInt(PrefsKey, Gold);
            PlayerPrefs.Save();
        }
    }
}
