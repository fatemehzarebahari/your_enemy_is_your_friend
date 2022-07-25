using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXDestoryer : MonoBehaviour
{
    private Animator animator;
    // [SerializeField]
    // float delay;
    void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
