using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    GameObject scoreUITextGO; //reference to the text UI gameObject
    public GameObject ExplosionGO;
    float speed;

    void Start()
    {
        speed = 2f;

        //get the score text UI
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        //get enemy current position
        Vector2 position = transform.position;

        //compute the enemy position
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        
        //update the enemy position
        transform.position = position;

        //this is the botton-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //destroy enemy outside the screen
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect colision of the enemy ship with the player ship||bullet
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();

            //add 100 points to the score
            scoreUITextGO.GetComponent<GameScore>().Score += 100;

            //Destroy enemy ship
            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }
}
