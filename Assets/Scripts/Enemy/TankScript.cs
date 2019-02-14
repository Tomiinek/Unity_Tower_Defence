using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class TankScript : MonoBehaviour
{
    private Health _health;
    private GameObject _fire;

    public float fireThreshold = 0.5f;

    private void OnEnable()
    {
        _health = gameObject.GetComponent<Health>();
        _fire = transform.Find("Fire").gameObject;
        Debug.Assert(_fire != null);
    }

    private void Update()
    {
        var healthRatio = _health.GetHealth() / (float)_health.startingHealth;
        if (healthRatio > fireThreshold) _fire.SetActive(false);
        else _fire.SetActive(true);
    }
}
