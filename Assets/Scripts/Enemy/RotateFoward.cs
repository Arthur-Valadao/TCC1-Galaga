using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFoward : MonoBehaviour
{
    #region Parameters

    private Vector3 position;
    private Vector3 previusPosition;

    #endregion

    private void Awake()
    {
        position = transform.parent.position;
        previusPosition = position;
    }

    private void Update()
    {
        position = transform.parent.position;

        Vector2 direction = new Vector2 (previusPosition.x, previusPosition.y) - new Vector2 (position.x, position.y);
        float anguloEmRadianos = Mathf.Atan2(direction.y, direction.x);
        float anguloEmGraus = anguloEmRadianos * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, anguloEmGraus-90), transform.rotation, .8f);

        previusPosition = transform.parent.position;
        
    }
}
