using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{   

    public GameEvent baseScene;

    void Start()
    {
        baseScene.open();
        SceneController.Instance.activeSceneIndex++;
    }

}
