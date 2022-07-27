using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private HostScript host;
    public bool isAiming, isLocked, isDead, isGhost;

    [SerializeField]
    float aimDelay = 1f, lockDelay = 0.5f, aimSpeed = 15f;

    [SerializeField]
    Transform shootPosition;

    [SerializeField]
    private float currentDelay = 0f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    private Rigidbody2D rb;

    [SerializeField]
    private AudioSource shootSound, deathSound, poofSound;
    private GameObject camHolder;

    [SerializeField]
    private ParticleSystem deathParticle;

    [SerializeField]
    private GameObject shootFX, deathFX;

	public void GetKilled(){
        isDead = true;
        animator.SetTrigger("fall");
        animator.SetBool("isDead", true);
        GetComponent<NPCFollower>().stopForwarding();
        col.enabled = false;
        deathSound.Play();
        deathParticle.gameObject.SetActive(true);
        deathParticle.Play();
        StartCoroutine(Poof(3.5f));
    }

    public IEnumerator Fall(float duration){
        animator.SetTrigger("fall");
        yield return new WaitForSeconds(duration);
        animator.SetTrigger("unfall");
    }

    void Awake(){
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        camHolder = GameObject.FindGameObjectWithTag("CameraHolder");
    }

    void Update(){
        if (!isDead){
            if (!isAiming){
                if (GetComponent<NPCFollower>().target.transform.position.x > transform.position.x){
                    spriteRenderer.flipX = false;
                    shootPosition.localPosition = new Vector2(0.297f, 0.132f);
                }
                else{
                    spriteRenderer.flipX = true;
                    shootPosition.localPosition = new Vector2(-0.297f, 0.132f);
                } 
            }
            if (isAiming){
                currentDelay += Time.deltaTime;
                if (currentDelay >= aimDelay){
                    if (!isLocked){
                        isLocked = true;
                        GetComponent<ProjectileLuncher>().StartAiming();
                        GetComponent<NPCFollower>().stopForwarding();
                        animator.SetTrigger("charge");
                    }
                    else if (currentDelay >= aimDelay + lockDelay){
                        GetComponent<ProjectileLuncher>().stopAiming();
                        GetComponent<ProjectileLuncher>().Shoot(this.gameObject);
                        Instantiate(shootFX, shootPosition.position, Quaternion.identity);
                        if (shootFX.transform.position.x < 0) shootFX.GetComponent<SpriteRenderer>().flipX = true;
                        animator.SetTrigger("shoot");
                        currentDelay = 0f;
                        isAiming = false;
                        isLocked = false;
                        GetComponent<NPCFollower>().startForwarding();
                        shootSound.Play();
                        StartCoroutine(Shake(0.075f, 0.035f));
                    }
                }
            }
        }
        else if (isGhost){
            if (host.transform.position.x < transform.position.x) spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;
            
            if ((transform.position - host.transform.position).magnitude < 1){
                Instantiate(deathFX, transform.position, Quaternion.identity);
                StartCoroutine(Shake(0.075f, 0.05f));
                deathSound.Play();
                Destroy(gameObject, deathSound.clip.length);
                isGhost = false;
            }
        }
    }
    public void IntroduceHost(HostScript host){
        this.host = host;
        OnEnable();
    }

    private void StartAttacking(){
        if (isDead) return;
        int rand = Random.Range(1,4);
        if (rand == 2){
            GetComponent<NPCFollower>().setSpeed(aimSpeed);
            isAiming = true;
            animator.SetTrigger("aim");
        }
    }

    private void OnEnable(){
        if (host != null){
            host.onAttack += StartAttacking;
        }
    }

    private void OnDisable(){
        if (host != null){
            host.onAttack -= StartAttacking;
        }
    }

    private IEnumerator FallFor(float duration){
		yield return new WaitForSeconds(duration);
		animator.SetTrigger("unfall");
	}

    private IEnumerator Poof(float duration){
        yield return new WaitForSeconds(duration);
        Instantiate(deathFX, transform.position, Quaternion.identity);
        animator.SetBool("isDead", false);
        animator.SetTrigger("unfall");
        spriteRenderer.color = new Color(1f,1f,1f,0.6f);
        NPCFollower npcFollower = GetComponent<NPCFollower>();
        npcFollower.target = host.transform;
        npcFollower.startForwarding();
        npcFollower.setSpeed(5);
        poofSound.Play();
        isGhost = true;
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
