using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuAudioManager : MonoBehaviour
{
    public static MenuAudioManager instance;

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    [SerializeField] AudioClip BGMClip;
    [SerializeField]
    [Range(0f, 1f)] float BGMVolume = 1f;

    private void Start()
    {
        musicSource.clip = BGMClip;
        musicSource.Play();
    }
}
