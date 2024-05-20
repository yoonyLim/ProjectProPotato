using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthPotato : MonoBehaviour
{
    private int numPotatoes;
    private void TossPotato()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);

        numPotatoes = Random.Range(1, 8);

        for (int i = 0; i < numPotatoes; i++)
        {
            GameObject potato = PotatoPool.pool.GetPooledPotato();

            if (potato != null)
            {
                IPooledPotato pooledPotato = potato.GetComponent<IPooledPotato>();

                potato.SetActive(true);
                potato.transform.position = new Vector3(Random.Range(-12, 12), -5.0f, 0.0f);
                pooledPotato.OnPotatoSpawn();
            }
        }
    }

    void Start()
    {
        InvokeRepeating("TossPotato", 0.0f, 0.5f);
    }
}
