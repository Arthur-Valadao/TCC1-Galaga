using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Parameters

    public static GameController gm;

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI lifeTxt;
    private int life;
    private int score;

    public GameObject myShip;

    #endregion

    private void Awake()
    {
        gm = this;
    }

    public void UpdateScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreTxt.text = score.ToString();
    }
    
    public void UpdateLife(int lifeAmount)
    {
        life = lifeAmount;
        lifeTxt.text = lifeAmount.ToString();
    }

    public int LifeAmount()
    {
        return life;
    }
    
    public void StartGame()
    {
        enemySpawner.spawnLevel = 1;
    }
    
}
