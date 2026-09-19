using System;
using System.Collections.Generic;
using MarineSlayer.Core;
using MarineSlayer.Save;
using UnityEngine;

namespace MarineSlayer.Lore
{
    [Serializable]
    public sealed class LoreEntry
    {
        public string id;
        public string category;
        public string title;
        public string body;
        public string audioReference;

        public LoreEntry(string entryId, string entryCategory, string entryTitle, string entryBody, string entryAudioReference)
        {
            id = entryId;
            category = entryCategory;
            title = entryTitle;
            body = entryBody;
            audioReference = entryAudioReference;
        }
    }

    public sealed class LoreService : MonoBehaviour
    {
        private int collectionCount;
        private int readCount;

        public LoreEntry CurrentEntry { get; private set; }
        public bool IsOpen { get; private set; }
        public int CollectionCount { get { return collectionCount; } }
        public int ReadCount { get { return readCount; } }

        public bool Open(LoreEntry entry)
        {
            if (entry == null || string.IsNullOrEmpty(entry.id)) return false;
            bool newlyCollected = Collect(entry.id);
            MarkRead(entry.id);
            CurrentEntry = entry;
            IsOpen = true;
            GameRoot.Instance.State.SetState(GameState.Lore);
            GameEvents.RaiseLoreOpened(entry.id);
            return newlyCollected;
        }

        public bool Close()
        {
            if (!IsOpen) return false;
            IsOpen = false;
            if (GameRoot.Instance.State.CurrentState == GameState.Lore)
                GameRoot.Instance.State.SetState(GameState.Playing);
            GameEvents.RaiseLoreClosed();
            return true;
        }

        public bool Collect(string loreId)
        {
            if (string.IsNullOrEmpty(loreId)) return false;
            List<string> collected = CollectedIds();
            if (collected.Contains(loreId)) return false;
            collected.Add(loreId);
            collectionCount++;
            GameRoot.Instance.Saves.Write();
            return true;
        }

        public bool MarkRead(string loreId)
        {
            if (string.IsNullOrEmpty(loreId)) return false;
            List<string> read = ReadIds();
            if (read.Contains(loreId)) return false;
            read.Add(loreId);
            readCount++;
            GameRoot.Instance.Saves.Write();
            return true;
        }

        public void ResetSession()
        {
            CurrentEntry = null;
            IsOpen = false;
            collectionCount = 0;
            readCount = 0;
        }

        private static List<string> CollectedIds()
        {
            CampaignSaveData data = GameRoot.Instance.Saves.Current;
            if (data.collectedLoreIds == null) data.collectedLoreIds = new List<string>();
            return data.collectedLoreIds;
        }

        private static List<string> ReadIds()
        {
            CampaignSaveData data = GameRoot.Instance.Saves.Current;
            if (data.readLoreIds == null) data.readLoreIds = new List<string>();
            return data.readLoreIds;
        }
    }
}
