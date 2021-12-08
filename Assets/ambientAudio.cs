using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ambientAudio : MonoBehaviour
{
    // Start is called before the first frame update

    [Range(0, 1)]
    [SerializeField] float volume = 1f;
    [SerializeField] GameEvent gameEvent;

    [SerializeField] bool doesAudioFade = false;

    [Range(0, 1)]
    [SerializeField] float audioFadeVolume = 0f;


    AudioSource audioSource;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.DOFade(volume, 2).SetDelay(1f);

        gameEvent.onClose += AudioFadeOut;
    }


    void AudioFadeOut()
    {
        if (doesAudioFade)
        {
            audioSource.DOFade(audioFadeVolume, 2);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
