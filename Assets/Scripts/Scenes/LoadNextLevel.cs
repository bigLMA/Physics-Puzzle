using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Scene to load")]
    private string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        // Load next scene if player
        if(other.name =="Player")
        {
            //SceneManager.LoadScene(sceneName);
            StartCoroutine(nameof(LoadAsyncScene));
        }
    }

    private IEnumerator LoadAsyncScene()
    {
        var loadOperation = SceneManager.LoadSceneAsync(sceneName);
        loadOperation.allowSceneActivation = false;

        while(!loadOperation.isDone)
        {
            if(loadOperation.progress>=0.9f) loadOperation.allowSceneActivation=true;

            yield return null;
        }
    }
}
