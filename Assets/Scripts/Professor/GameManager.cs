using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float elapsedTime = 0f;
    public enum professorState { Idle, Attack };
    public professorState state = professorState.Idle;

    public float knifeCooldown = 2f;
    public float pinCooldown = 5f;

    public float knifeSpeed = 100f;
    public float knifeRoateSpeed = 800f;

    public float pinSpeed = 100f;
    public float pinRotateSpeed = 1500f;

    public bool rageTransforming = false;
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

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }
}
