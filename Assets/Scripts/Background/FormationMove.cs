using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationMove : MonoBehaviour
{
    private void Update()
    {
        transform.localScale = Vector3.one * (1.25f + Mathf.Sin(Time.time) * 0.2f);
    }
}
