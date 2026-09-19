using UnityEngine;

namespace MarineSlayer.Input
{
    public sealed class InputService : MonoBehaviour
    {
        public Vector2 Move { get { return new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical")); } }
        public Vector2 Aim { get { return new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal2"), UnityEngine.Input.GetAxisRaw("Vertical2")); } }
        public bool SubmitPressed { get { return UnityEngine.Input.GetKeyDown(KeyCode.Return) || UnityEngine.Input.GetKeyDown(KeyCode.JoystickButton0); } }
        public bool PausePressed { get { return UnityEngine.Input.GetKeyDown(KeyCode.Escape) || UnityEngine.Input.GetKeyDown(KeyCode.JoystickButton7); } }
        public bool RestartPressed { get { return UnityEngine.Input.GetKeyDown(KeyCode.R); } }
        public bool DebugDeathPressed { get { return UnityEngine.Input.GetKeyDown(KeyCode.K); } }
        public bool MenuPressed { get { return UnityEngine.Input.GetKeyDown(KeyCode.M); } }
    }
}
