using UnityEngine;


public class NPCFollower : MonoBehaviour
{
	[SerializeField]
	Transform player;

	[SerializeField]
	float speed = 3f;

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
		
		if(getPlayerAsTarget){
			player = GameObject.FindGameObjectWithTag("player").transform;
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
				rb.velocity = aim_Vector.normalized * speed;
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
	}
	public void stopForwarding()
	{
		isForwarding = false;
	}

}
