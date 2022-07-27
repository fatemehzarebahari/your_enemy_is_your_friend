using UnityEngine;
using UnityEngine.Events;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] AudioSource walkingSoundEffect;
    [SerializeField, Range(0f,100f)]
    float moveSpeed = 35f;
    
    private Rigidbody2D rb;
    private Animator animator;

    private bool playEvent = false;

    private GameObject camHolder;

    private float timer = 0, maxTime = 2f , newMaxTime;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        camHolder = GameObject.FindGameObjectWithTag("CameraHolder");
        newMaxTime = maxTime;
    }

    void FixedUpdate(){
        if (GetComponent<PlayerManager>().isAlive)
            Move();
        else{
            rb.velocity = new Vector2(0, 0); 
            walkingSoundEffect.Stop();
            playEvent = false;
        }

        if (timer > newMaxTime)
        {
            StartCoroutine(Shake(0.7f, 0.03f));
            timer = 0;
            newMaxTime = Random.Range(maxTime, maxTime + 4);
        }
        timer += Time.deltaTime;
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
            walkingSoundEffect.volume = 0.05f;
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
