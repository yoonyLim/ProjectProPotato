using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    public static ObjectsPool pool;

    private List<GameObject> pooledPotatoes = new List<GameObject>();
    private List<GameObject> pooledBottles = new List<GameObject>();
    private int amntPool = 30;

    [SerializeField] private GameObject potatoPrefab;
    [SerializeField] private GameObject bottlePrefab;
    private void Awake()
    {
        if (pool == null)
        {
            pool = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < amntPool; i++)
        {
            GameObject potato = Instantiate(potatoPrefab);
            potato.SetActive(false);
            pooledPotatoes.Add(potato);

            GameObject bottle = Instantiate(bottlePrefab);
            bottle.SetActive(false);
            pooledBottles.Add(bottle);
        }
    }

    public GameObject GetPooledPotato()
    {
        for (int i = 0; i < pooledPotatoes.Count; i++)
        {
            if (!pooledPotatoes[i].activeInHierarchy)
            {
                return pooledPotatoes[i];
            }
        }

        return null;
    }

    public GameObject GetPooledBottle()
    {
        for (int i = 0; i < pooledBottles.Count; i++)
        {
            if (!pooledBottles[i].activeInHierarchy)
            {
                return pooledBottles[i];
            }
        }

        return null;
    }
}