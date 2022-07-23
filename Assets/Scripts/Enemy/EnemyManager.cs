using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private HostScript host;
    private bool isAiming, isLocked;

    [SerializeField]
    float aimDelay = 1f, lockDelay = 0.5f, aimSpeed = 15f;
    private float currentDelay = 0f;

    void Update(){
        if (isAiming){
            currentDelay += Time.deltaTime;
            if (currentDelay >= aimDelay){
                if (!isLocked){
                    isLocked = true;
                    GetComponent<ProjectileLuncher>().setLocked();
                    GetComponent<NPCFollower>().stopForwarding();
                }
                else if (currentDelay >= aimDelay + lockDelay){
                    GetComponent<ProjectileLuncher>().stopAiming();
                    GetComponent<ProjectileLuncher>().shoot();
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
