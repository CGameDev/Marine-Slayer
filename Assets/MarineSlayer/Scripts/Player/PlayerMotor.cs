using MarineSlayer.Core;
using UnityEngine;

namespace MarineSlayer.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public sealed class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float turnSpeed = 720f;
        private Rigidbody body;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            body.interpolation = RigidbodyInterpolation.Interpolate;
        }

        private void FixedUpdate()
        {
            if (GameRoot.Instance.State.CurrentState != GameState.Playing) return;
            Vector2 rawMove = GameRoot.Instance.Input.Move;
            Vector3 move = new Vector3(rawMove.x, 0f, rawMove.y);
            if (move.sqrMagnitude > 1f) move.Normalize();
            body.MovePosition(body.position + move * moveSpeed * Time.fixedDeltaTime);

            Vector2 rawAim = GameRoot.Instance.Input.Aim;
            Vector3 facing = new Vector3(rawAim.x, 0f, rawAim.y);
            if (facing.sqrMagnitude < 0.04f) facing = move;
            if (facing.sqrMagnitude > 0.04f)
            {
                Quaternion target = Quaternion.LookRotation(facing.normalized, Vector3.up);
                body.MoveRotation(Quaternion.RotateTowards(body.rotation, target, turnSpeed * Time.fixedDeltaTime));
            }
        }
    }
}
