using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
/// <summary>
/// Simple example of Grabbing system.
/// </summary>
public class SimpleGrabSystem : MonoBehaviour
{

    public GameEvent baseScene;

    // Reference to the character camera.
    [SerializeField]
    private Camera characterCamera;
    // Reference to the slot for holding picked item.
    [SerializeField]
    private Transform slot;
    // Reference to the currently held item.
    private PickableItem pickedItem;

    [SerializeField] AudioClip[] audio1;
    [SerializeField] AudioClip[] audio2;
    [SerializeField] AudioClip[] audio3;
    [SerializeField] AudioSource audioSource;
    TextMeshPro textMesh;

    /// <summary>
    /// Method called very frame.
    /// </summary>

    private void Start()
    {
        // AudioSource audioSource = GetComponent<AudioSource>();

    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Check if player picked some item already
            if (pickedItem)
            {
                // If yes, drop picked item
                DropItem(pickedItem);
            }
            else
            {
                // If no, try to pick item in front of the player
                // Create ray from center of the screen
                var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
                RaycastHit hit;
                // Shot ray to find object to pick
                if (Physics.Raycast(ray, out hit, 3.5f))
                {

                    // Check if object is pickable
                    var pickable = hit.transform.GetComponent<PickableItem>();
                    // If object has PickableItem class
                    if (pickable)
                    {
                        // Pick it
                        PickItem(pickable);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Method for picking up item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void PickItem(PickableItem item)
    {
        // Assign reference
        pickedItem = item;
        // Disable rigidbody and reset velocities
        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;
        // Set Slot as a parent
        item.transform.SetParent(slot);
        // Reset position and rotation


        item.transform.DOLocalMove(Vector3.forward * 1.85f, 0.5f);
        item.transform.DOLocalRotate(new Vector3(-104.254f, 149.318f, -135.929f), 0.5f).OnComplete(() =>
        {
            StartEnd(item);
        });
    }
    /// <summary>
    /// Method for dropping item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void DropItem(PickableItem item)
    {
        // Remove reference
        pickedItem = null;
        // Remove parent
        item.transform.SetParent(null);
        // Enable rigidbody
        item.Rb.isKinematic = false;
        // Add force to throw item a little bit
        item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);

        Reset();
    }

    void Reset()
    {
        Debug.Log("reset");
        StartCoroutine(RemoveItems());
    }

    IEnumerator RemoveItems()
    {

        // Debug.Log(SceneController.Instance.prefabContainer.transform.childCount);
        // foreach (Transform item in SceneController.Instance.prefabContainer.transform)
        // {
        //     Debug.Log(item.gameObject.name);
        //     SceneController.Instance.prefabContainer.transform.Destroy(item.gameObject);
        //     yield return new WaitForSeconds(.08f);
        // }

        int childrens = SceneController.Instance.prefabContainer.transform.childCount;
        for (int i = childrens - 1; i > 0; i--)
        {
            int innerChildrens = SceneController.Instance.prefabContainer.transform.GetChild(i).childCount;
            for (int j = innerChildrens - 1; j > 0; j--)
            {
                GameObject.Destroy(SceneController.Instance.prefabContainer.transform.GetChild(i).GetChild(j).gameObject);
                yield return new WaitForSeconds(.3f);
            }

            GameObject.Destroy(SceneController.Instance.prefabContainer.transform.GetChild(i).gameObject);
        }


        Destroy(SceneController.Instance.prefabContainer.transform.GetChild(0).gameObject);
        SceneController.Instance.activeSceneIndex = 0;
        SceneController.Instance.choice = 0;

        baseScene.open();
        SceneController.Instance.activeSceneIndex++;



    }

    private void StartEnd(PickableItem item)
    {

        GameObject textItem = item.transform.GetChild(0).gameObject;

        textMesh = textItem.GetComponent<TextMeshPro>();


        List<AudioClip> selectedAudios = new List<AudioClip>();
        List<string> selectedTexts = new List<string>();

        List<int> choices = SceneController.Instance.choiceHistory;


        // DESERT/FORET + JOUR/NUIT
        if (choices[0] == 1 && choices[1] == 1)
        {
            selectedAudios.Add(audio1[0]);
            selectedTexts.Add("Soir dans la foret");
        }
        if (choices[0] == 0 && choices[1] == 1)
        {
            selectedAudios.Add(audio1[1]);
            selectedTexts.Add("Soir dans le desert");
        }
        if (choices[0] == 1 && choices[1] == 0)
        {
            selectedAudios.Add(audio1[2]);
            selectedTexts.Add("Aube dans la foret");
        }
        if (choices[0] == 0 && choices[1] == 0)
        {
            selectedAudios.Add(audio1[3]);
            selectedTexts.Add("Aube dans le desert");
        }


        // MOULIN/VESTIGES + BROUILLARD/PLUIE
        if (choices[2] == 1 && choices[3] == 1)
        {
            selectedAudios.Add(audio2[0]);
            selectedTexts.Add("Des vestiges couvert de pluie");
        }
        if (choices[2] == 0 && choices[3] == 1)
        {
            selectedAudios.Add(audio2[1]);
            selectedTexts.Add("Du moulin couvert de pluie");
        }
        if (choices[2] == 0 && choices[3] == 0)
        {
            selectedAudios.Add(audio2[2]);
            selectedTexts.Add("Du moulin dans le brouillard");
        }
        if (choices[2] == 1 && choices[3] == 0)
        {
            selectedAudios.Add(audio2[3]);
            selectedTexts.Add("Des vestiges dans le brouillard");
        }


        // VIE/MORT
        if (choices[4] == 0)
        {
            selectedAudios.Add(audio3[0]);
            selectedTexts.Add("Un monde reprend vie");
        }
        if (choices[4] == 1)
        {
            selectedAudios.Add(audio3[1]);
            selectedTexts.Add("Fige par la mort");
        }


        float sumLength = 0;
        float clipDelay = 0.5f;

        for (int i = 0; i < selectedAudios.Count; i++)
        {
            StartCoroutine(PlayClip(selectedAudios[i], selectedTexts[i], sumLength));
            sumLength += (selectedAudios[i].length + clipDelay);
        }

        // Invoke("Reset", sumLength + 1f);
    }



    IEnumerator PlayClip(AudioClip audioClip, string textContent, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.clip = audioClip;
        StartCoroutine(AddText(textContent));
        audioSource.Play();

    }
    IEnumerator AddText(string textContent)
    {
        foreach (var letter in textContent)
        {
            textMesh.text += letter;
            yield return new WaitForSeconds(.08f);
        }
        textMesh.text += "\n";
    }

}