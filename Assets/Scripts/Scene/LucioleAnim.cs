using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucioleAnim : MonoBehaviour
{   

    public Transform Trigger;

        Vector3 startPosition;
    public float Radius = 5;
    public float Speed  = 1;

    void Start()
    {   
        startPosition = transform.localPosition;
        Radius += Random.Range(-1.0f, 1.0f);
        Speed += Random.Range(-1.0f, 1.0f);
    }

    void Update()
    {
        // transform.Rotate(Vector3.back * Random.Range(0.1f, 2.0f));
        transform.RotateAround(Trigger.transform.position, Vector3.up, 30 * Time.deltaTime);
        transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.Self);
 
        if(Vector3.Distance(Trigger.transform.position, transform.position) > Radius)
        {
            Vector3 dir = Trigger.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
 
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }


        // Vector3 position = transform.localPosition;
        // position.y = startPosition.y + Mathf.Sin(Time.time * 5) * 0.5f;
        // transform.localPosition = position;

        // transform.RotateAround(Trigger.transform.position, Vector3.up, 30 * Time.deltaTime);



    }
}
