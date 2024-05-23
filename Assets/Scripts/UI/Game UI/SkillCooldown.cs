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
            // update text
            knifeCooldownTxt.text = (knifeCooldown * unitCooldown).ToString("0");
            knifeCooldownTxt.enabled = true;
        }

        if (knifeCooldown <= 0)
        {
            knifeCooldownTxt.enabled = false;
            isKnifeThrown = false;
            knifeCooldown = 0;
        }

        if (isRollerRolled)
        {
            rollerCooldownCircle.fillAmount = (GameManager.instance.pinCooldown - rollerCooldown) / GameManager.instance.pinCooldown;
            rollerCooldown -= Time.deltaTime;
            // update text
            rollerCooldownTxt.text = (rollerCooldown * unitCooldown).ToString("0");
            rollerCooldownTxt.enabled = true;
        }

        if (rollerCooldown <= 0)
        {
            rollerCooldownTxt.enabled = false;
            isRollerRolled = false;
            rollerCooldown = 0;
        }
    }
}
