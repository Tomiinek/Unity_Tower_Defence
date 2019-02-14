using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    // https://answers.unity.com/questions/932497/object-pooling-different-gameobjects.html

    static private ObjectPooler _singleton = null;
    static private ObjectPooler singleton
    {
        get
        {
            if (_singleton == null) Instantiate();
            return _singleton;
        }
    }

    static private void Instantiate()
    {
        new GameObject("#ObjectPooler", typeof(ObjectPooler));
    }

    void Awake()
    {
        if (_singleton == null) _singleton = this;
        else if (_singleton != this) Destroy(this);
    }

    private Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>();

    private GameObject AddInstanceToPool(GameObject argPrefab)
    {
        int instanceID = argPrefab.GetInstanceID();

        argPrefab.SetActive(false);
        GameObject instance = Instantiate(argPrefab);
        argPrefab.SetActive(true);

        instance.name = argPrefab.name;

        if (pool.ContainsKey(instanceID) == false)
        {
            pool.Add(instanceID, new Queue<GameObject>());
        }
        pool[instanceID].Enqueue(instance);
        return instance;
    }

    private GameObject GetPooledInstance(GameObject argPrefab, Vector3 argPosition, Quaternion argRotation)
    {
        int instanceID = argPrefab.GetInstanceID();
        GameObject instance = null;
        if (pool.ContainsKey(instanceID) && pool[instanceID].Count != 0)
        {
            for (int i = 0; i < pool[instanceID].Count; ++i)
            {
                if (pool[instanceID].Peek().activeSelf == false)
                {
                    instance = pool[instanceID].Dequeue();
                    pool[instanceID].Enqueue(instance);
                    break;
                }
                else pool[instanceID].Enqueue(pool[instanceID].Dequeue());
            }  
        }
        
        if (instance == null)
        {
            instance = AddInstanceToPool(argPrefab);
        }

        instance.transform.position = argPosition;
        instance.transform.rotation = argRotation;
        instance.SetActive(true);

        return instance;
    }

    static public GameObject InstantiatePooled(GameObject argPrefab, Vector3 argPosition, Quaternion argRotation)
    {
        return singleton.GetPooledInstance(argPrefab, argPosition, argRotation);
    }

    static public GameObject InstantiatePooled<T>(GameObject argPrefab, Vector3 argPosition, Quaternion argRotation, System.Action<T> argAction)
    {
        GameObject instance = singleton.GetPooledInstance(argPrefab, argPosition, argRotation);
        var c = instance.GetComponent<T>();
        argAction(c);
        return instance;
    }
}
