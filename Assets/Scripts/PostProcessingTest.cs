using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingTest : MonoBehaviour
{
    public Volume postProcessVolume;
    private float intensityValue = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnDamage();
        }
    }

    private void OnDamage()
    {
        if (postProcessVolume.profile.TryGet(out Vignette vignette))
        {
            intensityValue += 0.2f;
            intensityValue = Mathf.Clamp(intensityValue, 0, 0.6f);
            vignette.intensity.value = intensityValue;
        }
    }
}
