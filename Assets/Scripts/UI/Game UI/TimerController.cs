using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] ResultDisplay resultDisplay;

    [SerializeField] private TextMeshProUGUI timerTxt = null;
    [SerializeField] private Slider timerBar = null;
    
    private const float PLAY_TIME = 60.0f;

    [SerializeField] GenericObserver<float> ElapsedTime = new GenericObserver<float>(0.0f);

    public void UpdateTimerBar(float val)
    {
        timerBar.value = (PLAY_TIME - val) / PLAY_TIME * 100.0f;
    }

    public void UpdateTimerTxt(float val)
    {
        float res = (PLAY_TIME - val);
        timerTxt.text = res.ToString("0") + " SECONDS";
    }

    private void Awake()
    {
        ElapsedTime.Invoke();
    }

    private void Update()
    {
        if (ElapsedTime.Value < PLAY_TIME)
            ElapsedTime.Value += Time.deltaTime;
        else
            resultDisplay.UpdateWinner("감자");
    }
}
