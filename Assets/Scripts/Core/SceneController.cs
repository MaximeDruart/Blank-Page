using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{

    public int activeSceneIndex = 0;
    public int choice = 0;
    public GameObject character;

    public GameObject prefabContainer;

    public List<int> choiceHistory = new List<int>();

}
