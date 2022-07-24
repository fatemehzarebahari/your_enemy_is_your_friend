using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private HostScript host;
    private bool isAiming, isLocked;

    [SerializeField]
    float aimDelay = 1f, lockDelay = 0.5f, aimSpeed = 15f;

    [SerializeField]
    Transform shootPosition;
    private float currentDelay = 0f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

		public void GetKilled(){
        
			Destroy(gameObject);
		}

    void Awake(){
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if (GetComponent<NPCFollower>().player.transform.position.x > transform.position.x){
            spriteRenderer.flipX = false;
            shootPosition.localPosition = new Vector2(0.297f, 0.132f);
        }
        else{
            spriteRenderer.flipX = true;
            shootPosition.localPosition = new Vector2(-0.297f, 0.132f);
        } 
        if (isAiming){
            currentDelay += Time.deltaTime;
            if (currentDelay >= aimDelay){
                if (!isLocked){
                    isLocked = true;
                    GetComponent<ProjectileLuncher>().setLocked();
                    GetComponent<NPCFollower>().stopForwarding();
                    animator.SetTrigger("charge");
                }
                else if (currentDelay >= aimDelay + lockDelay){
                    GetComponent<ProjectileLuncher>().stopAiming();
                    GetComponent<ProjectileLuncher>().shoot();
                    animator.SetTrigger("shoot");
                    currentDelay = 0f;
                    isAiming = false;
                    isLocked = false;
                    GetComponent<NPCFollower>().startForwarding();
                }
            }
        }
    }
    public void IntroduceHost(HostScript host){
        this.host = host;
        OnEnable();
    }

    private void StartAttacking(){
        int rand = Random.Range(1,4);
        if (rand == 3){
            GetComponent<NPCFollower>().setSpeed(aimSpeed);
            GetComponent<ProjectileLuncher>().aiming();
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
}
