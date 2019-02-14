using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour {

    public GameObject objectToSpawn;

    public void Perform()
    {
        ObjectPooler.InstantiatePooled(objectToSpawn, gameObject.transform.position, Quaternion.identity);
    }
}
