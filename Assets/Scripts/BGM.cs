using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField] AudioClip firstBGM;
    [SerializeField] AudioClip secondBGM;
    public AudioSource audioSource;
    Animator musicAnim;
    bool musicChanged = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        musicAnim = GetComponent<Animator>();
        audioSource.clip = firstBGM;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state == GameManager.professorState.Transforming && !musicChanged)
        {
            StartCoroutine(ChangeMusicRoutine());   
            musicChanged = true;
        }
    }

    IEnumerator ChangeMusicRoutine()
    {
        musicAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1f);
        audioSource.clip = secondBGM;
        audioSource.Play();
    }
}
