using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTuner : MonoBehaviour
{
    private AudioSource ad;
    private void Awake()
    {
        ad = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Time.timeScale == 0)
        {
            ad.pitch = 0.8f;
        }
    }
}
