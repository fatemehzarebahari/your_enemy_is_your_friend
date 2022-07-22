using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0f,100f)]
    float moveSpeed = 35f;

    [SerializeField, Range(0f,250f)]
    float dashSpeed = 120f;

    [SerializeField, Range(0.1f,1f)]
    float dashDuration = 0.3f;
    private float currentDashTime = 0f;

    [SerializeField, Range(1f,6f)]
    float dashCooldownDuration = 1f;
    private float currentCooldownTime = 0f;
    private bool isDashing = false;
    
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update(){
        ProcessInputs();
    }

    void FixedUpdate(){
        Move();
        if (isDashing){
            Dash();
        }
        if (currentDashTime >= dashDuration){
            currentDashTime = 0;
            isDashing = false;
            currentCooldownTime += Time.deltaTime;
        }
        if (currentCooldownTime < dashCooldownDuration && currentCooldownTime > 0){
            currentCooldownTime += Time.deltaTime;
        }
        else if (currentCooldownTime >= dashCooldownDuration){
            currentCooldownTime = 0;
        }
    }

    void ProcessInputs(){
        if (!isDashing){
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.y = Input.GetAxisRaw("Vertical");
            moveDirection.Normalize();
        }

        if (currentDashTime < dashDuration && currentCooldownTime == 0 && moveDirection != Vector2.zero && Input.GetKeyDown(KeyCode.Space)){
            isDashing = true;
        }
    }

    void Move(){
        rb.velocity = moveDirection * moveSpeed * Time.deltaTime * 10; 
        if (moveDirection.x > 0) animator.SetTrigger("right");
        else if (moveDirection.x < 0) animator.SetTrigger("left");
        else if (moveDirection.y > 0) animator.SetTrigger("up");
        else if (moveDirection.y < 0) animator.SetTrigger("down");;
        if (rb.velocity.magnitude > 0) animator.SetBool("IsMoving", true);
        else animator.SetBool("IsMoving", false);
    }

    void Dash(){
        rb.velocity = moveDirection * dashSpeed * Time.deltaTime * 10;
        currentDashTime += Time.deltaTime;
    }
}
