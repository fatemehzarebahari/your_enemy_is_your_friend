// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Bat : MonoBehaviour
// {
//     public int speed = 10;
//     public Vector3 direction = Vector3.zero;
//     bool running = false;
//     public Vector3 destination;
//     Rigidbody2D rb;
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();   
//     }

//     void Update()
//     {
//             StartCoroutine(SetRandomTrajectory());
        

//         rb.velocity +=  new Vector2 (direction.x,direction.y)*Time.deltaTime; 
//    }
//       IEnumerator SetRandomTrajectory(){
//         running = true;
//         yield return new WaitForSeconds(3);
//         direction.x =  Random.Range(-1f,1f);
//         direction.y =  Random.Range(-1f,1f);    
//         running = false;
    

//     }
// }
