using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatingBasic : MonoBehaviour
{
    public float baseSize;

    public GameObject[] prefabs;

    public int step;
    // Start is called before the first frame update
    void Start()
    {
        int startValue = 0;
        for (int x = 0; x < step; x++)
        {
            for (int z = 0; z < step; z++)
            {
                startValue = GetNextPrefab(startValue);
                Instantiate(prefabs[startValue], new Vector3(x * baseSize + 20, 0, z * baseSize), Quaternion.identity, transform);
            }
        }
    }

    int GetNextPrefab(int value) 
    {
        value++;
        if(value >= prefabs.Length)
            value = 0;
        return value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
