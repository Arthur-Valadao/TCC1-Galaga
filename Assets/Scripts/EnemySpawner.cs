using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO;
    float maxSpawnRateInSeconds = 1f;

    void Start()
    {
        wave1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 spawnLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.25f));
        Vector2 spawnRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.25f));
        Vector2 spawnTopLeft = Camera.main.ViewportToWorldPoint(new Vector2(0.25f, 1));
        Vector2 spawnTopRight = Camera.main.ViewportToWorldPoint(new Vector2(0.75f, 1));


        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.transform.position = spawnTopLeft;
        // anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y); 
        
        // ScheduleNextEnemySpawn();
    }

    void wave1()
    {
        //Spawnar 8 naves, 4 em cada lado do TOP
        Invoke("spawnTopLeft", 0.5f);
        Invoke("spawnTopLeft", 1);
        Invoke("spawnTopLeft", 1.5f);
        Invoke("spawnTopLeft", 2);
    }

    void spawnTopLeft()
    {
        Vector2 spawnTopLeft = Camera.main.ViewportToWorldPoint(new Vector2(0.25f, 1));

        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.transform.position = spawnTopLeft;

    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds;
        if(maxSpawnRateInSeconds > 1f)
        {
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
        {
            spawnInNSeconds = 1f;
        }
        Invoke("SpawnEnemy", 0.5f);
    }

    //increase the dificulty of the game
    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;
        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    //start enemy spawner
    public void ScheduleEnemySpawner()
    {
        //reset max spawn rate
        maxSpawnRateInSeconds = 5f;

        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        //increase spawn rate every 30 sec
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    //stop enemy spawner
    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
