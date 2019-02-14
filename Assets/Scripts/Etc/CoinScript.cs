using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    public float duration;
    public int price;
    public float maxFlipDegrees;

    private float sinOffset;

    private GameObject coin;
    private GameObject shadow;

    private SpriteRenderer spriteRendererCoin;
    private SpriteRenderer spriteRendererShadow;

    private float time;

    private void OnEnable()
    {
        coin = transform.Find("Coin").gameObject;
        shadow = transform.Find("Shadow").gameObject;

        sinOffset = Random.Range(0, Mathf.PI * 2);

        if (duration == 0.0f) return;
        time = 0;
        Invoke("Destroy", duration);
        spriteRendererCoin = coin.GetComponent<SpriteRenderer>();
        spriteRendererShadow = shadow.GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Update()
    {
        var currentAngle = coin.transform.localEulerAngles.y;
        if (currentAngle > 180) currentAngle -= 360;

        var desiredAngle = Mathf.Sin(sinOffset + 5 * Time.time) * maxFlipDegrees;

        coin.transform.Rotate(Vector3.up * (desiredAngle - currentAngle));
        shadow.transform.Rotate(Vector3.up * (desiredAngle - currentAngle));

        time += Time.deltaTime;

        var opacity = Mathf.Cos(Mathf.PI / 2.0f * time / duration);

        var color = spriteRendererCoin.color;
        color.a = opacity;
        spriteRendererCoin.color = color;

        color = spriteRendererShadow.color;
        color.a = opacity;
        spriteRendererShadow.color = color;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);

        var color = spriteRendererCoin.color;
        color.a = 1;
        spriteRendererCoin.color = color;

        color = spriteRendererShadow.color;
        color.a = 1;
        spriteRendererShadow.color = color;
    }

}
