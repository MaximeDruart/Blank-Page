using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{

    int activeSceneIndex = 1;

    // Start is called before the first frame update


    public void OpenScene(string scene, LoadSceneMode mode = LoadSceneMode.Additive)
    {

        SceneManager.LoadSceneAsync(scene, mode);
    }

    public void CloseScene(string scene)
    {
        SceneManager.UnloadSceneAsync(scene);
    }


    public void GoToScene(char choice)
    {
        activeSceneIndex++;
        Debug.Log($"opening Scene{activeSceneIndex}-{choice}");
        OpenScene($"Scene{activeSceneIndex}-{choice}");
    }

}
