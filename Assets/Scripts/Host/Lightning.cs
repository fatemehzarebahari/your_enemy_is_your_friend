using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField]
    private float animationSpeed = 1f;

    [SerializeField]
    private float timer = 0, maxTime = 0;
    void Update()
    {
        if (timer > maxTime)
            Destroy(gameObject);
        timer += Time.deltaTime;
    }
}
