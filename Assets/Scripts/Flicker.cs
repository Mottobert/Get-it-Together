using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    [SerializeField]
    private Light flickerLight;
    [SerializeField]
    private float flickerMin;
    [SerializeField]
    private float flickerMax;

    // Update is called once per frame
    void Update()
    {
        float noiseAmount = Mathf.PerlinNoise(0, Time.frameCount / 10);
        float flickerAmount = Remap(noiseAmount, 0f, 1f, flickerMin, flickerMax);

        flickerLight.intensity = flickerAmount;
    }

    private float Remap(float val, float in1, float in2, float out1, float out2)
    {
        return out1 + (val - in1) * (out2 - out1) / (in2 - in1);
    }
}



