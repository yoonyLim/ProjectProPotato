using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    public float speed = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.elapsedTime <90f)
        {
            if (transform.position.z >= 82)
                transform.position = new Vector3(0, 0, -84);
            transform.position += Vector3.forward * speed * Time.fixedDeltaTime;
        }
        
        
    }
}
