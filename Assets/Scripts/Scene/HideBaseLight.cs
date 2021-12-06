using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HideBaseLight : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Light baseLight;


    void OnEnable()
    {
        // TriggerAnim.OnClicked += Hide;
        Debug.Log("adding it");
    }
    void OnDisable()
    {
        // TriggerAnim.OnClicked -= Hide;
    }

    void Hide()
    {
        Debug.Log("playnig it");
        baseLight.DOIntensity(0f, 3f);
        // TriggerAnim.OnClicked -= Hide;
    }

}
