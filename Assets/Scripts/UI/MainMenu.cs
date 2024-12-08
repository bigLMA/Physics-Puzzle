using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;


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

        foreach(var timer in textTimers)
        {
            timer.text = "Best time: n/a";
        }
    }

    public void Exit()
    {
        print("ALEGUS");

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
            }

            yield return null;
        }
    }
}
