using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    #region Parameters
    
    [SerializeField] private GameObject scoreUITextGO; //reference to the text UI gameObject
    [SerializeField] private GameObject ExplosionGO;
    private float speed;
    private float timeLive;
    private int count;
    private Vector2 limit;

    private Transform tgtPos;
    private int moveSet;

    #endregion
    
    void Start()
    {
        limit = Camera.main.ViewportToWorldPoint(new Vector2(.5f, .25f));
        count = 1;

        speed = 2f;
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    void Update()
    {
        timeLive += Time.deltaTime;
        if (moveSet == 1) basicWave();
    }
    
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }

    public void SetTgtPos(Transform tgt)
    {
        tgtPos = tgt;
    }

    public void SetMoveSet(int newMoveSet)
    {
        moveSet = newMoveSet;
    }
    
    #region movementPatterns

    private void basicWave()
    {
        if(count != 0) wavesDown();
        else followTgt();
    }
    
    private void wavesDown()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x+ Mathf.Sin(timeLive * 6) * speed * Time.deltaTime, position.y - speed * Time.deltaTime);
        transform.position = position;
        if (transform.position.y <= limit.y) count = 0;
    }

    private void followTgt()
    {
        //Vector2 position = (tgtPos.position - transform.position)*(speed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, tgtPos.position, .005f);
    }
    
    #endregion

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
}
