using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoPool : MonoBehaviour
{
    public static PotatoPool pool;

    private List<GameObject> pooledPotatoes = new List<GameObject>();
    private int amntPool = 15;

    [SerializeField] private GameObject potatoPrefab;

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
}
