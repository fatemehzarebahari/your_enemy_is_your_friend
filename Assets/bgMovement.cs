using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 0f;
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent <Renderer>();   
        Time.timeScale = 0.5f;
    }

    void Update()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0);
        renderer.material.mainTextureOffset = offset;
        
    }
}
