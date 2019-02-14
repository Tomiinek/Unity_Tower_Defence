using UnityEngine;
using System.Collections;

public class ParticleSystemAutoDestroyScript : MonoBehaviour
{
    private ParticleSystem ps;
    private AudioSource aus;

    void OnEnable()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        ps.Play();
        aus = gameObject.GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (ps)
        {
            if (ps.IsAlive() || (aus != null && aus.isPlaying)) return;
            gameObject.SetActive(false);
        }
    }
}