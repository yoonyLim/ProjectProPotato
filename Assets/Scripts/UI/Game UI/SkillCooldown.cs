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

    private void CooldownSkill(Image progress, float cooldown, float curCooldown, Image sprite, TextMeshProUGUI txt)
    {
        progress.fillAmount = (cooldown - curCooldown) / cooldown;
        // tint color
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
        // update text
        txt.text = Mathf.Ceil(curCooldown).ToString("0");
        txt.enabled = true;
    }

    private void ResetCooldown(Image progress, Image sprite, TextMeshProUGUI txt, ref bool isUsed)
    {
        progress.fillAmount = 1;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        txt.enabled = false;
        isUsed = false;
    }

    void Update()
    {
        if (isKnifeThrown)
        {
            knifeCooldown -= Time.deltaTime;
            CooldownSkill(knifeCooldownCircle, GameManager.instance.knifeCooldown, knifeCooldown, knifeSprite, knifeCooldownTxt);
        }

        if (knifeCooldown <= 0)
        {
            ResetCooldown(knifeCooldownCircle, knifeSprite, knifeCooldownTxt, ref isKnifeThrown);
        }

        if (isRollerRolled)
        {
            rollerCooldown -= Time.deltaTime;
            CooldownSkill(rollerCooldownCircle, GameManager.instance.pinCooldown, rollerCooldown, rollerSprite, rollerCooldownTxt);
        }

        if (rollerCooldown <= 0)
        {
            ResetCooldown(rollerCooldownCircle, rollerSprite, rollerCooldownTxt, ref isRollerRolled);
        }
    }
}
