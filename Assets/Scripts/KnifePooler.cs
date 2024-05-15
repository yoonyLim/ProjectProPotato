using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifePooler : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int poolSize = 10;
    [SerializeField] List<Transform> spawnPoints;


    private List<GameObject> pool;
    // Start is called before the first frame update
    void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                obj.transform.rotation = Quaternion.Euler(-90f, -90f, 0f);
                return obj;
            }
        }

        return CreateNewObject();

    }

    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab, transform);
        obj.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        obj.transform.rotation = Quaternion.Euler(-90f, -90f, 0f);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

}
