using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{

    int activeSceneIndex = 1;

    // Start is called before the first frame update

    AsyncOperation async;

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }


    IEnumerator OpenScene(string scene, LoadSceneMode mode = LoadSceneMode.Additive)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, mode);
        // Wait until the asynchronous scene fully loads
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }


        asyncLoad.allowSceneActivation = true;
    }

    public void CloseScene(string scene)
    {
        SceneManager.UnloadSceneAsync(scene);
    }


    public void GoToScene(int choice)
    {
        activeSceneIndex++;
        Debug.Log($"opening Scene{activeSceneIndex}-{choice}");
        StartCoroutine(OpenScene($"Scene{activeSceneIndex}-{choice}"));
    }

}
