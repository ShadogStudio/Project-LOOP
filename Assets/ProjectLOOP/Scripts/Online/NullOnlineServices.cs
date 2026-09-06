using System.Threading.Tasks;
using UnityEngine;

namespace ProjectLOOP
{
    public sealed class NullOnlineServices : IOnlineServices
    {
        public bool IsAvailable => false;

        public Task<bool> TryLoginAsync()
        {
            return Task.FromResult(false);
        }

        public Task<bool> TryCloudSaveMetaAsync(int townGold)
        {
            Debug.Log($"[Online:Null] Skip cloud save (gold={townGold})");
            return Task.FromResult(false);
        }

        public Task<int?> TryCloudLoadMetaAsync()
        {
            return Task.FromResult<int?>(null);
        }

        public void UnlockAchievement(string achievementId)
        {
            Debug.Log($"[Online:Null] Skip achievement '{achievementId}'");
        }
    }
}
