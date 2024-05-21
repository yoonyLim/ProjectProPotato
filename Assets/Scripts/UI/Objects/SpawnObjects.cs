using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private int MAX_SPAWNS = 10;
    private int numPotatoes;
    private int numBottles;
    private void TossObjects()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);

        numPotatoes = Random.Range(1, MAX_SPAWNS);
        numBottles = MAX_SPAWNS - numPotatoes;

        for (int i = 0; i < numPotatoes; i++)
        {
            GameObject potato = ObjectsPool.pool.GetPooledPotato();

            if (potato != null)
            {
                IPooledObjects pooledPotato = potato.GetComponent<IPooledObjects>();

                potato.SetActive(true);
                potato.transform.position = new Vector3(Random.Range(-12, 12), -5.0f, 0.0f);
                pooledPotato.OnObjectSpawn();
            }
        }

        for (int i = 0; i < numBottles; i++)
        {
            GameObject bottle = ObjectsPool.pool.GetPooledBottle();

            if (bottle != null)
            {
                IPooledObjects pooledBottle = bottle.GetComponent<IPooledObjects>();

                bottle.SetActive(true);
                bottle.transform.position = new Vector3(Random.Range(-12, 12), -5.0f, 0.0f);
                pooledBottle.OnObjectSpawn();
            }
        }
    }

    void Start()
    {
        InvokeRepeating("TossObjects", 0.0f, 0.5f);
    }
}
