using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveAnim : MonoBehaviour
{

    [SerializeField] GameEvent forestEvent;
    private Renderer[] _materialRenderers;
    string LeavesMaterialName = "Leaves_Dissolve_Mat"; 
    string StoneMaterialName = "Stone_Dissolve_Mat"; 
    string BarkMaterialName = "Bark_Dissolve_Mat"; 

    float lerpDuration = 4.5f; 
    float startValue = 0f; 
    float endValue = 2f; 

    IEnumerator Lerp(Renderer materialRenderer)
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            materialRenderer.sharedMaterial.SetFloat("Vector1_50a59e65ae5140beb306c5c385ea81a6", Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        materialRenderer.sharedMaterial.SetFloat("Vector1_50a59e65ae5140beb306c5c385ea81a6", 2);
    }


    void dissolve()
    {
        GameObject Vegetation = GameObject.Find("Vegetation");
        _materialRenderers = Vegetation.transform.GetComponentsInChildren<Renderer>();
        
        foreach (var materialRenderer in _materialRenderers)
        {
            string assignedMaterialName = materialRenderer.sharedMaterial.name;

            if ((assignedMaterialName.Contains(LeavesMaterialName)) || (assignedMaterialName.Contains(StoneMaterialName)) || (assignedMaterialName.Contains(BarkMaterialName))) 
            { 
                StartCoroutine(Lerp(materialRenderer));
            } 

        }
    }

    void Start()
    {
        forestEvent.onOpen += dissolve;
    }
    
}
