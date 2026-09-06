using UnityEngine;

namespace ProjectLOOP
{
    public sealed class LocalPlayerPrefsSaveService : ISaveService
    {
        const string TownGoldKey = "ProjectLOOP.TownGold";

        public int LoadTownGold()
        {
            return PlayerPrefs.GetInt(TownGoldKey, 0);
        }

        public void SaveTownGold(int gold)
        {
            PlayerPrefs.SetInt(TownGoldKey, gold);
            PlayerPrefs.Save();
        }
    }
}
