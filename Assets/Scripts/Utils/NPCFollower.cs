using UnityEngine;


public class NPCFollower : MonoBehaviour
{
	[SerializeField]
	public Transform player;

	[SerializeField]
	float speed = 30f;
	private float currentSpeed;

	[SerializeField]
	float range;

	[SerializeField]
	private bool getPlayerAsTarget;

	Rigidbody2D rb;
	bool isForwarding = true;

	Vector2 player_pos, pet_pos;

	float dist;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		currentSpeed = speed;

		if(getPlayerAsTarget){
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}


	void Update()
	{
		if (isForwarding)
		{
			pet_pos = rb.position;
			player_pos = player.position;
			dist = (player_pos - pet_pos).magnitude;

			if (dist > range)
			{
				Vector2 aim_Vector = player_pos - pet_pos;
				rb.velocity = aim_Vector.normalized * currentSpeed * Time.deltaTime * 50;
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

	public void setSpeed(float speed){
		currentSpeed = speed;
	}

	public void resetSpeed(){
		currentSpeed = speed;
	}
}
