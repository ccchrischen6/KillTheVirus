using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject powerup;

    private float zEnemySpawn = 12;
    private float xSpawnBound = 16;
    private float ySpawn = 0.75f;
    private float zPowerupRange = 5;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy() {
        float randomX = Random.Range(-xSpawnBound, xSpawnBound);
        int randomIndex = Random.Range(0, enemies.Length);
        Vector3 spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);

        Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
    }

    void SpawnPowerup()
    {
        float randomX = Random.Range(-xSpawnBound, xSpawnBound);
        float randoxZ = Random.Range(-zEnemySpawn, zEnemySpawn);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, randoxZ);

        Instantiate(powerup, spawnPos, powerup.transform.rotation);

    }

   
}
