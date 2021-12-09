using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class millRotate : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 startRot;
    float rotZ = 0;

    void Start()
    {
        startRot = transform.localRotation.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        rotZ += Time.deltaTime * 8f;
        transform.localRotation = Quaternion.Euler(startRot.x, startRot.y, rotZ);
        Debug.Log(rotZ);
    }
}
