using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void LoadMenu() => StartCoroutine(Load());

   private IEnumerator Load()
   {
        var loadOperation = SceneManager.LoadSceneAsync("Menu");
        loadOperation.allowSceneActivation = false;

        while(!loadOperation.isDone)
        {
            if(loadOperation.progress>=0.9f) loadOperation.allowSceneActivation=true;

            yield return null;
        }
   }
}
