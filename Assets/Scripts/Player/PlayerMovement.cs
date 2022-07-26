using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] AudioSource walkingSoundEffect;
    [SerializeField, Range(0f,100f)]
    float moveSpeed = 35f;
    
    private Rigidbody2D rb;
    private Animator animator;

    private bool playEvent = false;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate(){
        if (GetComponent<PlayerManager>().isAlive)
            Move();
        else{
            rb.velocity = new Vector2(0, 0); 
            walkingSoundEffect.Stop();
            playEvent = false;
        } 
    }

    void Move() {
        var moveDirection = GetComponent<PlayerInputManager>().moveDirection;
        if (!GetComponent<PlayerDash>().isDashing)
        {
            rb.velocity = moveDirection * moveSpeed * Time.deltaTime * 10;
        }
        if ((moveDirection.x != 0 || moveDirection.y != 0) && !playEvent)
        {
            walkingSoundEffect.Play();
            playEvent = true;
        }
        if (moveDirection.x == 0 && moveDirection.y == 0 && playEvent)
        {
            walkingSoundEffect.Stop();
            playEvent = false;
        }
        if (moveDirection.x > 0) 
            animator.SetTrigger("right");
        else if (moveDirection.x < 0) 
            animator.SetTrigger("left");
        else if (moveDirection.y > 0) 
            animator.SetTrigger("up");
        else if (moveDirection.y < 0) 
            animator.SetTrigger("down");;
        if (rb.velocity.magnitude > 0)
            animator.SetBool("IsMoving", true);
        else
            animator.SetBool("IsMoving", false);
    }
}
