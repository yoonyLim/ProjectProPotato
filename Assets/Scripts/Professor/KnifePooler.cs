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

    public (GameObject, int) GetPooledObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                int randomIndex = Random.Range(0, spawnPoints.Count);
                obj.transform.position = spawnPoints[randomIndex].position;
                obj.transform.rotation = Quaternion.Euler(-90f, -90f, 0f);
                return (obj, randomIndex);
            }
        }

        return CreateNewObject();

    }

    private (GameObject, int) CreateNewObject()
    {
        int randomIndex = Random.Range(0, spawnPoints.Count);
        GameObject obj = Instantiate(prefab, transform);
        obj.transform.position = spawnPoints[randomIndex].position;
        obj.transform.rotation = Quaternion.Euler(-90f, -90f, 0f);
        obj.SetActive(false);
        pool.Add(obj);
        return (obj, randomIndex);
    }

}
