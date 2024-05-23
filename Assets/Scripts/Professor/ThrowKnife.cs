using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowKnife : MonoBehaviour
{
    Rigidbody body;
    private bool isPlayerAlreadyHit;

    
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.down * GameManager.instance.knifeRoateSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FindObjectOfType<ProfessorController>().knifeSpawnIndex == 0)
        {
            body.velocity = (FindObjectOfType<ProfessorController>().leftAimPoint.position - transform.position).normalized
                * GameManager.instance.knifeSpeed;
        }
        else if (FindObjectOfType<ProfessorController>().knifeSpawnIndex == 1)
        {
            body.velocity = (FindObjectOfType<ProfessorController>().rightAimPoint.position - transform.position).normalized
                * GameManager.instance.knifeSpeed;
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
