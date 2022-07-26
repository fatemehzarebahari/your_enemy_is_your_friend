using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
 
 public class Bat : MonoBehaviour {
     private float timeToChangeDirection;
    public Transform player;
     public GameObject bat;
          public GameObject cloneBat;

     float maxDist=2;
     
 
     public void Start () {
         ChangeDirection();
     }
     
     // Update is called once per frame
     public void Update () {
         timeToChangeDirection -= Time.deltaTime;
 
         if (timeToChangeDirection <= 0) {
             ChangeDirection();
         }
 
         GetComponent<Rigidbody2D>().velocity = transform.up * 2;
         checkLocation();

     }
 
 
 
     private void ChangeDirection() {
         float angle = Random.Range(180f, 360f);
         Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
         Vector3 newUp = quat * Vector3.up;
         newUp.z = 0;
         newUp.Normalize();
         transform.up = newUp;
         timeToChangeDirection = 1.5f;
     }

     private void checkLocation(){
        float dist = Vector3.Distance(player.position, transform.position);

        if(dist > maxDist){
            Destroy(bat);
            cloneBat.GetComponent<BatRespawn>().respawn();
            
        }
     }
 }