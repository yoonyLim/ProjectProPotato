using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ProfessorController : MonoBehaviour
{
    [SerializeField] ObjectPooler knifePool;
    Animator anim;

    

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            float knifeXPos = SpawnKnife();

            if (knifeXPos > 0)
            {
                anim.SetTrigger("LeftThrow");
            } 
            else if (knifeXPos < 0)
            {
                anim.SetTrigger("RightThrow");
            }

        }



    }

    float SpawnKnife()
    {
        GameObject knife = knifePool.GetPooledObject();
        knife.SetActive(true);
        return knife.transform.position.x;
    }

    

    

    
}
