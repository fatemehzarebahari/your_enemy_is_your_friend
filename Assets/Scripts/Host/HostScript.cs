using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;
public class HostScript : MonoBehaviour
{
    public event Action onAttack;
    [SerializeField]
    private int level = 1, minActiveEnemies = 5;

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

    private void Awake()
    {
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
            int damage = lastActiveEnemies - activeEnemies.Length;
            healthbar.value -= damage * damgeTakenMultiplier;
        }
        lastActiveEnemies = activeEnemies.Length;
        //EO Dameage

        //Spawn
        if (activeEnemies.Length < minActiveEnemies + level) 
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

    IEnumerator SpawnEnmies() 
    {
        while (activeEnemies.Length < minActiveEnemies)
        {
            yield return new WaitForSeconds(spawnDelay);
            Spawn();
        }
    }

    private void Spawn()
    {
            spawnCounter++;
        if (spawnCounter <= blackList.Length + 1)
        {
            explosionSound.Play();
            int spawnerIndex = Random.Range(0, spawnPoints.Length);
            while (blackList[spawnerIndex] != 0)
                spawnerIndex = Random.Range(0, spawnPoints.Length);
            blackList[spawnerIndex] = spawnerIndex + 1;
            int Enemy = Random.Range(0, enemies.Length);
            Instantiate(lightning, (spawnPoints[spawnerIndex].transform.position), Quaternion.identity);
            GameObject enemy = Instantiate(enemies[Enemy], spawnPoints[spawnerIndex].transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyManager>().IntroduceHost(this);
        }
    }


    private void playLanding() 
    {
        // landingSound.Play();
    }

}
