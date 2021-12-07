using UnityEngine;


public class TriggerAnim : MonoBehaviour
{



    public GameEvent activeGameEvent;
    public GameEvent nextGameEvent;
    public Transform levelSelector;

    // Start is called before the first frame update
    bool isHidden = false;

    Collider objectCollider;
    int id;

    public TriggerData triggerData;
    public GameObject triggerRadius;
    public GameObject triggerModel;

    float rotX = 0;
    float rotY = 0;

    void Start()
    {
        objectCollider = GetComponent<Collider>();

        id = GetInstanceID();
    }


    void FloatAnim()
    {
        rotX += Time.deltaTime * 5f;
        rotY -= Time.deltaTime * 5f;

        triggerModel.transform.localRotation = Quaternion.Euler(rotX, 0, rotY);
    }

    void GoToNext()
    {
        // get the tag's last letter (either trigger-1 or trigger-2 so 1 or 2)
        int choice = triggerData.choiceIndex;
        activeGameEvent.close();

        // choice should be 0 or 1
        SceneController.Instance.choice = choice - 1;

        nextGameEvent.open();

        SceneController.Instance.activeSceneIndex += 2;

    }



    // Update is called once per frame
    void Update()
    {
        FloatAnim();



        if (SceneController.Instance.character != null)
        {
            float distanceToCenter = Vector3.Distance(transform.position, SceneController.Instance.character.transform.position);

            if (distanceToCenter < 10 && !isHidden)
            {
                GoToNext();
                isHidden = true;

            }

        }

    }

    void FixedUpdate()
    {
        if (isHidden)
        {
            Destroy(levelSelector.gameObject);
        }

    }
}
