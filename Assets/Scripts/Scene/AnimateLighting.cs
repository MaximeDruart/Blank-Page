using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Experimental.Rendering;
using DG.Tweening;




public class AnimateLighting : MonoBehaviour
{

    [SerializeField] GameEvent baseEvent;
    [SerializeField] GameEvent dayEvent;
    [SerializeField] GameEvent nightEvent;

    [SerializeField] GameObject baseVolumeContainer;
    [SerializeField] GameObject postVolumeContainer;
    [SerializeField] Light baseLight;

    Volume baseVolume;
    Volume postVolume;
    HDAdditionalLightData hdLight;
    PhysicallyBasedSky sky;
    ColorAdjustments colorAdjustments;
    WhiteBalance whiteBalance;


    void startNight()
    {
        Sequence sequence = DOTween.Sequence();

        // ANIMATE SATURATION
        sequence.Append(DOTween.To(() => colorAdjustments.saturation.value, x => colorAdjustments.saturation.value = x, 0, 2));

        // ANIMATE TEMPERATURE
        float targetTemperature = -20f;

        sequence.Join(DOTween.To(() => whiteBalance.temperature.value, x => whiteBalance.temperature.value = x, targetTemperature, 3));


        // ANIMATE DIRECTIONAL LIGHT INTENSITY
        float targetIntensity = 2420f;
        sequence.Join(DOTween.To(() => hdLight.intensity, x => hdLight.intensity = x, targetIntensity, 2));

        // ANIMATE DIRECTIONAL LIGHT COLOR
        float startKelvinColor = 8736f;
        float targetKelvinColor = 20000f;
        float k = startKelvinColor;

        sequence.Join(
            DOTween.To(() => k, x => k = x, targetKelvinColor, 2).OnUpdate(() =>
            {
                hdLight.SetColor(Color.white, k);
            })
        );

        float startExposure = sky.exposure.value;
        float targetExposure = 0;
        float exp = startExposure;

        // sequence.Join(
        //     DOTween.To(() => exp, x => exp = x, targetExposure, 2).OnUpdate(() =>
        //     {
        //         sky.exposure.value = exp;
        //     })
        // );

        // ANIMATE SPACE MAP : INCREASE SPACE EMISSION MULTIPLIER
        float targetSpaceMult = 1000f;
        float startMult = 0;

        sequence.Append(
            DOTween.To(() => startMult, x => startMult = x, targetSpaceMult, 7).OnUpdate(() =>
            {
                sky.spaceEmissionMultiplier.value = startMult;

            })
        );

    }

    void startDay()
    {
        Sequence sequence = DOTween.Sequence();

        // ANIMATE SATURATION
        sequence.Append(DOTween.To(() => colorAdjustments.saturation.value, x => colorAdjustments.saturation.value = x, 0, 3));

        // ColorCurves colorCurves;
        // if (postVolume.profile.TryGet<ColorCurves>(out colorCurves))
        // {
        //     colorCurves.active = false;
        // }

        // ANIMATE TEMPERATURE
        float targetTemperature = 20f;

        sequence.Join(DOTween.To(() => whiteBalance.temperature.value, x => whiteBalance.temperature.value = x, targetTemperature, 3));

        // ANIMATE DIRECTIONAL LIGHT INTENSITY
        float targetIntensity = 5000f;
        sequence.Join(DOTween.To(() => hdLight.intensity, x => hdLight.intensity = x, targetIntensity, 3));

        // ANIMATE DIRECTIONAL LIGHT COLOR
        float startKelvinColor = 8736f;
        float targetKelvinColor = 6570f;
        float k = startKelvinColor;

        sequence.Join(
            DOTween.To(() => k, x => k = x, targetKelvinColor, 3).OnUpdate(() =>
            {
                hdLight.SetColor(Color.white, k);
            })
        );

        // ANIMATE SUN SIZE AND POS
        float targetSunSize = 1.83f;
        sequence.Join(DOTween.To(() => hdLight.angularDiameter, x => hdLight.angularDiameter = x, targetSunSize, 3));

        Vector3 targetRotation = new Vector3(150f, baseLight.transform.rotation.y, baseLight.transform.rotation.z);


        sequence.Join(baseLight.transform.DORotate(targetRotation, 5));
    }


    void spaceRotationLoop()
    {
        Vector3 rot = sky.spaceRotation.value;
        rot.z += Time.deltaTime * 0.5f;
        sky.spaceRotation.value = rot;
    }

    void Start()
    {
        baseVolume = baseVolumeContainer.GetComponent<Volume>();
        postVolume = postVolumeContainer.GetComponent<Volume>();
        hdLight = baseLight.GetComponent<HDAdditionalLightData>();

        PhysicallyBasedSky skyTemp;
        if (baseVolume.profile.TryGet<PhysicallyBasedSky>(out skyTemp))
        {
            sky = skyTemp;
        }

        ColorAdjustments colorTemp;

        if (postVolume.profile.TryGet<ColorAdjustments>(out colorTemp))
        {
            colorAdjustments = colorTemp;
        }

        WhiteBalance whiteBalanceTemp;
        if (postVolume.profile.TryGet<WhiteBalance>(out whiteBalanceTemp))
        {
            whiteBalance = whiteBalanceTemp;
        }


        dayEvent.onOpen += startDay;
        nightEvent.onOpen += startNight;
    }



    // Update is called once per frame
    void Update()
    {
        spaceRotationLoop();
    }
}
