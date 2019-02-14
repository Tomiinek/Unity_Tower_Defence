using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(GameManager))]
public class PlayerHealth : Health
{
    public Text healthText;
    public Image damageImage;
    public float damageImageOpacity;
    public float flashSpeed; 

    private bool _gotDamage;
    private bool _increment;

    private void Update()
    {
        if (!_gotDamage) return;

        var aColor = damageImage.color;
        if (_increment == true) aColor.a += damageImageOpacity * Time.deltaTime / flashSpeed;
        else aColor.a -= damageImageOpacity * Time.deltaTime / flashSpeed;

        if (aColor.a > damageImageOpacity) _increment = false;
        else if (aColor.a < 0f)
        {
            _increment = true;
            _gotDamage = false;
        }

        damageImage.color = aColor;
    }

    void Start()
    {
        _gotDamage = false;
        _increment = true;
        healthText.text = startingHealth.ToString();
    }

    public override void TakeDamage(int amount)
    {
        if (isDead) return;

        damaged = true;
        currentHealth -= amount;

        healthText.text = currentHealth.ToString();
        _gotDamage = true;

        if (currentHealth <= 0 && !isDead) Death();
    }

    protected override void Death()
    {
        isDead = true;

        var manager = gameObject.GetComponent<GameManager>();
        manager.GameOver();
    }
}