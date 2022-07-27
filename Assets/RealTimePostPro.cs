using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RealTimePostPro : MonoBehaviour
{
    private Volume volume;
    private ColorAdjustments CA;

    float timer = 0, maxTime, newMaxTime;
    private bool hasTint = false;
    private void Start()
    {
        maxTime = Random.Range(7,11);
        newMaxTime = maxTime;
       volume = GetComponent<Volume>();
    }
    void Update()
    {
        if (timer > newMaxTime)
        {
            if (!hasTint)
            {
                if (volume.profile.TryGet<ColorAdjustments>(out CA))
                {
                    CA.hueShift.value = Random.Range(-100, -65);
                    CA.contrast.value = 100;
                }
                maxTime = Random.RandomRange(2, 5);
                hasTint = true;
            }
            else 
            {
                if (volume.profile.TryGet<ColorAdjustments>(out CA))
                {
                    CA.hueShift.value = 0;
                    CA.contrast.value = 27;
                    maxTime = Random.RandomRange(maxTime, maxTime + 8);
                }
                hasTint = false;
            }
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
