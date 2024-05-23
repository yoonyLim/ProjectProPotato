using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuAudioManager : MonoBehaviour
{
    public static MenuAudioManager instance;

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;

    [Header("Audio Clip")]
    [SerializeField] AudioClip BGMClip;

    private void Start()
    {
        musicSource.clip = BGMClip;
        musicSource.Play();
    }
}
