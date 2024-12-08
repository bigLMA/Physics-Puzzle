using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using static UnityEngine.Rendering.BoolParameter;


public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] textTimers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Enable cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        int idx = 0;

        foreach(var timer in textTimers)
        {
            string levelName = timer.name;
            string displayTime = "";

            if (idx< PlayerStatsManager.Instance.timeRecords.Count)
            {
                float total = PlayerStatsManager.Instance.timeRecords[idx];
                float minutes = Mathf.Floor(total / 60);
                float seconds = Mathf.Round(total - 60 * minutes);

                //Format minutes and seconds
                string minutesDisplay = minutes < 10f ? $"0{minutes}" : $"{minutes}";
                string secondsDisplay = seconds < 10f ? $"0{seconds}" : $"{seconds}";
                displayTime = minutesDisplay + ":"+ secondsDisplay;
            }
            else
            {
                displayTime = "n/a";
            }

            ++idx;

            timer.text = $"Best time: {displayTime}";
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenLevel(string level)
    {
        StartCoroutine(OpenLevelCoroutine(level));
    }

    private IEnumerator OpenLevelCoroutine(string level)
    {
        var operation = SceneManager.LoadSceneAsync(level);
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            if(operation.progress>=0.9f)
            {
                operation.allowSceneActivation=true;
                PlayerStatsManager.Instance.TimeSinceNewScene = Time.unscaledTime;
            }

            yield return null;
        }
    }
}
