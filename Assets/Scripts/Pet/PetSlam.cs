using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSlam : MonoBehaviour
{
    [SerializeField]
    public Transform PetPos;

    private Vector2 Center;

    [SerializeField, Range(50f,250f)]
    public float explosionStrength ;

     void Awake(){
        Center =   PetPos.position;
        explosionStrength = 50f;
    }


    void Update() {
        if ( Input.GetKeyDown(KeyCode.LeftShift)){

            Slam();
        }
    }

     public  void Slam(){
        var colliders = Physics2D.OverlapCircleAll(Center, 10f);
        Debug.Log(colliders.Length);
        if (colliders.Length > 0)
        {            
            for(int i =0 ; i< colliders.Length;i++){
                Debug.Log(colliders[i].gameObject.tag);

                if(colliders[i].gameObject.tag == "Enemy"){
                    Debug.Log("Enemy detected");
                    colliders[i].GetComponent<NPCFollower>().stopForwarding();
                    Vector2 ForceVec = -colliders[i].GetComponent<Rigidbody2D>().velocity.normalized * explosionStrength;
                    colliders[i].GetComponent<Rigidbody2D>().AddForce(ForceVec);
                }
            }
            
        }
    }
}
