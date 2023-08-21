using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    #region Parameters

    
    [SerializeField] private GameObject EnemyGO;
    private float maxSpawnRateInSeconds = 1f;

    private Vector3 spawnTopLeft;
    private Vector3 spawnTopRight;
    private Vector3 spawnRight;
    private Vector3 spawnLeft;

    [SerializeField] private Transform enemyFormation1;
    private int currentFormation;

    public int spawnLevel;
    public bool canSpawn;

    #endregion

    void Start()
    {
         spawnLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.25f)) + Vector3.left;
         spawnRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.25f)) + Vector3.right;
         spawnTopLeft = Camera.main.ViewportToWorldPoint(new Vector2(0.25f, 1)) + Vector3.up;
         spawnTopRight = Camera.main.ViewportToWorldPoint(new Vector2(0.75f, 1)) + Vector3.up;
    }

    private void FixedUpdate()
    {
        if (spawnLevel == 1)
        {
            spawnLevel = 0;
            canSpawn = true;

            wave1();
        }
    }

    void SpawnEnemy(Vector3 spawnPoint, int moveSet)
    {
        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.GetComponent<EnemyControl>().SetTgtPos(enemyFormation1.GetChild(currentFormation));
        anEnemy.GetComponent<EnemyControl>().SetMoveSet(1);
        currentFormation++;
        anEnemy.transform.position = spawnPoint;
        
        Invoke("wave1",.5f);
    }

    void wave1()
    {
        
        if (canSpawn && currentFormation < enemyFormation1.childCount)
        {
            SpawnEnemy(spawnTopLeft,1);
        }
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

    
}
