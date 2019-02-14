using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishScript : MonoBehaviour {

    public float duration;

    private float time;
    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        Begin();
    }

    public void Begin()
    {
        if (duration == 0.0f) return;
        time = 0;
        Invoke("Destroy", duration);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    public void Destroy()
    {
        gameObject.SetActive(false);

        var color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;
    }

    void Update()
    {
        time += Time.deltaTime;
        
        var opacity = Mathf.Cos(Mathf.PI / 2.0f * time / duration);
        
        var color = spriteRenderer.color;
        color.a = opacity;
        spriteRenderer.color = color;
    }
}
