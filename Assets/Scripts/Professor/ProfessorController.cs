using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ProfessorController : MonoBehaviour
{
    [SerializeField] KnifePooler knifePool;
    [SerializeField] PinPooler pinPool;
    Animator anim;
    bool throwReady = true;
    bool rollReady = true;

    public float knifeXPos;
    public Transform leftAimPoint;
    public Transform rightAimPoint;

    public float startTime;
    public float throwKeyTime = 0.5f;
    bool isHoldingA = false;



    private void Awake()
    {
        anim = GetComponent<Animator>();
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
                }
                isHoldingA = false;
            }
            if (Input.GetKey(KeyCode.A) && isHoldingA)
            {
                if (Time.time - startTime > 1f)
                {
                    RollPin();
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
            knifeXPos = SpawnKnife();

            if (knifeXPos > 0)
            {
                anim.SetTrigger("LeftThrow");
                AudioManager.instance.PlayThrowVoice();
                StartCoroutine(PlayThrowSound(0.1f));
            }
            else if (knifeXPos < 0)
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
            SpawnPin();

            anim.SetTrigger("Roll");
            AudioManager.instance.PlayRollVoice();

            StartCoroutine(RollCooldownRoutine(GameManager.instance.pinCooldown));
        }
    }

    float SpawnKnife()
    {
        GameObject knife = knifePool.GetPooledObject();
        StartCoroutine(SpawnKnifeDelay(knife));
        return knife.transform.position.x;
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
