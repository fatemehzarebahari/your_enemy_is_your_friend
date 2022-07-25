using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] ParticleSystem whooshParticle;
    [SerializeField] AudioSource whooshAudio;
    [SerializeField, Range(0f,250f)]
    float dashSpeed = 120f;

    [SerializeField, Range(0f,1f)]
    float dashDuration = 0.25f;
    private float currentDashTime = 0f;

    [SerializeField, Range(0.001f,3f)]
    float dashCooldownDuration = 1f;
    private float currentCooldownTime = 0f;

    private Vector2 dashDirection;

    [HideInInspector]
    public bool isDashing = false;

    private Rigidbody2D rb;

    private GameObject camHolder;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        camHolder = GameObject.FindGameObjectWithTag("CameraHolder");

    }
    void Update(){
        if (currentDashTime < dashDuration && currentCooldownTime == 0 &&
                GetComponent<PlayerInputManager>().moveDirection != Vector2.zero && GetComponent<PlayerInputManager>().dashPressed){
            isDashing = true;

            Activateeffect();
            dashDirection = GetComponent<PlayerInputManager>().moveDirection;
        }
    }

    void FixedUpdate(){
        if (isDashing){
            Dash();
        }
        else
        {
            DeActivateEffect();
        }
        if (currentDashTime >= dashDuration){
            currentDashTime = 0;
            isDashing = false;
            currentCooldownTime += Time.deltaTime;
            //transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (currentCooldownTime < dashCooldownDuration && currentCooldownTime > 0){
            currentCooldownTime += Time.deltaTime;
        }
        else if (currentCooldownTime >= dashCooldownDuration){
            currentCooldownTime = 0;
        }
    }

    void Dash(){

        rb.velocity = dashDirection * dashSpeed * Time.deltaTime * 10;
        currentDashTime += Time.deltaTime;
    }

    void Activateeffect()
    {
        whooshAudio.Play();
        whooshParticle.Play();
        StartCoroutine(Shake(0.1f, 0.1f));
    }
    void DeActivateEffect()
    {
        whooshParticle.Stop();
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 origin = camHolder.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            camHolder.transform.localPosition = new Vector3(x, y, origin.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        camHolder.transform.localPosition = origin;
    }
}
