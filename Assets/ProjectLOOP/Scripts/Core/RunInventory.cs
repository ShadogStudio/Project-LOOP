namespace ProjectLOOP
{
    /// <summary>
    /// Loot carried during a dungeon run. Cleared on death.
    /// </summary>
    public sealed class RunInventory
    {
        public int Loot { get; private set; }

        public void Add(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            Loot += amount;
        }

        public void Clear()
        {
            Loot = 0;
        }
    }
}
