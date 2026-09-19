using UnityEngine;

namespace MarineSlayer.Campaign
{
    [RequireComponent(typeof(Light))]
    public sealed class EmergencyLightFlicker : MonoBehaviour
    {
        [SerializeField] private float minimumIntensity = 0.35f;
        [SerializeField] private float maximumIntensity = 2.4f;
        [SerializeField] private float frequency = 13f;
        private Light source;
        private float phase;

        private void Awake()
        {
            source = GetComponent<Light>();
            phase = transform.position.sqrMagnitude * 0.137f;
        }

        private void Update()
        {
            if (source == null) return;
            float pulse = Mathf.Sin(Time.time * frequency + phase);
            float interruption = Mathf.Sin(Time.time * frequency * 0.27f + phase * 2f);
            float value = pulse > -0.35f && interruption > -0.8f ? maximumIntensity : minimumIntensity;
            source.intensity = value;
        }
    }
}
