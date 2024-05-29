using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float elapsedTime = 0f;

    public enum professorState { Idle, Attack, Transforming };
    public professorState state = professorState.Idle;

    public float knifeCooldown = 2f;
    public float pinCooldown = 5f;

    public float knifeSpeed = 100f;
    public float knifeRoateSpeed = 800f;

    public float pinSpeed = 100f;
    public float pinRotateSpeed = 1500f;

    public bool rageTransforming = false;

    public int maxPotatoFeverCount = 3;

    public int alcoholThreshold = 3;

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

    public void UpdateElapsedTime(float val)
    {
        elapsedTime = val;
    }
}
