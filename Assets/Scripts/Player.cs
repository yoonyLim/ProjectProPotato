using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isRight;

    // Start is called before the first frame update
    void Awake()
    {
        isRight = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRight)
            transform.position = Vector3.right*5+Vector3.up*2.5f;
        else
            transform.position = Vector3.left*5 + Vector3.up * 2.5f;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRight = !isRight;
        }
    }
}
