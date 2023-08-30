using System;
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

    private Beam beam;
    private EnemyGun gun;

    private bool boss;
    private float attackDelay = 10f;
    private float delayTimer;

    private bool attacking;

    #endregion
    
    void Start()
    {
        beam = Beam.GetComponent<Beam>();
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
        if(boss && delayTimer > 0 && GameController.gm.canGalagaAttack)
        {
            delayTimer -= Time.deltaTime;
        }
    }
    
    private void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        SFXcontroller.sfxcontroller.PlayExplosionSound();
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
        Invoke("DisableAttack",1.5f);
    }

    private void DisableAttack()
    {
        Beam.SetActive(false);
        delayTimer = attackDelay;
        attacking = false;
    }

    #endregion

    private void OnDisable()
    {
        EnableCapturedShip();
    }

    private void EnableCapturedShip()
    {
        if(beam.capturedShip != null)
        {
            Transform mainShip = GameController.gm.myShip.transform;
            GameObject capturedShip = beam.capturedShip;
            capturedShip.GetComponent<PlayerController>().enabled = true;
            capturedShip.transform.parent = GameController.gm.myShip.transform;
            int side;
            if (mainShip.childCount % 2 == 0) side = 1;
            else side = -1;
            capturedShip.transform.position = GameController.gm.myShip.transform.position + Vector3.right * .5f * side;
            capturedShip.SetActive(true);
            capturedShip.GetComponent<PlayerController>().SetSpeed2Zero();
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerBulletTag")
        {
            PlayExplosion();
            
            EnableCapturedShip();

            //Destroy enemy ship
            gameObject.SetActive(false);
        }
    }
}
