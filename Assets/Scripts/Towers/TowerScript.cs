using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TowerScript : MonoBehaviour {
    
    public GameObject bulletPrefab;
    public int cost;
    public float rotationSpeed;

    public float cooldown;
    private float _currentCooldown;

    public AudioClip gunShoot;
    private AudioSource _aus;

    private HashSet<GameObject> _currentTargets;
    private GameObject _gun;

    private void OnEnable()
    {
        _currentCooldown = cooldown;
        _currentTargets = new HashSet<GameObject>();
        _gun = transform.Find("Gun").gameObject;
        Debug.Assert(_gun != null);
        _aus = gameObject.GetComponent<AudioSource>();
    }

    public void Destroy()
    {
        _currentTargets = null;
        _gun = null;
        gameObject.SetActive(false); 
    }

    void Update()
    {
        GameObject target = GetMostDistantInRange();

        if (target == null) return;

        var change = target.transform.position - _gun.transform.position;
        var angle = Vector3.Angle(change, _gun.transform.up);
        if (Vector3.Cross(change, _gun.transform.up).z > 0) angle *= -1;
        _gun.transform.Rotate(0, 0, angle * Time.deltaTime * rotationSpeed);

        if (_currentCooldown > 0.0f) _currentCooldown -= Time.deltaTime;
        else
        {
            _currentCooldown = cooldown;
            Shoot(target);
        }
    }

    public GameObject GetMostDistantInRange()
    {
        var toRemove = new List<GameObject>();
        GameObject target = null;
        float maxDistance = 0;

        foreach (var enemy in _currentTargets)
        {
            if (!enemy.activeInHierarchy)
            {
                toRemove.Add(enemy);
                continue;
            }

            var es = enemy.GetComponent<WalkerScript>();
            if (maxDistance < es.GetProgress())
            {
                maxDistance = es.GetProgress();
                target = enemy;
            }
        }

        for (int i = 0; i < toRemove.Count; i++)
        {
            _currentTargets.Remove(toRemove[i]);
        }

        return target;
    }

    void OnTriggerEnter2D(Collider2D other)
    {  
        _currentTargets.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _currentTargets.Remove(other.gameObject);
    }

    public virtual void Shoot(GameObject target)
    {
        System.Action<BulletScript> bulletSetup = (b) =>
        {
            b.target = target;
            b.source = gameObject;
        };
        _aus.PlayOneShot(gunShoot);
        ObjectPooler.InstantiatePooled<BulletScript>(bulletPrefab, _gun.transform.position, _gun.transform.rotation, bulletSetup);

    }
}
