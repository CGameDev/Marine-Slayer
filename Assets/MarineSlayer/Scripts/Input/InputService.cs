using UnityEngine;

namespace MarineSlayer.Input
{
    public sealed class InputService : MonoBehaviour
    {
        private bool testOverride;
        private Vector2 testMove;
        private Vector2 testAim;
        private bool testFire;
        private bool triggerPressed;
        private bool previousTriggerHeld;

        public Vector2 Move { get { return testOverride ? testMove : new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical")); } }
        public Vector2 Aim { get { return testOverride ? testAim : new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal2"), UnityEngine.Input.GetAxisRaw("Vertical2")); } }
        public bool SubmitPressed { get { return UnityEngine.Input.GetKeyDown(KeyCode.Return) || UnityEngine.Input.GetKeyDown(KeyCode.JoystickButton0); } }
        public bool InteractPressed { get { return SubmitPressed; } }
        public bool PausePressed { get { return UnityEngine.Input.GetKeyDown(KeyCode.Escape) || UnityEngine.Input.GetKeyDown(KeyCode.JoystickButton7); } }
        public bool RestartPressed { get { return UnityEngine.Input.GetKeyDown(KeyCode.R); } }
        public bool DebugDeathPressed { get { return UnityEngine.Input.GetKeyDown(KeyCode.K); } }
        public bool MenuPressed { get { return UnityEngine.Input.GetKeyDown(KeyCode.M) || UnityEngine.Input.GetKeyDown(KeyCode.JoystickButton1); } }
        public bool FireHeld { get { return testOverride ? testFire : UnityEngine.Input.GetButton("Fire") || UnityEngine.Input.GetAxisRaw("FireTrigger") > 0.1f; } }
        public bool FirePressed { get { return testOverride ? testFire : UnityEngine.Input.GetButtonDown("Fire") || triggerPressed; } }
        public bool ReloadPressed { get { return !testOverride && UnityEngine.Input.GetButtonDown("Reload"); } }
        public bool WeaponNextPressed { get { return !testOverride && (UnityEngine.Input.GetButtonDown("ChangeWeapon") || UnityEngine.Input.GetKeyDown(KeyCode.JoystickButton5)); } }

        private void Update()
        {
            bool triggerHeld = !testOverride && UnityEngine.Input.GetAxisRaw("FireTrigger") > 0.1f;
            triggerPressed = triggerHeld && !previousTriggerHeld;
            previousTriggerHeld = triggerHeld;
        }

        public void SetTestInput(Vector2 move, Vector2 aim, bool fire = false)
        {
            testOverride = true;
            testMove = move;
            testAim = aim;
            testFire = fire;
        }

        public void ClearTestInput()
        {
            testOverride = false;
            testMove = Vector2.zero;
            testAim = Vector2.zero;
            testFire = false;
        }
    }
}
