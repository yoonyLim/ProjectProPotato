using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultDisplay : MonoBehaviour
{
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject resultUI;
    [SerializeField] TextMeshProUGUI winnerText;
    [SerializeField] TextMeshProUGUI countdownText;

    private bool isGameFinished = false;
    private float waitTime = 5.0f;

    public void UpdateWinner(string winner)
    {
        winnerText.text = winner;
        StartCoroutine(ShowResult());
    }

    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(3f);
        isGameFinished = true;
        inGameUI.SetActive(false);
        resultUI.SetActive(true);
    }

    private void Update()
    {
        if (isGameFinished && waitTime >= 0)
        {
            waitTime -= Time.deltaTime;
            countdownText.text = Mathf.Ceil(waitTime).ToString("0");
        }

        if (Mathf.Ceil(waitTime) == 0)
        {
            LoadingController.instance.LoadScene(0);
        }
    }
}
