using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : BulletScript
{
    public float rotationSpeed;

    protected override void Update()
    {
        if (target == null || !target.activeInHierarchy)
        {
            if (source == null || !source.activeInHierarchy)
            {
                Destroy();
                return;
            }
            target = source.GetComponent<TowerScript>().GetMostDistantInRange();
            if (target == null)
            {
                transform.Translate(0, speed * Time.deltaTime, 0);
                return;
            }
        }

        var change = target.transform.position - transform.position;

        var angle = Vector3.Angle(change, transform.up);
        if (Vector3.Cross(change, transform.up).z > 0) angle *= -1;

        transform.Rotate(0, 0, angle * Time.deltaTime * rotationSpeed);

        change = Vector3.RotateTowards(transform.up, change, Mathf.PI / 180 * Time.deltaTime * speed, float.MaxValue);
        change.Normalize();
        change *= Time.deltaTime * speed;
        transform.position += change;
    }
}
