using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ambientAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().DOFade(1f, 2).SetDelay(1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
