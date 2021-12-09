using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class stickToPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameEvent rainEvent;
    [SerializeField] GameEvent baseEvent;




    VisualEffect vfx;

    void Start()
    {
        vfx = GetComponent<VisualEffect>();

        vfx.Stop();
        rainEvent.onOpen += vfx.Play;
        baseEvent.onOpen += vfx.Stop;

    }
}
