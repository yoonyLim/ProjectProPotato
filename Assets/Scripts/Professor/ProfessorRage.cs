using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorRage : MonoBehaviour
{
    [SerializeField] float rageStartTime = 40f;
    SpriteRenderer spriteRenderer;
    float colorElapsedTime = 0f;
    [SerializeField] float colorChangeDuration = 4f;
    [SerializeField] GameObject smokeObject;
    bool hasRaged = false;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasRaged && GameManager.instance.elapsedTime > rageStartTime)
        {
            GameManager.instance.rageTransforming = true;
            StartCoroutine(RageTransformRoutine());
            hasRaged = true;
        }
    }

    private void ChangeCooldown()
    {
        GameManager.instance.knifeCooldown -= 1f;
        GameManager.instance.pinCooldown -= 1f;
        GameManager.instance.knifeSpeed += 25f;
        GameManager.instance.knifeRoateSpeed += 200f;
        GameManager.instance.pinSpeed += 25f;
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
        
        animator.SetLayerWeight(animator.GetLayerIndex("Angry"), 1f);
        animator.SetLayerWeight(animator.GetLayerIndex("Normal"), 0f);
        animator.SetTrigger("Anger");
        smokeObject.SetActive(true);
        AudioManager.instance.PlayAngerVoice();
        StartCoroutine(ChangeColor());
        ChangeCooldown();
        yield return new WaitForSeconds(4f);
        GameManager.instance.rageTransforming = false;
    }
}
