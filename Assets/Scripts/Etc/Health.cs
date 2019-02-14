using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    public int startingHealth = 100;
    protected int currentHealth;

    public bool isDead { get; protected set; }
    protected bool damaged;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        damaged = false;
    }

    public void Initialize()
    {
        currentHealth = startingHealth;
        damaged = false;
        isDead = false;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public virtual void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        if (currentHealth <= 0 && !isDead) Death();
    }

    protected virtual void Death()
    {
        isDead = true;
    }
}