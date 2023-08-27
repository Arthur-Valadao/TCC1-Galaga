using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    #region Parameters
    
    [SerializeField] private GameObject scoreUITextGO; //reference to the text UI gameObject
    [SerializeField] private GameObject ExplosionGO;
    [SerializeField] private GameObject Beam;
    private float speed;
    private float timeLive;
    private int count;
    private Vector2 limit;

    private Transform tgtPos;
    private int moveSet;

    private EnemyGun gun;

    private bool boss;
    private float attackDelay = 10f;
    private float delayTimer;

    private bool attacking;

    #endregion
    
    void Start()
    {
        gun = GetComponent<EnemyGun>();
        limit = Camera.main.ViewportToWorldPoint(new Vector2(.5f, .25f));
        delayTimer = attackDelay;
        count = 1;

        speed = 2f;
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    void Update()
    {
        timeLive += Time.deltaTime;
        if(boss && delayTimer < 0)
        {
            if(!attacking)
            {
                count = 1;
                attacking = true;
            }
            BossAttack();
        }
        else if (moveSet == 1) basicWave();
        else if (moveSet == 2) basicWave2Right();
        else if (moveSet == 3) basicWave2Left();
        if(boss && delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
        }
    }
    
    private void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }

    public void IsTheBoss()
    {
        boss = true;
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

    private void basicWave2Left()
    {
        if(count != 0) waves2side(-1);
        else followTgt();
    }

    private void basicWave2Right()
    {
        if(count != 0) waves2side(1);
        else followTgt();
    }
    
    private void waves2side(int side)
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x + (speed * Time.deltaTime * side), position.y + Mathf.Sin(timeLive * 6) * speed * Time.deltaTime);
        transform.position = position;
        
        if (side < 0 && transform.position.x < limit.x) count = 0;
        else if (side > 0 && transform.position.x > limit.x) count = 0;
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
        transform.position = Vector3.Lerp(transform.position, tgtPos.position, .005f);
    }

    private void BossAttack()
    {
        
        if(count != 0) wavesDown();
        else Attack();
    }

    private void Attack()
    {
        Beam.SetActive(true);
        Invoke("DisableAttack",3f);
    }

    private void DisableAttack()
    {
        Beam.SetActive(false);
        delayTimer = attackDelay;
        attacking = false;
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
