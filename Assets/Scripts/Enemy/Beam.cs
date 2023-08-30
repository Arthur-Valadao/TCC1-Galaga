using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public GameObject capturedShip;

    private void DisableShip()
    {
        GameController.gm.canGalagaAttack = false;
        
        capturedShip.GetComponent<PlayerController>().enabled = false;
        capturedShip.transform.parent = transform.parent;
        capturedShip.transform.position = transform.parent.position + Vector3.up * .4f;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShipTag"))
        {
            capturedShip = other.gameObject;
            DisableShip();
            capturedShip.GetComponent<LifeSystem>().TakeDamage(1);
        }
    }
}
