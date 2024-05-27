using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingController : MonoBehaviour
{
    public static LoadingController instance;

    [SerializeField] private TextMeshProUGUI percentTxt = null;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] Animator transitionAnim;

    private float unitPercent = 1.0f;

    public void SetPercentTxt(float val)
    {
        float res = val * unitPercent;
        percentTxt.text = res.ToString("0") + "%";
    }

    public void LoadScene(int sceneId)
    {
        if (sceneId == 1)
        {
            loadingScreen.SetActive(true);
            mainMenu.SetActive(false);
        }

        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        transitionAnim.SetTrigger("Fade In");

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        while (!operation.isDone && sceneId == 1)
        {
            loadingBar.value = operation.progress * 100;
            yield return null;
        }
    }

    private void Start()
    {
        transitionAnim.SetTrigger("Fade Out");
    }

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
}
