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

    #endregion

    private void Awake()
    {
        rightLimit = Camera.main.ViewportToWorldPoint(new Vector2(.9f, 0)).x;
        leftLimit = Camera.main.ViewportToWorldPoint(new Vector2(.1f, 0)).x;
    }

    private void Update()
    {
        Move(Input.GetAxis("Horizontal"));
        if (Input.GetButtonDown("Jump")) Shoot();
    }

    private void Shoot()
    {
        GameObject shoot = Instantiate(bullet);
        shoot.transform.position = transform.position;
    }

    private void Move(float direction)
    {
        Vector3 newPosition = new Vector3(transform.position.x + direction * speed * Time.deltaTime,transform.position.y,transform.position.z);
        if (newPosition.x > leftLimit && newPosition.x < rightLimit) transform.position = newPosition;
    }
}
