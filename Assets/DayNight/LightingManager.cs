using UnityEngine;
using System;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;

    [Header("Game Day Window (Hours)")]
    [SerializeField, Range(0f, 24f)] private float startHour = 8f;   // 08:00
    [SerializeField, Range(0f, 24f)] private float endHour = 24f;    // 24:00 (midnight)

    [Header("Clock")]
    [SerializeField, Range(0f, 24f)] private float TimeOfDay = 8f;
    [Tooltip("Game minutes advanced per real second. 5s IRL = 10m IG → 2.0")]
    [SerializeField] private float gameMinutesPerRealSecond = 2f;
    [SerializeField] private bool autoRestartNextDay = false;

    public event Action OnPassOut;

    private bool _passedOutThisFrame = false;

    // For debug logging
    private int _lastLoggedMinute = -1;

    private void OnEnable()
    {
        startHour = Mathf.Clamp(startHour, 0f, 24f);
        endHour = Mathf.Clamp(endHour, 0f, 24f);

        if (Application.isPlaying)
        {
            if (TimeOfDay < startHour || TimeOfDay > endHour)
                TimeOfDay = startHour;
        }
    }

    private void Update()
    {
        if (Preset == null)
            return;

        _passedOutThisFrame = false;

        if (Application.isPlaying)
        {
            float hoursPerRealSecond = gameMinutesPerRealSecond / 60f;
            TimeOfDay += Time.deltaTime * hoursPerRealSecond;

            if (TimeOfDay >= endHour)
            {
                TimeOfDay = endHour;

                if (!_passedOutThisFrame)
                {
                    _passedOutThisFrame = true;
                    OnPassOut?.Invoke();
                }

                if (autoRestartNextDay)
                    TimeOfDay = startHour;
            }

            // Log hour/minute once per in-game minute
            int totalMinutes = Mathf.FloorToInt(TimeOfDay * 60f);
            int currentHour = totalMinutes / 60;
            int currentMinute = totalMinutes % 60;

            if (currentMinute != _lastLoggedMinute)
            {
                _lastLoggedMinute = currentMinute;
                Debug.Log($"[LightingManager] Time: {currentHour:D2}:{currentMinute:D2}");
            }

            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation =
                Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight == null)
        {
            if (RenderSettings.sun != null)
            {
                DirectionalLight = RenderSettings.sun;
            }
            else
            {
                var lights = GameObject.FindObjectsOfType<Light>();
                foreach (var light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        DirectionalLight = light;
                        break;
                    }
                }
            }
        }

        TimeOfDay = Mathf.Clamp(TimeOfDay, 0f, 24f);
    }

    public void ResetToStartOfDay() => TimeOfDay = startHour;
    public float GetTimeOfDay() => TimeOfDay;
}
