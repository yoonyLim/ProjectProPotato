using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    [SerializeField] Image[] hearts;
    private int lives = 3;

    // Update is called once per frame
    public void UpdateLifeDisplay(int lives)
    {
        this.lives = lives;

        foreach (Image heart in hearts)
        {
            heart.enabled = false;
        }

        for (int i = 0; i < lives; i++)
        {
            hearts[i].enabled = true;
        }
    }
}
