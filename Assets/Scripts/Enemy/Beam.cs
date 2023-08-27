using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShipTag"))
        {
            other.GetComponent<PlayerController>().enabled = false;
            other.transform.parent = transform.parent;
            other.transform.position = transform.parent.position + Vector3.up * .4f;
                //Vector3.Lerp(other.transform.position, transform.parent.position + Vector3.up, 0.01f);
        }
    }
}
