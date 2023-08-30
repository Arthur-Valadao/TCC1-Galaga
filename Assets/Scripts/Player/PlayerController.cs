using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    #region Parameters

    [SerializeField] private GameObject bullet;
    [SerializeField] private float speed;
    private float rightLimit;
    private float leftLimit;
    
    [SerializeField] private GameObject player;
    private Vector3 startPos = new Vector3(0, -2.7f, 0);

    #endregion

    private void Awake()
    {
        GameController.gm.myShip = gameObject;
        rightLimit = Camera.main.ViewportToWorldPoint(new Vector2(.9f, 0)).x;
        leftLimit = Camera.main.ViewportToWorldPoint(new Vector2(.1f, 0)).x;
    }

    private void Update()
    {
        Move(Input.GetAxis("Horizontal"));
        if (Input.GetButtonDown("Jump")) Shoot();
    }

    public void SetSpeed2Zero()
    {
        speed = 0;
    }

    private void Shoot()
    {
        GameObject shoot = Instantiate(bullet);
        SFXcontroller.sfxcontroller.PlayLaserSound();
        shoot.transform.position = transform.position;
    }

    private void Move(float direction)
    {
        Vector3 newPosition = new Vector3(transform.position.x + direction * speed * Time.deltaTime,transform.position.y,transform.position.z);
        if (newPosition.x > leftLimit && newPosition.x < rightLimit) transform.position = newPosition;
    }

    private void OnDisable()
    {
        Invoke("RespawnPlayer",2f);
    }

    private void RespawnPlayer()
    {
        if(GameController.gm.canRespawn)
        {
            GameObject newShip = Instantiate(player);
            newShip.transform.position = startPos;
            newShip.SetActive(true);
            newShip.GetComponent<PlayerController>().enabled = true;
            GameController.gm.myShip = newShip;
        }
    }
}
