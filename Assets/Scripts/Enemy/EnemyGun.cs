using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBulletGO;

    
    void Start()
    {
        RetardGunFire(1f);
    }

    public void RetardGunFire(float timer)
    {
        Invoke("FireEnemyBullet", timer);
    }

    public void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find("PlayerGO");

        if(playerShip != null)
        {
            GameObject bullet = (GameObject)Instantiate(EnemyBulletGO);
            bullet.transform.position = transform.position;
            Vector2 direction = playerShip.transform.position - bullet.transform.position;
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
