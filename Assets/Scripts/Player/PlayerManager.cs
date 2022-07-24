using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public bool isAlive;
private Animator animator;

private void Awake(){
    animator = GetComponent<Animator>();

}


// void Update(){
//     if(isAlive==false){
//         animator.SetTrigger("staydead");

//     }
// }
  public void OnCollisionEnter2D(Collision2D collision){
    if(collision.gameObject.tag=="Bullets"){
        Debug.Log("Bullets detected");
        isAlive=true;
        // animator.SetBool("die",true);
        animator.SetTrigger("staydead");


    }
  }
    
}
