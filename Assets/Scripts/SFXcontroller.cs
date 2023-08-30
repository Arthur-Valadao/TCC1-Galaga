using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXcontroller : MonoBehaviour
{
    #region Parameters

    public static SFXcontroller sfxcontroller;
    private AudioSource audioSource;

    [SerializeField] private AudioClip explosion;
    [SerializeField] private AudioClip laser;

    #endregion

    private void Start()
    {
        sfxcontroller = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayExplosionSound()
    {
        audioSource.PlayOneShot(explosion);
    }
    
    public void PlayLaserSound()
    {
        audioSource.PlayOneShot(laser);
    }
}
