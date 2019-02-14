using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestroyScript))]
[RequireComponent(typeof(Health))]
public class EnemyScript : MonoBehaviour
{
    private GameManager _manager;
    private MoneyScript _money;
    private Health _enemyHealth;

    public int strength;
    public int reward;

    void OnEnable()
    {
        _enemyHealth = GetComponent<Health>();
        _enemyHealth.Initialize();
        var go = GameObject.Find("GameManager");
        _money = go.GetComponent<MoneyScript>();
        _manager = go.GetComponent<GameManager>();
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    private void Update()
    {
        if (_enemyHealth.isDead)
        {
            _money.AddBudget(reward);
            _money.CreateCoin(transform.position);
            Destroy();
        }
    }

    public void Destroy(bool silent = false)
    {  
        gameObject.SetActive(false);
        _manager.RemoveEnemy();
        if (silent) return;
        var ds = gameObject.GetComponent<DestroyScript>();
        ds.Perform();      
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var bullet = other.gameObject.GetComponent<BulletScript>();
        if (bullet == null) return;
        _enemyHealth.TakeDamage(bullet.damage);
        bullet.Destroy();
    }
}
