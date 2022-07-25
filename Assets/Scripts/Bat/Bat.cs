using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public int speed = 10;
    public Vector3 direction = Vector3.zero;
    bool running = false;
    void Start()
    {
        
    }

    void Update()
    {
        
        transform.position += direction*speed;
    }
      IEnumerator SetRandomTrajectory(){
        yield return new WaitForSeconds(3);
        direction.x =  Random.Range(-1f,1f);
        direction.y = 0;        

    }
}
