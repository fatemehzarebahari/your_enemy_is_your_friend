using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    [SerializeField]
    float bulletSpeed = 100f;

    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void shoot(Vector2 aim)
    {
        rb.velocity = aim.normalized * bulletSpeed;
    }

		private void OnCollisionEnter2D(Collision2D other){
			Destroy(gameObject);
		}
}
