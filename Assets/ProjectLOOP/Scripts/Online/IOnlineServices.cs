using System.Threading.Tasks;

namespace ProjectLOOP
{
    /// <summary>
    /// Common online surface. EOS binds here later; v1 uses NullOnlineServices.
    /// </summary>
    public interface IOnlineServices
    {
        bool IsAvailable { get; }
        Task<bool> TryLoginAsync();
        Task<bool> TryCloudSaveMetaAsync(int townGold);
        Task<int?> TryCloudLoadMetaAsync();
        void UnlockAchievement(string achievementId);
    }
}
