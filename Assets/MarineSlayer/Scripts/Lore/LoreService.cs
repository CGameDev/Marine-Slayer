using MarineSlayer.Core;

namespace MarineSlayer.Lore
{
    public sealed class LoreService : UnityEngine.MonoBehaviour
    {
        public bool Collect(string loreId)
        {
            if (string.IsNullOrEmpty(loreId)) return false;
            if (GameRoot.Instance.Saves.Current.collectedLoreIds.Contains(loreId)) return false;
            GameRoot.Instance.Saves.Current.collectedLoreIds.Add(loreId);
            GameRoot.Instance.Saves.Write();
            return true;
        }
    }
}
