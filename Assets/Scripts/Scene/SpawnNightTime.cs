using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNightTime : MonoBehaviour
{
    // Start is called before the first frame update
    float moonRot = 0f;
    float alpha = 0f;
    [SerializeField] Light moon;
    void Start()
    {
        Debug.Log("starting light transition");
    }

    // Update is called once per frame
    void Update()
    {
        alpha += Time.deltaTime * 0.01f;
    }

    void FixedUpdate()
    {
        moonRot = Mathf.Lerp(moonRot, 34, alpha);
        moon.transform.rotation = Quaternion.Euler(moonRot, 123f, 0f);
    }
}
