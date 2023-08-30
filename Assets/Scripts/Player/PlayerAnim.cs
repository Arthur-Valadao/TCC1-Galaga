using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    #region Parameters

    private BoxCollider2D box;
    private LifeSystem life;
    private PlayerController pc;
    private Animator anim;
    private SpriteRenderer sprite;

    public bool respawning;
    
    #endregion

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        pc = GetComponent<PlayerController>();
        life = GetComponent<LifeSystem>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (respawning)
        {
            respawning = false;
            DisableForRespawn();
        }
    }

    public void DisableForRespawn()
    {
        GameController.gm.canRespawn = false;
        
        box.enabled = false;
        pc.enabled = false;
        life.enabled = false;
        sprite.enabled = false;

        Invoke("EnableAgain",3f);
    }

    public void EnableAgain()
    {
        PlayAnim("PlayerAnimation");
        
        transform.position = Vector3.Lerp(new Vector3(0,-5,0),transform.position,.005f);
        
        box.enabled = true;
        pc.enabled = true;
        life.enabled = true;
        sprite.enabled = true;
        
        GameController.gm.canRespawn = true;
    }

    public void PlayAnim(String animName)
    {
        anim.Play("PlayerExplosion");
    }
}
