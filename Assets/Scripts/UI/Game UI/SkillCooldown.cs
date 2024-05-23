using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI knifeCooldownTxt;
    [SerializeField] private TextMeshProUGUI rollerCooldownTxt;
    [SerializeField] private Image knifeCooldownCircle;
    [SerializeField] private Image rollerCooldownCircle;
    [SerializeField] private Image knifeSprite;
    [SerializeField] private Image rollerSprite;

    private float unitCooldown = 1.0f;

    private float knifeCooldown = 0f;
    private float rollerCooldown = 0f;

    private bool isKnifeThrown = false;
    private bool isRollerRolled = false;

    public void AlertKnifeCooldownUI()
    {
        knifeCooldown = GameManager.instance.knifeCooldown;
        isKnifeThrown = true;
    }

    public void AlertRollerCooldownUI()
    {
        rollerCooldown = GameManager.instance.pinCooldown;
        isRollerRolled = true;
    }

    private void Awake()
    {
        knifeCooldownTxt.enabled = false;
        rollerCooldownTxt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isKnifeThrown)
        {
            knifeCooldownCircle.fillAmount = (GameManager.instance.knifeCooldown - knifeCooldown) / GameManager.instance.knifeCooldown;
            knifeCooldown -= Time.deltaTime;
            // tint sprite
            knifeSprite.color = new Color(knifeSprite.color.r, knifeSprite.color.g, knifeSprite.color.b, 0.5f);
            // update text
            knifeCooldownTxt.text = (knifeCooldown * unitCooldown).ToString("0");
            knifeCooldownTxt.enabled = true;
        }

        if (knifeCooldown <= 0)
        {
            knifeCooldownCircle.fillAmount = 1;
            knifeSprite.color = new Color(knifeSprite.color.r, knifeSprite.color.g, knifeSprite.color.b, 1);
            knifeCooldownTxt.enabled = false;
            isKnifeThrown = false;
            knifeCooldown = 0;
        }

        if (isRollerRolled)
        {
            rollerCooldownCircle.fillAmount = (GameManager.instance.pinCooldown - rollerCooldown) / GameManager.instance.pinCooldown;
            rollerCooldown -= Time.deltaTime;
            // tint sprite
            rollerSprite.color = new Color(rollerSprite.color.r, rollerSprite.color.g, rollerSprite.color.b, 0.5f);
            // update text
            rollerCooldownTxt.text = (rollerCooldown * unitCooldown).ToString("0");
            rollerCooldownTxt.enabled = true;
        }

        if (rollerCooldown <= 0)
        {
            rollerCooldownCircle.fillAmount = 1;
            rollerSprite.color = new Color(rollerSprite.color.r, rollerSprite.color.g, rollerSprite.color.b, 1);
            rollerCooldownTxt.enabled = false;
            isRollerRolled = false;
            rollerCooldown = 0;
        }
    }
}
