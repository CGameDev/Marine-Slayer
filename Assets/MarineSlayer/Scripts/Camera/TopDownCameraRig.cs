using MarineSlayer.Core;
using UnityEngine;

namespace MarineSlayer.CameraSystem
{
    public sealed class TopDownCameraRig : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 12f, -10f);
        [SerializeField] private float followTime = 0.12f;
        [SerializeField] private float aimLookAhead = 1.75f;
        private Vector3 velocity;

        public void SetTarget(Transform value) { target = value; }

        private void LateUpdate()
        {
            if (target == null) return;
            Vector2 aim = GameRoot.Instance.Input.Aim;
            Vector3 lookAhead = new Vector3(aim.x, 0f, aim.y) * aimLookAhead;
            Vector3 desired = target.position + offset + lookAhead;
            transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, followTime);
            transform.rotation = Quaternion.Euler(45f, 0f, 0f);
        }
    }
}
