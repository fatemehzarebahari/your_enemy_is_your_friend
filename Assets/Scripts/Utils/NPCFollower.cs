using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCFollower : MonoBehaviour
{
	[SerializeField]
	public Transform target;

	[SerializeField]
	float speed = 30f;
	private float currentSpeed;

	[SerializeField]
	float range;

	[SerializeField]
	private bool getPlayerAsTarget;

	Rigidbody2D rb;
	bool isForwarding = true, isEnabled = true;

	Vector2 player_pos, pet_pos;

	float dist;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		currentSpeed = speed;

		if(getPlayerAsTarget){
			target = GameObject.FindGameObjectWithTag("Player").transform;
			target.gameObject.GetComponent<PlayerManager>().onDeath += disableFollower;
		}
	}


	void Update()
	{
		if (isForwarding && isEnabled)
		{
			dist = (target.position - transform.position).magnitude;

			if (dist > range)
			{
				Vector2 aim_Vector = target.position - transform.position;
				rb.velocity = aim_Vector.normalized * currentSpeed;
			}
			else
			{
				rb.velocity = Vector2.zero;
			}


		}
	}

	public void startForwarding()
	{
		isForwarding = true;
		resetSpeed();
	}

	public void stopForwarding()
	{
		isForwarding = false;
		rb.velocity = Vector2.zero;
	}

	public void disableFollower(){
		isEnabled = false;
	}

	public IEnumerator StopFollowingFor(float duration){
		isForwarding = false;
		yield return new WaitForSeconds(duration);
		isForwarding = true;
	}

	public void setSpeed(float speed){
		currentSpeed = speed;
	}

	public void resetSpeed(){
		currentSpeed = speed;
	}
}
