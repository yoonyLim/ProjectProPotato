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
    [SerializeField] float knifeCooldownTime = 2f;
    [SerializeField] float pinCooldownTime = 2f;
    public float knifeXPos;
    public Transform leftAimPoint;
    public Transform rightAimPoint;




    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && throwReady && anim.GetCurrentAnimatorStateInfo(0).IsName("Professor_Idle"))
        {

            knifeXPos = SpawnKnife();

            if (knifeXPos > 0)
            {
                anim.SetTrigger("LeftThrow");
            } 
            else if (knifeXPos < 0)
            {
                anim.SetTrigger("RightThrow");
            }

            StartCoroutine(ThrowCooldownRoutine(knifeCooldownTime));

        }

        if (Input.GetKeyDown(KeyCode.D) && rollReady && anim.GetCurrentAnimatorStateInfo(0).IsName("Professor_Idle"))
        {
            SpawnPin();

            anim.SetTrigger("Roll");

            StartCoroutine(RollCooldownRoutine(pinCooldownTime));
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

    float SpawnKnife()
    {
        GameObject knife = knifePool.GetPooledObject();
        StartCoroutine(spawnKnifeDelay(knife));
        return knife.transform.position.x;
    }

    IEnumerator spawnKnifeDelay(GameObject knife)
    {
        yield return new WaitForSeconds(0.5f);
        knife.SetActive(true);
    }

    void SpawnPin()
    {
        GameObject pin = pinPool.GetPooledObject();
        StartCoroutine(spawnPinDelay(pin));
    }

    IEnumerator spawnPinDelay(GameObject pin)
    {
        yield return new WaitForSeconds(1f);
        pin.SetActive(true);
    }

    

    

    
}
