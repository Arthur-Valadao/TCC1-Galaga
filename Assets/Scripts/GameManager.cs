using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject enemySpawner;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject playerPrefab;

    public Text livesUIText; //ref lives here 
    public GameObject playerStart;
    private GameObject _player;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMState;

    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                //Hide game over
                GameOverGO.SetActive(false);

                //Set play button visible(active)
                playButton.SetActive(true);

                break;

            case GameManagerState.Gameplay:
                //Reset the score
                scoreUITextGO.GetComponent<GameScore>().Score = 0;

                //hide gameplay button 
                playButton.SetActive(false);

                //set the player visible(active) and init the player lives
                /*
                _player = Instantiate(playerPrefab, playerStart.transform.position, quaternion.identity);
                _player.GetComponent<PlayerController>().PlayerDeathEvent += OnPlayerDeath;
                _player.GetComponent<PlayerController>().PlayerDamageEvent += OnPlayerDamage;
                _player.GetComponent<PlayerController>().Init();
                livesUIText.text = _player.GetComponent<PlayerController>().MaxLives.ToString();
                */


                //start enemy spawner

                break;
            
        }
    }

    public void OnPlayerDeath(PlayerController player)
    {
        //Stop enemy spawner

        //Display game over
        GameOverGO.SetActive(true);

        //change game manager state to Opening state after 5 sec
        Invoke("ChangeToOpeningState", 5f);
    }
    
    public void OnPlayerDamage(PlayerController player, int lives)
    {
        livesUIText.text = lives.ToString();
    }
    
    //set the game manager state
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    //Begin button call
    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    //change game manager state to opening state
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
