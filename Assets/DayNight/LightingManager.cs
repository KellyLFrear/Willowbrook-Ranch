using UnityEngine;
using System;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    public static LightingManager Instance { get; private set; }

    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //[SerializeField] private GUIUpdater guiUpdater;

    [Header("Game Day Window (Hours)")]
    [SerializeField, Range(0f, 24f)] private float startHour = 8f; // 8:00am
    [SerializeField, Range(0f, 24f)] private float endHour = 24f;  // Midnight

    [Header("Clock")]
    [SerializeField, Range(0f, 24f)] private float TimeOfDay = 8f;
    [Tooltip("Game minutes advanced per real second. 5s IRL = 10m IG → 2.0")]
    [SerializeField] private float gameMinutesPerRealSecond = 2f;

    [Header("Calendar")]
    [SerializeField] private int daysInMonth = 30;
    [SerializeField] private int currentDay = 1;  // start on Day 1

    public event Action OnPassOut;
    public event Action<int> OnDayChanged;   // passes new day number (1..daysInMonth)

    private bool _passedOutThisFrame = false;
    private int _lastLoggedMinute = -1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        startHour = Mathf.Clamp(startHour, 0f, 24f);
        endHour = Mathf.Clamp(endHour, 0f, 24f);

        if (daysInMonth < 1) daysInMonth = 30;
        currentDay = Mathf.Clamp(currentDay, 1, daysInMonth);

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
            // Advance time
            float hoursPerRealSecond = gameMinutesPerRealSecond / 60f;
            TimeOfDay += Time.deltaTime * hoursPerRealSecond;

            // Hit midnight → pass out → next day at 8am
            if (TimeOfDay >= endHour)
            {
                TimeOfDay = endHour;

                if (!_passedOutThisFrame)
                {
                    _passedOutThisFrame = true;

                    // Debug.Log("[LightingManager] Player passed out at midnight!");
                    OnPassOut?.Invoke();

                    // Makes Pop Up Window Appear To Inform The Player They Passed Out
                    GUIUpdater.Instance.ShowPassedOutPopUp();
                    // Commenting This Out In Case Pop-Ups Don't Work:
                    // AdvanceToNextDay(fromPassOut: true);
                }
            }

            // Log hour/minute once per in-game minute (you’re using GUI, so this is optional)
            int totalMinutes = Mathf.FloorToInt(TimeOfDay * 60f);
            int currentHour = totalMinutes / 60;
            int currentMinute = totalMinutes % 60;

            if (currentMinute != _lastLoggedMinute)
            {
                _lastLoggedMinute = currentMinute;
                // Debug.Log($"[LightingManager] Day {currentDay}/{daysInMonth} – Time: {currentHour:D2}:{currentMinute:D2}");
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
        if (daysInMonth < 1) daysInMonth = 30;
        currentDay = Mathf.Clamp(currentDay, 1, daysInMonth);
    }

    // --- NEW DAY LOGIC ---

    public void AdvanceToNextDay(bool fromPassOut)
    {
        // Advance calendar
        currentDay++;
        if (currentDay > daysInMonth)
        {
            currentDay = 1; // loop back to Day 1 after Day 30
        }

        // Reset clock to start of day
        TimeOfDay = startHour;

        Debug.Log($"[LightingManager] New Day {currentDay} started at {startHour:0}:00 (fromPassOut: {fromPassOut})");

        // Grow plants / daily systems
        if (PlantManager.Instance != null)
        {
            PlantManager.Instance.AdvanceDay();
        }

        // Notify UI or systems
        OnDayChanged?.Invoke(currentDay);
    }

    /// <summary>
    /// Manual sleep: call from bed interaction to skip to next day at 8am.
    /// </summary>
    public void SleepToNextDay()
    {
        AdvanceToNextDay(fromPassOut: false);
    }

    /// <summary>
    /// Only reset the clock to 8am without advancing the day (for debugging/cutscenes).
    /// </summary>
    public void ResetToStartOfDay()
    {
        TimeOfDay = startHour;
        Debug.Log("[LightingManager] Clock reset to startHour (no day advancement).");
    }

    public float GetTimeOfDay() => TimeOfDay;
    public int CurrentDay => currentDay;

    // Helper properties for GUI
    public int CurrentHour
    {
        get
        {
            int totalMinutes = Mathf.FloorToInt(TimeOfDay * 60f);
            return totalMinutes / 60;
        }
    }

    public int CurrentMinute
    {
        get
        {
            int totalMinutes = Mathf.FloorToInt(TimeOfDay * 60f);
            return totalMinutes % 60;
        }
    }
}
