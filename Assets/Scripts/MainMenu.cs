using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // 1 for "Game" scene
        SceneManager.LoadSceneAsync(1);
    }
}