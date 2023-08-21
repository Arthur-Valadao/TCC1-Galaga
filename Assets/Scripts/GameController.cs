using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Parameters

    public static GameController gm;

    [SerializeField] private EnemySpawner enemySpawner;

    #endregion

    private void Awake()
    {
        gm = this;
    }

    public void StartGame()
    {
        enemySpawner.spawnLevel = 1;
        
    }
}
