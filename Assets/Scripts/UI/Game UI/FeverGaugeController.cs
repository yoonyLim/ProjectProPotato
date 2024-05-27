using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverGaugeController : MonoBehaviour
{
    [SerializeField] private Slider feverGauge;
    [SerializeField] private Image fill;

    private const int maxFeverCount = 3;
    private int curFeverCount = 0;

    private float HSVColor = 0f;

    public void UpdateFeverCount(int val)
    {
        print(val);

        curFeverCount = val;

        if (curFeverCount != maxFeverCount)
            fill.color = Color.red;

        feverGauge.value = (float)val / (float)maxFeverCount * 100;
    }

    private void Update()
    {
        if (curFeverCount == maxFeverCount)
        {
            HSVColor = (HSVColor + 0.01f) % 1f;
            fill.color = Color.HSVToRGB(HSVColor, 1, 1);
        }
    }
}
