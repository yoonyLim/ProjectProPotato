using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GenericObserver<int> Lives = new GenericObserver<int>(3);

    Animator potatoAnimator;

    public bool isRight;

    private Vector3 rightPos = Vector3.right * 5 + Vector3.up * 0.67f;
    private Vector3 leftPos = Vector3.left * 5 + Vector3.up * 0.67f;


    // Start is called before the first frame update
    void Awake()
    {
        Lives.Invoke();
        isRight = true;
        potatoAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, (isRight ? rightPos : leftPos), 0.1f);
        //if (isRight)
        //    transform.position = Vector3.right*5+Vector3.up*0.67f;
        //else
        //    transform.position = Vector3.left*5 + Vector3.up * 0.67f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //potatoAnimator.SetBool("isJump", true);
            isRight = !isRight;
        }

        // If player is hit by a knife or a roller
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            Lives.Value--;
    }
}
