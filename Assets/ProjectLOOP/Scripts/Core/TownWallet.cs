namespace ProjectLOOP
{
    /// <summary>
    /// Persistent town currency. Survives death.
    /// </summary>
    public sealed class TownWallet
    {
        public int Gold { get; private set; }

        public void SetGold(int value)
        {
            Gold = value < 0 ? 0 : value;
        }

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
    }
}
