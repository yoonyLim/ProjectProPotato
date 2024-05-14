using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorController : MonoBehaviour
{
    [SerializeField] ObjectPooler knifePool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnKnife();
        }
    }

    void SpawnKnife()
    {
        GameObject knife = knifePool.GetPooledObject();
        knife.SetActive(true);
    }
}
