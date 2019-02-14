using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlaneScript : MonoBehaviour
{
    private Health _health;
    private GameObject _leftFire;
    private GameObject _rightFire;

    public float oneFireThreshold = 0.66f;
    public float twoFireThreshold = 0.33f;

    private void OnEnable()
    {
        _health = gameObject.GetComponent<Health>();
        _leftFire = transform.Find("LeftFire").gameObject;
        _rightFire = transform.Find("RightFire").gameObject;
        Debug.Assert(_leftFire != null);
        Debug.Assert(_rightFire != null);
    }

    private void Update()
    {
        var healthRatio = _health.GetHealth() / (float)_health.startingHealth;
        if (healthRatio > oneFireThreshold)
        {
            _leftFire.SetActive(false);
            _rightFire.SetActive(false);
        }
        else if (healthRatio < twoFireThreshold)
        {
            _leftFire.SetActive(true);
            _rightFire.SetActive(true);
        }
        else
        {
            if (_leftFire.activeInHierarchy || _rightFire.activeInHierarchy) return;
            int r = Random.Range(0, 1);
            if (r == 0) _leftFire.SetActive(true);
            else _rightFire.SetActive(true);
        }
    }
}
