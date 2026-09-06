using UnityEngine;

namespace ProjectLOOP
{
    public sealed class LocalPlayerPrefsSaveService : ISaveService
    {
        const string TownGoldKey = "ProjectLOOP.TownGold";
        const string BonusMaxHpKey = "ProjectLOOP.BonusMaxHp";
        const string BonusMeleeDamageKey = "ProjectLOOP.BonusMeleeDamage";

        public int LoadTownGold()
        {
            return PlayerPrefs.GetInt(TownGoldKey, 0);
        }

        public void SaveTownGold(int gold)
        {
            PlayerPrefs.SetInt(TownGoldKey, Mathf.Max(0, gold));
            PlayerPrefs.Save();
        }

        public int LoadBonusMaxHp() => PlayerPrefs.GetInt(BonusMaxHpKey, 0);

        public void SaveBonusMaxHp(int value)
        {
            PlayerPrefs.SetInt(BonusMaxHpKey, Mathf.Max(0, value));
            PlayerPrefs.Save();
        }

        public int LoadBonusMeleeDamage() => PlayerPrefs.GetInt(BonusMeleeDamageKey, 0);

        public void SaveBonusMeleeDamage(int value)
        {
            PlayerPrefs.SetInt(BonusMeleeDamageKey, Mathf.Max(0, value));
            PlayerPrefs.Save();
        }

        public void SaveMeta(int townGold, int bonusMaxHp, int bonusMeleeDamage)
        {
            PlayerPrefs.SetInt(TownGoldKey, Mathf.Max(0, townGold));
            PlayerPrefs.SetInt(BonusMaxHpKey, Mathf.Max(0, bonusMaxHp));
            PlayerPrefs.SetInt(BonusMeleeDamageKey, Mathf.Max(0, bonusMeleeDamage));
            PlayerPrefs.Save();
        }
    }
}
