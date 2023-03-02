using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;


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
                playerShip.GetComponent<PlayerController>().Init();

                //start enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

                break;

            case GameManagerState.GameOver:
                //Stop enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();

                //Display game over
                GameOverGO.SetActive(true);

                //change game manager state to Opening state after 5 sec
                Invoke("ChangeToOpeningState", 5f);

                break;
        }
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
