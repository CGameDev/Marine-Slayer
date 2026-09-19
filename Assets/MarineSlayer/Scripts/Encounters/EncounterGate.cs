using UnityEngine;

namespace MarineSlayer.Encounters
{
    [RequireComponent(typeof(Collider))]
    public sealed class EncounterGate : MonoBehaviour
    {
        private Collider gateCollider;
        private Renderer gateRenderer;
        private Material gateMaterial;

        public bool IsLocked { get; private set; }

        private void Awake()
        {
            gateCollider = GetComponent<Collider>();
            gateRenderer = GetComponent<Renderer>();
            if (gateRenderer != null) gateMaterial = gateRenderer.material;
            SetLocked(false);
        }

        public void SetLocked(bool value)
        {
            IsLocked = value;
            if (gateCollider == null) gateCollider = GetComponent<Collider>();
            gateCollider.enabled = value;
            if (gateMaterial == null && gateRenderer != null) gateMaterial = gateRenderer.material;
            if (gateMaterial != null)
                gateMaterial.color = value ? new Color(0.9f, 0.08f, 0.04f, 1f) : new Color(0.08f, 0.8f, 0.35f, 1f);
        }
    }
}
