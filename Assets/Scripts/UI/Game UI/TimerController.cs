using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerTxt = null;
    [SerializeField] private Slider timerBar = null;
    [SerializeField] private static float PLAY_TIME = 90.0f;

    [SerializeField] GenericObserver<float> ElapsedTime = new GenericObserver<float>(0.0f);

    private float unitPercent = 1.0f;

    public void UpdateTimerBar(float val)
    {
        timerBar.value = (PLAY_TIME - val) / PLAY_TIME * 100.0f;
    }

    public void UpdateTimerTxt(float val)
    {
        float res = (PLAY_TIME - val) * unitPercent;
        timerTxt.text = res.ToString("0") + " SECONDS";
    }

    private void Awake()
    {
        ElapsedTime.Invoke();
    }

    private void Update()
    {
        ElapsedTime.Value += Time.deltaTime;
    }
}
