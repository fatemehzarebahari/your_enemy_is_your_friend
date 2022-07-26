using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Random = UnityEngine.Random;
public class HostScript : MonoBehaviour
{
	public event Action onAttack;
	[SerializeField]
	private int level = 1;

	[Header("Host Control")]
	[SerializeField, Min(2)]
	private int minActiveEnemies = 5;
	[SerializeField, Min(0)]
	private int spawnRate = 5;

	private GameObject[] spawnPoints, activeEnemies;

	[SerializeField, Min(0f)]
	private float hostHealth = 100f, damgeTakenMultiplier = 5f, attackCoolDown = 1f, spawnDelay = 0.3f;

	[SerializeField] private GameObject[] enemies;
	[SerializeField] private Slider healthbar;

	private float timer = 0.0f, spawnTimer = 0f, newAttackCoolDown = 0f;
	private int[] blackList;
	private int spawnCounter = 1, lastActiveEnemies = 0;

	[Header("Effects")]
	[SerializeField]
	private GameObject lightning;
	[SerializeField]
	private AudioSource explosionSound, landingSound;

	private Animator animator;
	private Vector3 startingPosition;

	private void Awake()
	{
		startingPosition = transform.position;
		animator = GetComponent<Animator>();

		healthbar.maxValue = hostHealth;
		healthbar.value = hostHealth;

		newAttackCoolDown = attackCoolDown;

		spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
		blackList = new int[spawnPoints.Length];
		for (int i = 0; i < spawnPoints.Length; i++)
			blackList[i] = 0;

		if (spawnPoints.Length == 0)
			Debug.Log("Host: There Are No Spawn Points!");
	}

	private void Update()
	{
		activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");


		//Take Damage
		if (activeEnemies.Length < lastActiveEnemies)
		{
			animator.SetTrigger("GetHurt");
			int damage = lastActiveEnemies - activeEnemies.Length;
			healthbar.value -= damage * damgeTakenMultiplier;
			if (healthbar.value <= 0)
				onWin();
		}
		lastActiveEnemies = activeEnemies.Length;
		//EO Dameage

		//Spawn
		if (activeEnemies.Length < minActiveEnemies + level + Random.Range(0, spawnRate + 1)) 
		{
			animator.SetTrigger("SpawnEnemy");


			if (spawnCounter > spawnPoints.Length || activeEnemies.Length >= minActiveEnemies) 
			{
				spawnCounter = 1;
				for (int i = 0; i < spawnPoints.Length; i++)
					blackList[i] = 0;
			}
		}
		//EO Spawn


		//Attack
		if (timer > newAttackCoolDown)
		{
			onAttack?.Invoke(); 

			timer = 0;
			newAttackCoolDown = Random.Range(attackCoolDown - 2, attackCoolDown);
		}
		timer += Time.deltaTime;

		//EO Attack
	}


	private void Spawn()
	{
		spawnCounter++;
		if (spawnCounter < blackList.Length + Random.Range(0,2))
		{
			explosionSound.Play();
			int spawnerIndex = Random.Range(0, spawnPoints.Length);
			while (blackList[spawnerIndex] != 0)
				spawnerIndex = Random.Range(0, spawnPoints.Length);
			blackList[spawnerIndex] = spawnerIndex + 1;
			int Enemy = Random.Range(0, enemies.Length);
			transform.position = spawnPoints[spawnerIndex].transform.position;
			Instantiate(lightning, (spawnPoints[spawnerIndex].transform.position), Quaternion.identity);
			GameObject enemy = Instantiate(enemies[Enemy], spawnPoints[spawnerIndex].transform.position, Quaternion.identity);
			enemy.GetComponent<EnemyManager>().IntroduceHost(this);
		}
		else{
			transform.position = startingPosition;
		}
	}


	private void playLanding() 
	{
		 landingSound.Play();
	}


	private void onWin() 
	{
		print("YOU WON!");
	}

}
