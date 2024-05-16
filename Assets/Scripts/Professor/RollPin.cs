using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollPin : MonoBehaviour
{
    Rigidbody body;
    [SerializeField] float pinSpeed;
    [SerializeField] float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        body.velocity = Vector3.back * pinSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjectDeactivateWall"))
        {
            gameObject.SetActive(false);
        }
    }
}
