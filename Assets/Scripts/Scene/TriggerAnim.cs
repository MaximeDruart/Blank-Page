using UnityEngine;
using DG.Tweening;


public class TriggerAnim : MonoBehaviour
{



    public GameEvent activeGameEvent;
    public GameEvent nextGameEvent;
    public Transform levelSelector;
    public AudioSource nextAudio;

    public Transform lucioles;

    // Start is called before the first frame update
    bool isDisabled = false;

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

        SceneController.Instance.choiceHistory.Add(choice - 1);

        nextGameEvent.open();

        SceneController.Instance.activeSceneIndex += 2;

        nextAudio.DOFade(0, 0.6f);

    }



    // Update is called once per frame
    void Update()
    {
        // FloatAnim();

        if (SceneController.Instance.character != null)
        {
            float distanceToCenter = Vector3.Distance(transform.position, SceneController.Instance.character.transform.position);

            if (distanceToCenter < 8 && !isDisabled)
            {
                isDisabled = true;
                GoToNext();

                int index = transform.GetSiblingIndex();

                // assuming it's always a 2-way choice, we can just take the opposite value

                index = index == 1 ? 0 : 1;


                Destroy(levelSelector.GetChild(index).gameObject);
                triggerRadius.SetActive(false);

                lucioles.gameObject.SetActive(false);


            }

        }

    }
}
