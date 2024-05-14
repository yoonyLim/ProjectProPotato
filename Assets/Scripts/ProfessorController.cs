using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ProfessorController : MonoBehaviour
{
    [SerializeField] ObjectPooler knifePool;
    Animator anim;
    bool throwReady = true;
    [SerializeField] float cooldownTime = 2f;
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
        if (Input.GetKeyDown(KeyCode.A) && throwReady)
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

            StartCoroutine(ThrowCooldownRoutine(cooldownTime));

        }


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

    

    

    
}
