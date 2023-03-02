using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO;
    float maxSpawnRateInSeconds = 5f;

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextEnemySpawn();
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
        Invoke("SpawnEnemy", spawnInNSeconds);
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
