namespace ProjectLOOP
{
    public interface ISaveService
    {
        int LoadTownGold();
        void SaveTownGold(int gold);
        int LoadBonusMaxHp();
        void SaveBonusMaxHp(int value);
        int LoadBonusMeleeDamage();
        void SaveBonusMeleeDamage(int value);

        /// <summary>Atomically write all meta fields, then flush once.</summary>
        void SaveMeta(int townGold, int bonusMaxHp, int bonusMeleeDamage);
    }
}
