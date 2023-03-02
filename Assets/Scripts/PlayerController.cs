using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public AudioSource shot;
    public AudioSource explosionSound;

    public GameObject GameManagerGO;
    public GameObject PlayerBulletGO;
    public GameObject bulletPosition01;
    //public GameObject bulletPosition02;
    public GameObject ExplosionGO;

    public Text LivesUIText;
    const int MaxLives = 3;
    int lives;

    public float speed;

    public void Init()
    {
        lives = MaxLives;

        //update the lives UI text
        LivesUIText.text = lives.ToString();

        //Reset the player position to the center of the screen
        //transform.position = new Vector2(0, 0);

        //set player game object 
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //play audio of bullet
            shot.Play();

            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition01.transform.position;

            //GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            //bullet01.transform.position = bulletPosition02.transform.position;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = 0;

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);

        transform.position = pos;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();

            lives--;
            LivesUIText.text = lives.ToString();
            if(lives == 0)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);

                //Destroy(gameObject);
                gameObject.SetActive(false);
            }   
        }
    }

    void PlayExplosion()
    {
        explosionSound.Play();

        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }
}
