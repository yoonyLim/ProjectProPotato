using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorRage : MonoBehaviour
{
    [SerializeField] float rageStartTime = 10f;
    SpriteRenderer spriteRenderer;
    float colorElapsedTime = 0f;
    [SerializeField] float colorChangeDuration = 4f;
    bool hasRaged = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasRaged && GameManager.instance.elapsedTime > rageStartTime)
        {
            StartCoroutine(RageTransformRoutine());
            hasRaged = true;
        }
    }

    private void ChangeCooldown()
    {
        GameManager.instance.knifeCooldown = 1f;
        GameManager.instance.pinCooldown = 4f;
        GameManager.instance.knifeSpeed += 50f;
        GameManager.instance.knifeRoateSpeed += 200f;
        GameManager.instance.pinSpeed += 50f;
        GameManager.instance.pinRotateSpeed += 500f;
    }

    private IEnumerator ChangeColor()
    {
        while (colorElapsedTime < colorChangeDuration)
        {
            colorElapsedTime += Time.deltaTime;
            float t = colorElapsedTime / colorChangeDuration;
            t = Mathf.Clamp01(t);
            spriteRenderer.color = Color.Lerp(new Color(1f, 1f, 1f), new Color(1f, 0.8f, 0.8f), t);
            yield return null; // wait for the next frame
        }
    }

    IEnumerator RageTransformRoutine()
    {
        GameManager.instance.rageTransforming = true;
        StartCoroutine(ChangeColor());
        ChangeCooldown();
        yield return new WaitForSeconds(6f);
        GameManager.instance.rageTransforming = false;
    }
}
