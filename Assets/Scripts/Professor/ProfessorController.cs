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
    [SerializeField] float pinCooldownTime = 5f;
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            ThrowKnife();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            RollPin();
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
            }
            else if (knifeXPos < 0)
            {
                anim.SetTrigger("RightThrow");
            }

            StartCoroutine(ThrowCooldownRoutine(knifeCooldownTime));

        }
    }

    void RollPin()
    {
        if ( rollReady)
        {
            SpawnPin();

            anim.SetTrigger("Roll");

            StartCoroutine(RollCooldownRoutine(pinCooldownTime));
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
    }

    


}
