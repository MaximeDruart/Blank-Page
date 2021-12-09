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
    public GameEvent sceneThreeTwo;
    public GameEvent sceneFourOne;
    public GameEvent sceneFourTwo;
    public GameEvent sceneFiveOne;
    public GameEvent sceneFiveTwo;

    public GameObject basePrefab;
    public GameObject sceneOneOnePrefab;
    public GameObject sceneOneTwoPrefab;
    public GameObject sceneTwoOnePrefab;
    public GameObject sceneTwoTwoPrefab;
    public GameObject sceneThreeOnePrefab;
    public GameObject sceneThreeTwoPrefab;
    public GameObject sceneFourOnePrefab;
    public GameObject sceneFourTwoPrefab;
    public GameObject sceneFiveOnePrefab;
    public GameObject sceneFiveTwoPrefab;

    List<GameObject> prefabs = new List<GameObject>();
    List<GameEvent> gameEvents = new List<GameEvent>();





    public void LoadPrefab()
    {

        GameObject prefab = prefabs[SceneController.Instance.activeSceneIndex + SceneController.Instance.choice];


        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }



    void Awake()
    {

        GameEvent[] gameEventsTemp = { baseScene, sceneOneOne, sceneOneTwo, sceneTwoOne, sceneTwoTwo, sceneThreeOne, sceneThreeTwo, sceneFourOne, sceneFourTwo, sceneFiveOne, sceneFiveTwo };

        gameEvents.AddRange(gameEventsTemp);



        GameObject[] prefabsTemp = { basePrefab, sceneOneOnePrefab, sceneOneTwoPrefab, sceneTwoOnePrefab, sceneTwoTwoPrefab, sceneThreeOnePrefab, sceneThreeTwoPrefab, sceneFourOnePrefab, sceneFourTwoPrefab, sceneFiveOnePrefab, sceneFiveTwoPrefab };

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
