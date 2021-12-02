using UnityEngine;


public class TriggerAnim : MonoBehaviour
{


    public Transform levelSelector;

    Vector3 startPosition;
    // Start is called before the first frame update
    bool isHovering = false;
    bool isHidden = false;

    Collider objectCollider;
    int id;
    void Start()
    {
        startPosition = transform.localPosition;
        Debug.Log("started");

        objectCollider = GetComponent<Collider>();

        id = GetInstanceID();
    }


    void FloatAnim()
    {
        Vector3 position = transform.localPosition;
        position.y = startPosition.y + Mathf.Sin(Time.time * 5) * 0.5f;
        transform.localPosition = position;
    }

    void Hide()
    {

    }



    // Update is called once per frame
    void Update()
    {
        FloatAnim();

        // send ray from the center of cam to where you're looking at
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            // assign scriptableobject to game object and get data thtough there
            // hit.collider.gameObject.GetComponent<TriggerData>().choiceIndex;
            // check if hit item is current game object
            bool objectIsHit = hit.collider.GetInstanceID() == objectCollider.GetInstanceID();
            isHovering = objectIsHit;

            if (objectIsHit && Input.GetButtonDown("Fire1"))
            {
                // get the tag's last letter (either trigger-1 or trigger-2 so 1 or 2)
                char choice = tag[tag.Length - 1];
                // load the scene 'Scene{activeSceneIndex}-{choice}'
                SceneController.Instance.GoToScene(choice);
                isHidden = true;


            }
        }
        else isHovering = false;


    }

    void FixedUpdate()
    {
        if (isHidden)
        {
            levelSelector.transform.localScale = Vector3.Lerp(levelSelector.transform.localScale, Vector3.zero, 0.1f);
        }
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * (isHovering ? 3f : 1f), 0.1f);

    }
}
