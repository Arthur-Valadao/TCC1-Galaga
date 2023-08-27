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

    private bool top = true;
    private bool right = false;

    [SerializeField] private Transform enemyFormation1;
    
    private int currentFormation;

    public int spawnLevel;
    public bool canSpawn;

    private int bossInitCount = 4; //default = 8
    private int bossEndCount = 7; //default = 11

    #endregion

    void Start()
    {
         spawnLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.4f)) + Vector3.left;
         spawnRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.4f)) + Vector3.right;
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
        anEnemy.GetComponent<EnemyControl>().SetMoveSet(moveSet);
        if(currentFormation <= bossEndCount &&  currentFormation >= bossInitCount) anEnemy.GetComponent<EnemyControl>().IsTheBoss();
        
        currentFormation++;
        anEnemy.transform.position = spawnPoint;
        anEnemy.transform.parent = gameObject.transform;

    }

    void wave1()
    {
        
        if (canSpawn && currentFormation < enemyFormation1.childCount)
        {
            if(top && !right) SpawnEnemy(spawnTopLeft,1);
            else if(top && right) SpawnEnemy(spawnTopRight,1);
            else if(!top && !right) SpawnEnemy(spawnLeft,2);
            else if(!top && right) SpawnEnemy(spawnRight,3);
        }

        if (currentFormation % 8 == 0)
        {
            top = !top;
            Invoke("wave1",1.5f);
        }
        else if (currentFormation % 4 == 0)
        {
            right = !right;
            Invoke("wave1",1.5f);
        }
        else if (canSpawn && currentFormation < enemyFormation1.childCount) Invoke("wave1",.5f);
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
