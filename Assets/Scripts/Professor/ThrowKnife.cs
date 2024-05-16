using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowKnife : MonoBehaviour
{
    Rigidbody body;
    [SerializeField] float throwSpeed;
    [SerializeField] float rotateSpeed;
    

    
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FindObjectOfType<ProfessorController>().knifeXPos > 0)
        {
            body.velocity = (FindObjectOfType<ProfessorController>().leftAimPoint.position - transform.position).normalized * throwSpeed;
        }
        else if (FindObjectOfType<ProfessorController>().knifeXPos < 0)
        {
            body.velocity = (FindObjectOfType<ProfessorController>().rightAimPoint.position - transform.position).normalized * throwSpeed;
        }
        



    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjectDeactivateWall"))
        {
            gameObject.SetActive(false);
        }
    }


}
