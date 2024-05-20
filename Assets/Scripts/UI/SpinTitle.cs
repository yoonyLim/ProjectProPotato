using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTitle : MonoBehaviour
{
    private List<GameObject> childrenList = new List<GameObject>();
    private List<float> torqueSpeedList = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);

        foreach (Transform child in gameObject.transform)
        {
            childrenList.Add(child.gameObject);
            torqueSpeedList.Add(Random.Range(-0.5f, 0.5f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < childrenList.Count; i++)
        {
            childrenList[i].transform.Rotate(0f, 0f, torqueSpeedList[i]);
        }
    }
}
