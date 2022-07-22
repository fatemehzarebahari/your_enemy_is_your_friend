using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostScript : MonoBehaviour
{
    [SerializeField]
    private int level = 1, minActiveEnemies = 5;

    private GameObject[] spawnPoints, activeEnemies;

    [SerializeField, Min(0f)]
    private float attackCoolDown = 1f;

    [SerializeField] private GameObject[] enemies;

    private float timer = 0.0f, spawnDelay = 0.3f, spawnTimer = 0f;
    private int[] blackList;
    private int spawnCounter = 1;

    private void Awake()
    {
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
        for(int i = 0; i < activeEnemies.Length; i++)
            //Attack

        if (timer > attackCoolDown)
        {
           
            timer = 0;
        }
        timer += Time.deltaTime;

    }

}
