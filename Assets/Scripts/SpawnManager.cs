using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController playerScript;
    public GameObject[] viruses;
    public GameObject[] powerups;
    private int initVirusNum = 10;
    private int initPowerupNum = 2;
    private int infectedNum = 0;
    private int newInfectedNum = 0;

    private int availablePowerups = 2;
    public int addedPowerups = 0;

    private float zSpawnBound = 23;
    private float xSpawnBound = 23;
    private float ySpawn = 0.75f;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = playerScriptInit();
        spawnObjectInitializer(viruses, initVirusNum);
        spawnObjectInitializer(powerups, initPowerupNum);
    }

    // Update is called once per frame
    void Update()
    {
        updatePowerup(playerScript.availablePowerups);
        virusProliferate();
    }

    private void spawnObject(GameObject[] objects)
    {
        int randomIdx = ranIdx(objects);
        Vector3 spawnPos = ranPos();
        Instantiate(objects[randomIdx], spawnPos, objects[randomIdx].gameObject.transform.rotation);
    }

    private Vector3 ranPos()
    {
        float randomX = Random.Range(-xSpawnBound, xSpawnBound);
        float randomZ = Random.Range(-zSpawnBound, zSpawnBound);
        return new Vector3(randomX, ySpawn, randomZ);
    }

    private int ranIdx(GameObject[] objects)
    {
        return Random.Range(0, objects.Length);
    }

    private void spawnObjectInitializer(GameObject[] objects, int num)
    {
        for (int i = 0; i < num; i++)
        {
            spawnObject(objects);
        }
    }

    private void virusProliferate()
    {
        newInfectedNum = GameObject.Find("Player").GetComponent<PlayerController>().infectedNum;
        if (newInfectedNum > infectedNum)
        {
            spawnObjectInitializer(viruses, newInfectedNum - infectedNum);
            infectedNum = newInfectedNum;
        }
    }

    private void updatePowerup(int availablePowerupNum){
        spawnObjectInitializer(powerups, initPowerupNum-availablePowerupNum);
        availablePowerups = initPowerupNum;
        addedPowerups = initPowerupNum-availablePowerupNum;
    }

    private PlayerController playerScriptInit(){
        GameObject player = GameObject.Find("Player");
        return player.GetComponent<PlayerController>();
    }



}
