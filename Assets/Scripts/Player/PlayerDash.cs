using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{   
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
    private Animator animator;


    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
    void Update(){
        if (currentDashTime < dashDuration && currentCooldownTime == 0 &&
                GetComponent<PlayerInputManager>().moveDirection != Vector2.zero && GetComponent<PlayerInputManager>().dashPressed){
            isDashing = true;
            dashDirection = GetComponent<PlayerInputManager>().moveDirection;
        }
    }

    void FixedUpdate(){
        if (isDashing){
            Dash();

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
        animator.SetTrigger("windEffect");
    }
}
