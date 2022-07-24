using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLuncher : MonoBehaviour
{
	[SerializeField]
	LineRenderer line;

	[SerializeField]
	GameObject bulletPrefab;

	[SerializeField]
	Transform shoothingStartPosition;

	[SerializeField]
	float aimingLength = 1;

	bool aim = false;
	Transform player;
	Vector2 target_position;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		line.enabled = false;
	}

	void Update()
	{
		if (aim)
		{
			aim_line();
			line.enabled = true;
		}
		else
			line.enabled = false;
	}

	void aim_line()
	{
		Vector3[] position = new Vector3[3] { shoothingStartPosition.position, target_position, new Vector3(0, 0, 0) };
		line.SetPositions(position);
	}

	public void Shoot()
	{
		Vector2 aim = target_position - (Vector2)shoothingStartPosition.position;
		GameObject bullet_Obj = Instantiate(bulletPrefab, shoothingStartPosition.position, new Quaternion());
		bullet_Obj.GetComponent<bullet>().shoot(aim);
	}

	public void StartAiming()
	{
		aim = true;
		target_position = player.position + (player.position - shoothingStartPosition.position) * aimingLength;
	}

	public void stopAiming()
	{
		aim = false;
	}
}
