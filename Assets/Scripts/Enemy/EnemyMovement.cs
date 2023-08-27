using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    void MovementPattern()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 spawnLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.25f));
        Vector2 spawnRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.25f));
        Vector2 spawnTopLeft = Camera.main.ViewportToWorldPoint(new Vector2(0.25f, 1));
        Vector2 spawnTopRight  = Camera.main.ViewportToWorldPoint(new Vector2(0.75f, 1));      
    }
}
