using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    #region Parameters
    
    [SerializeField] private int maxLife;
    private int currentLife;
    private PlayerAnim anim;
    
    #endregion

    private void Start()
    {
        if (GameController.gm.LifeAmount() != 0) currentLife = GameController.gm.LifeAmount();
        else currentLife = maxLife;
        GameController.gm.UpdateLife(currentLife);

        anim = GetComponent<PlayerAnim>();
    }

    public void LifeUp(int healAmount)
    {
        currentLife += healAmount;
        //if (currentLife > maxLife) currentLife = maxLife;
        GameController.gm.UpdateLife(currentLife);
    }

    public void TakeDamage(int damageAmount)
    {
        currentLife -= damageAmount;
        if (currentLife <= 0)
        {
            currentLife = 0;
            Death();
        }
        GameController.gm.UpdateLife(currentLife);
    }

    private void Death()
    {
        gameObject.SetActive(false);
        SFXcontroller.sfxcontroller.PlayExplosionSound();
        GameController.gm.GameOver();
    }
}
