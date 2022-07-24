using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSlam : MonoBehaviour
{
	[SerializeField]
	public Transform PetPos;

	private Vector2 Center;

	[SerializeField]
	public float explosionStrength, range = 2, duration = 0.5f;

	void Update() {
		if ( Input.GetKeyDown(KeyCode.LeftShift)){
			Slam();
		}
	}

	public void Slam(){
		var colliders = Physics2D.OverlapCircleAll(transform.position, range);

		if (colliders.Length > 0)
		{            
			for(int i =0 ; i< colliders.Length;i++){

				if(colliders[i].gameObject.tag == "Enemy"){

					if(!colliders[i].GetComponent<EnemyManager>().isAiming)
						{
						StartCoroutine(colliders[i].GetComponent<NPCFollower>().StopFollowingFor(duration));
						Transform t = colliders[i].transform;
						Rigidbody2D rb = colliders[i].GetComponent<Rigidbody2D>();
						Vector2 ForceVec = (t.position - transform.position).normalized * explosionStrength;
						rb.velocity = ForceVec;
						}
				}
			}
		}
	}
}
