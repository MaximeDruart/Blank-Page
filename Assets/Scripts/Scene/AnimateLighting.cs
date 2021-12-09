using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using DG.Tweening;




public class AnimateLighting : MonoBehaviour
{

    [SerializeField] GameEvent baseEvent;
    [SerializeField] GameEvent dayEvent;
    [SerializeField] GameEvent nightEvent;
    [SerializeField] GameEvent rainEvent;

    [SerializeField] GameObject baseVolumeContainer;
    [SerializeField] GameObject postVolumeContainer;
    [SerializeField] Light baseLight;

    Volume baseVolume;
    Volume postVolume;
    HDAdditionalLightData hdLight;
    PhysicallyBasedSky sky;
    ColorAdjustments colorAdjustments;
    WhiteBalance whiteBalance;

    string timeChosen;


    void startNight()
    {
        Sequence sequence = DOTween.Sequence();

        // ANIMATE SATURATION
        sequence.Append(DOTween.To(() => colorAdjustments.saturation.value, x => colorAdjustments.saturation.value = x, 0, 2));


        // ANIMATE TEMPERATURE
        float targetTemperature = -40f;

        sequence.Join(DOTween.To(() => whiteBalance.temperature.value, x => whiteBalance.temperature.value = x, targetTemperature, 3));


        // ANIMATE DIRECTIONAL LIGHT INTENSITY
        float targetIntensity = 1200f;
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

        // big lag transitioning exposure, need to find a workaorund

        // float startExposure = sky.exposure.value;
        // float targetExposure = 0;
        // float exp = startExposure;

        // sequence.Join(
        //     DOTween.To(() => exp, x => exp = x, targetExposure, 2).OnUpdate(() =>
        //     {
        //         sky.exposure.value = exp;
        //     })
        // );

        // ANIMATE SPACE MAP : INCREASE SPACE EMISSION MULTIPLIER
        float targetSpaceMult = 300f;
        float startMult = 0;

        sequence.Append(
            DOTween.To(() => startMult, x => startMult = x, targetSpaceMult, 4).OnUpdate(() =>
            {
                sky.spaceEmissionMultiplier.value = startMult;

            })
        );

        sequence.Join(DOTween.To(() => colorAdjustments.postExposure.value, x => colorAdjustments.postExposure.value = x, -0.7f, 2));

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

    void startRain()
    {

        Sequence sequence = DOTween.Sequence();

        float startMult = sky.spaceEmissionMultiplier.value;
        float targetSpaceMult = 0f;

        sequence.Append(
            DOTween.To(() => startMult, x => startMult = x, targetSpaceMult, 1.5f).OnUpdate(() =>
            {
                sky.spaceEmissionMultiplier.value = startMult;

            })
        );

        sequence.Join(DOTween.To(() => colorAdjustments.saturation.value, x => colorAdjustments.saturation.value = x, -20, 2));

        Color color = Color.white;

        float greyValue = 1f;


        sequence.Join(
            DOTween.To(() => greyValue, x => greyValue = x, 0.9f, 2f).OnUpdate(() =>
            {

                color.r = greyValue;
                color.g = greyValue;
                color.b = greyValue;

                colorAdjustments.colorFilter.value = color;

            })
        );

        float startExpo = colorAdjustments.postExposure.value;

        sequence.Join(DOTween.To(() => colorAdjustments.postExposure.value, x => colorAdjustments.postExposure.value = x, startExpo - 0.2f, 2));


    }

    void resetEffects()
    {
        Sequence sequence = DOTween.Sequence();

        float startMult = sky.spaceEmissionMultiplier.value;
        float targetSpaceMult = 0f;

        sequence.Append(
            DOTween.To(() => startMult, x => startMult = x, targetSpaceMult, 1.5f).OnUpdate(() =>
            {
                sky.spaceEmissionMultiplier.value = startMult;

            })
        );

        sequence.Join(DOTween.To(() => colorAdjustments.saturation.value, x => colorAdjustments.saturation.value = x, -100, 2));

        Color color = Color.white;

        float greyValue = colorAdjustments.colorFilter.value.r;


        sequence.Join(
            DOTween.To(() => greyValue, x => greyValue = x, 1f, 2f).OnUpdate(() =>
            {

                color.r = greyValue;
                color.g = greyValue;
                color.b = greyValue;

                colorAdjustments.colorFilter.value = color;

            })
        );

        float startExpo = colorAdjustments.postExposure.value;

        sequence.Join(DOTween.To(() => colorAdjustments.postExposure.value, x => colorAdjustments.postExposure.value = x, 0f, 2));


        // ANIMATE TEMPERATURE
        float targetTemperature = 0f;

        sequence.Join(DOTween.To(() => whiteBalance.temperature.value, x => whiteBalance.temperature.value = x, targetTemperature, 3));


        // ANIMATE DIRECTIONAL LIGHT INTENSITY
        float targetIntensity = 52393.4f;
        sequence.Join(DOTween.To(() => hdLight.intensity, x => hdLight.intensity = x, targetIntensity, 2));

        // ANIMATE DIRECTIONAL LIGHT COLOR
        float startKelvinColor = timeChosen == "NIGHT" ? 20000f : 6570f;
        float targetKelvinColor = 8736f;
        float k = startKelvinColor;

        sequence.Join(
            DOTween.To(() => k, x => k = x, targetKelvinColor, 2).OnUpdate(() =>
            {
                hdLight.SetColor(Color.white, k);
            })
        );


        // ANIMATE SUN SIZE AND POS
        float targetSunSize = 0f;
        sequence.Join(DOTween.To(() => hdLight.angularDiameter, x => hdLight.angularDiameter = x, targetSunSize, 3));

        Vector3 targetRotation = new Vector3(33.5f, 123.4f, 0f);
        sequence.Join(baseLight.transform.DORotate(targetRotation, 2));

        timeChosen = "null";

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
        rainEvent.onOpen += startRain;
        baseEvent.onOpen += resetEffects;
    }




    // Update is called once per frame
    void Update()
    {
        spaceRotationLoop();
    }
}
