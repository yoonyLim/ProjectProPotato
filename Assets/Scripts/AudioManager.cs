using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Throwing Swoosh Sound")]
    [SerializeField] AudioClip throwingClip;
    [SerializeField]
    [Range(0f, 1f)] float throwingVolume = 1f;

    [Header("Knife Roate Sound")]
    [SerializeField] AudioClip knifeRotateClip;
    [SerializeField]
    [Range(0f, 1f)] float knifeVolume = 1f;

    [Header("Rolling Pin Sound")]
    [SerializeField] AudioClip rollingPinClip;
    [SerializeField]
    [Range(0f, 1f)] float rollingPinVolume = 1f;

    [Header("Professor Throw Voice")]
    [SerializeField] AudioClip[] throwVoices;
    [SerializeField]
    [Range(0f, 1f)] float[] throwVoicesVolume;

    [Header("Professor Roll Voice")]
    [SerializeField] AudioClip rollVoice;
    [SerializeField]
    [Range(0f, 1f)] float rollVoiceVolume = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }

    public void PlayThrowingClip()
    {
        PlayClip(throwingClip, throwingVolume);
    }

    public void PlayKnifeClip()
    {
        PlayClip(knifeRotateClip, knifeVolume);
    }

    public void PlayPinClip()
    {
        PlayClip(rollingPinClip, rollingPinVolume);
    }

    public void PlayThrowVoice()
    {
        int randomIndex = UnityEngine.Random.Range(0, throwVoices.Length);
        PlayClip(throwVoices[randomIndex], throwVoicesVolume[randomIndex]);
    }

    public void PlayRollVoice()
    {
        PlayClip(rollVoice, rollVoiceVolume);
    }
}
