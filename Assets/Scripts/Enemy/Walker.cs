using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : WalkerScript
{
    public float walkingAngle;
    public float walkingFrequency;

    private float _walkingSinOffset;

    protected override void OnEnable()
    {
        base.OnEnable();
        _walkingSinOffset = Random.Range(0.0f, 2 * Mathf.PI);
    }

    protected override void Move(Vector2 position, float angle)
    {
        transform.position = position;
        transform.Rotate(0, 0, angle + Mathf.Sin(_walkingSinOffset + walkingFrequency * Time.time) * walkingAngle);
    }
}
