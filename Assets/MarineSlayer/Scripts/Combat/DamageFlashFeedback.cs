using UnityEngine;

namespace MarineSlayer.Combat
{
    [RequireComponent(typeof(Health))]
    public sealed class DamageFlashFeedback : MonoBehaviour
    {
        [SerializeField] private float flashSeconds = 0.1f;
        private Health health;
        private Renderer[] renderers;
        private Material[] materials;
        private Color[] baseColors;
        private bool[] supportsColor;
        private float restoreAt;
        private bool flashing;

        private void Awake()
        {
            health = GetComponent<Health>();
            renderers = GetComponentsInChildren<Renderer>(true);
            materials = new Material[renderers.Length];
            baseColors = new Color[renderers.Length];
            supportsColor = new bool[renderers.Length];
            for (int index = 0; index < renderers.Length; index++)
            {
                materials[index] = renderers[index].material;
                supportsColor[index] = materials[index].HasProperty("_Color");
                if (supportsColor[index]) baseColors[index] = materials[index].color;
            }
            health.Damaged += OnDamaged;
        }

        private void OnDestroy()
        {
            if (health != null) health.Damaged -= OnDamaged;
        }

        private void Update()
        {
            if (!flashing || Time.time < restoreAt) return;
            flashing = false;
            for (int index = 0; index < materials.Length; index++)
                if (supportsColor[index]) materials[index].color = baseColors[index];
        }

        private void OnDamaged(Health value, DamageInfo damage)
        {
            Color flash = damage.type == DamageType.Energy
                ? new Color(0.2f, 0.9f, 1f, 1f)
                : damage.type == DamageType.Explosive
                    ? new Color(1f, 0.45f, 0.08f, 1f)
                    : new Color(1f, 0.16f, 0.12f, 1f);
            for (int index = 0; index < materials.Length; index++)
                if (supportsColor[index]) materials[index].color = flash;
            restoreAt = Time.time + flashSeconds;
            flashing = true;
        }
    }
}
