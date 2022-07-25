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
        walkingSoundEffect.volume = 0;
        walkingSoundEffect.Play();
    }

    void FixedUpdate(){
        if (GetComponent<PlayerManager>().isAlive)
            Move();
        else rb.velocity = new Vector2(0, 0); 
    }

    void Move() {
        var moveDirection = GetComponent<PlayerInputManager>().moveDirection;
        if (!GetComponent<PlayerDash>().isDashing)
        {
            rb.velocity = moveDirection * moveSpeed * Time.deltaTime * 10;
        }
        if ((moveDirection.x != 0 || moveDirection.y != 0) && !playEvent)
        {
            walkingSoundEffect.volume = 100;
            playEvent = true;
        }
        if (moveDirection.x == 0 && moveDirection.y == 0 && playEvent)
        {
            walkingSoundEffect.volume = 0;
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
