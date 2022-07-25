using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    [SerializeField]
    float bulletSpeed = 100f;
    Rigidbody2D rb;

    [HideInInspector]
    public GameObject shooter;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void shoot(Vector2 aim, GameObject shooter)
    {
        rb.velocity = aim.normalized * bulletSpeed;
        this.shooter = shooter;
    }

	private void OnCollisionEnter2D(Collision2D other){
		Destroy(gameObject);
	}
}
