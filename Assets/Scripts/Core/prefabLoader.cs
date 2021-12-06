using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabLoader : MonoBehaviour
{
    // Start is called before the first frame update

    public GameEvent baseScene;
    public GameEvent sceneOneOne;
    public GameEvent sceneOneTwo;
    public GameEvent sceneTwoOne;
    public GameEvent sceneTwoTwo;
    public GameEvent sceneThreeOne;
    public GameObject basePrefab;
    public GameObject sceneOneOnePrefab;
    public GameObject sceneOneTwoPrefab;
    public GameObject sceneTwoOnePrefab;
    public GameObject sceneTwoTwoPrefab;
    public GameObject sceneThreeOnePrefab;

    List<GameObject> prefabs = new List<GameObject>();
    List<GameEvent> gameEvents = new List<GameEvent>();





    public void LoadPrefab()
    {
        // Instantiate(prefabs[SceneController.Instance.activeSceneIndex][SceneController.Instance.choice], new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log(SceneController.Instance.activeSceneIndex);
        Debug.Log(SceneController.Instance.choice);
        Instantiate(prefabs[SceneController.Instance.activeSceneIndex + SceneController.Instance.choice], new Vector3(0, 0, 0), Quaternion.identity);

    }



    void Awake()
    {

        GameEvent[] gameEventsTemp = { baseScene, sceneOneOne, sceneOneTwo, sceneTwoOne, sceneTwoTwo };

        gameEvents.AddRange(gameEventsTemp);

        GameObject[] prefabsTemp = { basePrefab, sceneOneOnePrefab, sceneOneTwoPrefab, sceneTwoOnePrefab, sceneTwoTwoPrefab };

        // GameObject[,] prefabsTemp = new GameObject[,,] {
        //     {basePrefab},
        //     {sceneOneOnePrefab, sceneOneTwoPrefab},
        //     {sceneTwoOnePrefab, sceneTwoTwoPrefab},
        // }

        prefabs.AddRange(prefabsTemp);

        for (int i = 0; i < gameEventsTemp.Length; i++)
        {

            gameEvents[i].onOpen += LoadPrefab;

        }

    }

    void Update()
    {

    }
}
