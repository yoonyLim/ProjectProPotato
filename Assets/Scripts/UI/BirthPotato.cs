using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthPotato : MonoBehaviour
{
    private void TossPotato()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);

        GameObject potato = PotatoPool.pool.GetPooledPotato();

        if (potato != null)
        {
            potato.transform.position = new Vector3(Random.Range(-960, 960), 0.0f, 0.0f);
            potato.SetActive(true);
        }
    }

    void Start()
    {
        InvokeRepeating("TossPotato", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
