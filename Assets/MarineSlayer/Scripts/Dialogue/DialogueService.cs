using UnityEngine;

namespace MarineSlayer.Dialogue
{
    public sealed class DialogueService : MonoBehaviour
    {
        public string Speaker { get; private set; }
        public string Subtitle { get; private set; }

        public void Show(string speaker, string subtitle)
        {
            Speaker = speaker;
            Subtitle = subtitle;
        }

        public void Clear()
        {
            Speaker = string.Empty;
            Subtitle = string.Empty;
        }
    }
}
