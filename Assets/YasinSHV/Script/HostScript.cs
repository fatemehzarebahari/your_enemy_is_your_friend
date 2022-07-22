using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostScript : MonoBehaviour
{
    [SerializeField]
    private int level = 1, minActiveEnemies = 5;

    private GameObject[] spawnPoints;
    private GameObject[] activeEnemies;

    [SerializeField, Min(0f)]
    private float attackCoolDown = 1f;

    [SerializeField] private GameObject[] enemies;

    private float timer = 0.0f, spawnDelay = 2f, spawnTimer = 0f;

    private void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        if (spawnPoints.Length == 0)
            Debug.Log("Host: There Are No Spawn Points!");
    }

    private void Update()
    {
        activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        //spawn
        if (activeEnemies.Length < minActiveEnemies) 
        {
            if (spawnDelay > spawnTimer)
            {
                int spawnerIndex = Random.Range(0, spawnPoints.Length);
                int Enemy = Random.Range(0, enemies.Length);
                Instantiate(enemies[Enemy], spawnPoints[spawnerIndex].transform.position, Quaternion.identity);
                spawnTimer = 0;
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
