using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    VideoPlayer vp;
    [SerializeField] GameObject videoObj;
    [SerializeField] GameObject videoUI;
    private void Awake()
    {
        Time.timeScale = 0f;
        videoObj.SetActive(true);
        videoUI.SetActive(true);
        vp = GetComponent<VideoPlayer>();
        vp.Play();
        vp.loopPointReached += OnVideoEnd;

    }
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoObj.SetActive(false);
        videoUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
