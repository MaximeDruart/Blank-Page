using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionCheck : MonoBehaviour
{
    Vector3 startScale;
    Vector3 activeScale;
    bool isInAnimationRange;
    // Start is called before the first frame update
    Material material;
    float materialAlpha = 0;
    float lastMaterialAlpha = 0;


    void Start()
    {
        startScale = transform.localScale;

        material = GetComponent<Renderer>().material;

        material.color = Color.clear;


    }
    void FloatAnim()
    {
        Vector3 scale = transform.localScale;
        scale.x = startScale.x + Mathf.Sin(Time.time * 2) * 22f * Time.deltaTime;
        scale.y = startScale.y + Mathf.Sin(Time.time * 2) * 22f * Time.deltaTime;

        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {

        isInAnimationRange = false;

        if (SceneController.Instance.character != null)
        {
            float distanceToCenter = Vector3.Distance(transform.position, SceneController.Instance.character.transform.position);

            if (distanceToCenter < 20) isInAnimationRange = true;

        }

    }

    private void FixedUpdate()
    {

        Vector3 scale = transform.localScale;
        if (isInAnimationRange)
        {
            scale.x = startScale.x + Mathf.Sin(Time.time * 2) * 22f;
            scale.y = startScale.y + Mathf.Sin(Time.time * 2) * 22f;
        }

        materialAlpha = Mathf.Lerp(materialAlpha, isInAnimationRange ? 1f : 0f, 0.1f);
        Color color = new Color(1f, 1f, 0f, materialAlpha);


        if (lastMaterialAlpha != materialAlpha)
        {
            material.color = color;
        }



        transform.localScale = Vector3.Lerp(transform.localScale, isInAnimationRange ? scale : Vector3.zero, 0.1f);
        lastMaterialAlpha = materialAlpha;

    }

}


