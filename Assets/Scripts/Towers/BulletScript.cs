using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VanishScript))]
public class BulletScript : MonoBehaviour
{
    public int damage;
    public float speed;
    public float lifeTime;

    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public GameObject source;

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
    
    protected virtual void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }
}
