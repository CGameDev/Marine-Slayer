using UnityEngine;

namespace MarineSlayer.Platform
{
    public sealed class PlatformService : MonoBehaviour
    {
        public bool IsXbox360 { get { return Application.platform == RuntimePlatform.XBOX360; } }
        public string RuntimeName { get { return Application.platform.ToString(); } }
    }
}
