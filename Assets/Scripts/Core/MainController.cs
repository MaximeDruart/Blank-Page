using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameEvent baseScene;

    void Start()
    {
        baseScene.open();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
