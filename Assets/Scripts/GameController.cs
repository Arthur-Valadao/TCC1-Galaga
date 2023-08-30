using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region Parameters

    public static GameController gm;

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI lifeTxt;
    [SerializeField] private TextMeshProUGUI HighScore;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject buttonRestart;
    
    public bool canRespawn;
    private int life;
    private int score;
    [SerializeField] private int scoreNeedToHeal;

    public bool canGalagaAttack;
    public GameObject myShip;

    #endregion

    private void Awake()
    {
        gm = this;
        canRespawn = true;
        canGalagaAttack = true;
        
        HighScore.text = PlayerPrefs.GetInt("score", 0).ToString();
    }

    public void UpdateScore(int scoreAmount)
    {
        score += scoreAmount;
        
        if(score % scoreNeedToHeal == 0) myShip.GetComponent<LifeSystem>().LifeUp(1);
        
        scoreTxt.text = score.ToString();
        UpdateHighscore();
    }

    private void UpdateHighscore()
    {
        int currentHighscore = PlayerPrefs.GetInt("score",0);
        if(score > currentHighscore)
        {
            HighScore.text = score.ToString();
            PlayerPrefs.SetInt("score", score);
        }
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

    public void GameOver()
    {
        canRespawn = false;
        gameOver.SetActive(true);
        buttonRestart.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Galaga");
    }
    
}
