using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Object : MonoBehaviour, IPooledObjects
{
    private float size;
    private float torque;

    public void OnObjectSpawn()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);

        size = Random.Range(5, 20);
        transform.localScale = new Vector3(size, size, 1.0f);

        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-10, 10), Random.Range(1, 15));
        torque = Random.Range(-10.0f, 10.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0f, 0f, torque);

        if (Mathf.Abs(transform.position.x) > 190 || transform.position.y <= -10)
        {
            gameObject.SetActive(false);
        }
    }
}
