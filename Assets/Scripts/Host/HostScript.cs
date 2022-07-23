using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostScript : MonoBehaviour
{
    [SerializeField]
    private int level = 1, minActiveEnemies = 5;

    private GameObject[] spawnPoints, activeEnemies;

    [SerializeField, Min(0f)]
    private float attackCoolDown = 1f, spawnDelay = 0.3f;

    [SerializeField] private GameObject[] enemies;

    private float timer = 0.0f, spawnTimer = 0f, newAttackCoolDown = 0f;
    private int[] blackList;
    private int spawnCounter = 1;

    [Header("Effects")]
    [SerializeField]
    private GameObject lightning;

    private void Awake()
    {
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
        //spawn
        if (activeEnemies.Length < minActiveEnemies) 
        {
            if (spawnDelay < spawnTimer)
            {
                int spawnerIndex = Random.Range(0, spawnPoints.Length);
                while(blackList[spawnerIndex] != 0)
                    spawnerIndex = Random.Range(0, spawnPoints.Length);
                blackList[spawnerIndex] = spawnerIndex + 1;
                int Enemy = Random.Range(0, enemies.Length);
                Instantiate(lightning, (spawnPoints[spawnerIndex].transform.position), Quaternion.identity);
                Instantiate(enemies[Enemy], spawnPoints[spawnerIndex].transform.position, Quaternion.identity);
                spawnTimer = 0;
                spawnCounter++;
            }
            if (spawnCounter > spawnPoints.Length || activeEnemies.Length >= minActiveEnemies) 
            {
                spawnCounter = 1;
                for (int i = 0; i < spawnPoints.Length; i++)
                    blackList[i] = 0;
            }
            spawnTimer += Time.deltaTime;
        }


        //Attack

        if (timer > newAttackCoolDown)
        {
            for (int i = 0; i < activeEnemies.Length; i++)
            {
              int shouldAttack = Random.Range(1, 4);
              if (shouldAttack == 3)
              {
                print("Enemy (" + i + ") Attacked");
              }
            }   
            
            timer = 0;
            newAttackCoolDown = Random.Range(attackCoolDown - 2, attackCoolDown);
        }
        timer += Time.deltaTime;

    }

}
