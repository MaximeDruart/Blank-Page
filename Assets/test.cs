using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("--------------------------------");
        Debug.Log(GetComponent<Volume>().profile.components.Count);
        Debug.Log(GetComponent<Volume>().profile.components[0]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
