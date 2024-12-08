using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get actual time
        float timeOnScene = Time.unscaledTime-PlayerStatsManager.Instance.TimeSinceNewScene;
        float minutes = Mathf.Floor(timeOnScene / 60);
        float seconds = Mathf.Round(timeOnScene - minutes * 60);

        // Format minutes and seconds
        string minutesDisplay = minutes < 10f ? $"0{minutes}" :$"{minutes}";
        string secondsDisplay = seconds < 10f ? $"0{seconds}" : $"{seconds}";

        text.text = $"Time: {minutesDisplay}:{secondsDisplay}";
    }
}