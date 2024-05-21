using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI percentTxt = null;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    private float unitPercent = 1.0f;

    public void SetPercentTxt(float val)
    {
        float res = val * unitPercent;
        percentTxt.text = res.ToString("0") + "%";
    }

    public void LoadScene(int sceneId)
    {
        loadingScreen.SetActive(true);
        mainMenu.SetActive(false);
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        while (!operation.isDone)
        {
            loadingBar.value = operation.progress * 100;
            yield return null;
        }
    }
}
