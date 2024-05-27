using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ProfessorController : MonoBehaviour
{
    [SerializeField] KnifePooler knifePool;
    [SerializeField] PinPooler pinPool;
    [SerializeField] SkillCooldown SkillUI;

    [SerializeField] GenericObserver<int> attackCount = new GenericObserver<int>(0);

    Animator anim;
    bool throwReady = true;
    bool rollReady = true;

    public int knifeSpawnIndex;
    public Transform leftAimPoint;
    public Transform rightAimPoint;

    public float startTime;
    public float throwKeyTime = 0.3f;
    bool isHoldingA = false;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        attackCount.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.rageTransforming && GameManager.instance.state == GameManager.professorState.Idle)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                isHoldingA = true;
                startTime = Time.time;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                float timeDiff = Time.time - startTime;
                if (timeDiff <= throwKeyTime)
                {
                    ThrowKnife();
                    attackCount.Value++;
                }
                isHoldingA = false;
            }
            if (Input.GetKey(KeyCode.A) && isHoldingA)
            {
                if (Time.time - startTime > 0.5f)
                {
                    RollPin();
                    attackCount.Value++;
                }
                
            } 
        }
        

    }

    

    IEnumerator RollCooldownRoutine(float cooldownTime)
    {
        rollReady = false;

        yield return new WaitForSeconds(cooldownTime);  

        rollReady = true;
    }

    IEnumerator ThrowCooldownRoutine(float cooldownTime)
    {
        throwReady = false;

        yield return new WaitForSeconds(cooldownTime);

        throwReady = true;
    }

    void ThrowKnife()
    {
        if (throwReady)
        {
            // alert UI
            SkillUI.AlertKnifeCooldownUI();

            knifeSpawnIndex = SpawnKnife();

            if (knifeSpawnIndex == 0)
            {
                anim.SetTrigger("LeftThrow");
                AudioManager.instance.PlayThrowVoice();
                StartCoroutine(PlayThrowSound(0.1f));
            }
            else if (knifeSpawnIndex == 1)
            {
                anim.SetTrigger("RightThrow");
                AudioManager.instance.PlayThrowVoice();
                StartCoroutine(PlayThrowSound(0.2f));
            }

            StartCoroutine(ThrowCooldownRoutine(GameManager.instance.knifeCooldown));


        }
    }
    IEnumerator PlayThrowSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.instance.PlayThrowingClip();
    }

    void RollPin()
    {
        if ( rollReady)
        {
            // alert UI
            SkillUI.AlertRollerCooldownUI();

            SpawnPin();

            anim.SetTrigger("Roll");
            AudioManager.instance.PlayRollVoice();

            StartCoroutine(RollCooldownRoutine(GameManager.instance.pinCooldown));
        }
    }

    int SpawnKnife()
    {
        var objTuple = knifePool.GetPooledObject();
        GameObject knife = objTuple.Item1;
        int index = objTuple.Item2;
        StartCoroutine(SpawnKnifeDelay(knife));
        return index;
    }

    IEnumerator SpawnKnifeDelay(GameObject knife)
    {
        yield return new WaitForSeconds(0.5f);
        knife.SetActive(true);
        AudioManager.instance.PlayKnifeClip();
    }

    void SpawnPin()
    {
        GameObject pin = pinPool.GetPooledObject();
        StartCoroutine(SpawnPinDelay(pin));
    }

    IEnumerator SpawnPinDelay(GameObject pin)
    {
        yield return new WaitForSeconds(1f);
        pin.SetActive(true);
        AudioManager.instance.PlayPinClip();
    }

    


}
