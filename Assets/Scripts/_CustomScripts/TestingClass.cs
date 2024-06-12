using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestingContainer
{
    public string key;
    public object value;
    public bool isSaved;
}

public class TestingClass : MonoBehaviour
{
    public Dictionary<string, object> dic = new Dictionary<string, object>();

    public int id;

    public TestingContainer[] container = new TestingContainer[4];

    public TestingContainer completeContainer = new TestingContainer();
    void Start()
    {
        for (int i = 0; i < container.Length; i++)
        {
            SetObjectToContainer(container[i]);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            UpdateID(1);
        if (Input.GetKeyDown(KeyCode.DownArrow)) 
            UpdateID(-1);
        
        if (Input.GetKeyDown(KeyCode.S))
            Save();
        if (Input.GetKeyDown(KeyCode.L))
        {
            completeContainer.value = dic[container[id].key]; // Get Value

            dic[container[id].key] = "qwe"; //Set new value

            completeContainer.key = container[id].key;
            completeContainer.value = Load(container[id].key);
            print($"Complete key:{completeContainer.key}, complete value:{completeContainer.value}");
        }

        if (Input.GetKeyDown(KeyCode.J))
            ToJcon();

    }

    void ToJcon() 
    {
        string json =  JsonUtility.ToJson(dic);
        print(json);
    }

    void UpdateID(int value) 
    {
        if (id + value > container.Length - 1)
            id = 0;
        else if(id + value < 0)
            id = container.Length - 1;
        else 
            id += value;
    }

    void SetObjectToContainer(TestingContainer container) 
    {
        switch (container.key)
        {
            case "name": container.value = gameObject.name; break;
            case "position": container.value = transform.position.x; break;
            case "scale": container.value = transform.localScale.y; break;
            case "rotation": container.value = transform.localEulerAngles.z; break;
        }
    }

    private void Save()
    {
        dic.Add(container[id].key, container[id].value);
        container[id].isSaved = true;
    }

    private object Load(string key)
    {
        foreach (KeyValuePair<string, object> kvp in dic)
        {
            if (kvp.Key == key)
            {
                object value = kvp.Value;
                return value;
            }
        }

        return null;
    }
}