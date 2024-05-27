using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlowGaugeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI percentTxt = null;
    [SerializeField] private Slider blowGauge = null;

    public float gaugeSpeed = 70f;

    public void SetPercentTxt(float val)
    {
        float res = val;
        percentTxt.text = res.ToString("0") + "%";
    }

    void DecreasePercent()
    {
        if (blowGauge.value > 0.0f && blowGauge.value < 100.0f)
        {
            blowGauge.value -= 1.0f;
        }
    }

    private void Start()
    {
        InvokeRepeating("DecreasePercent", 0.0f, 0.3f);
    }

    private void Update()
    {
        Debug.Log($"Professor: {NamedPipeClient1.Instance.ProAvg}");

        if (NamedPipeClient1.Instance.ProAvg >= 3)
        {
            blowGauge.value += gaugeSpeed * Time.deltaTime;
        }
        if (blowGauge.value == 100.0f)
        {
            LoadingController.instance.LoadScene(1);
        }
    }

    
}
