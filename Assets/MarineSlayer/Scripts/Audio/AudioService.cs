using UnityEngine;

namespace MarineSlayer.Audio
{
    public sealed class AudioService : MonoBehaviour
    {
        [Range(0f, 1f)] public float masterVolume = 1f;
        [Range(0f, 1f)] public float musicVolume = 0.8f;
        [Range(0f, 1f)] public float effectsVolume = 1f;
        [Range(0f, 1f)] public float dialogueVolume = 1f;
    }
}
