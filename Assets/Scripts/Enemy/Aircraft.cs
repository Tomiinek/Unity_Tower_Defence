using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : WalkerScript
{
    private GameObject _plane;
    private GameObject _shadow;

    public float corpusRotationSpeed;
    public float maxRotationDegrees;

    protected override void OnEnable()
    {
        base.OnEnable();
        _plane = transform.Find("Plane").gameObject;
        _shadow = transform.Find("Shadow").gameObject;
    }

    protected override void Move(Vector2 position, float angle)
    {
        transform.position = position;
        transform.Rotate(0, 0, angle);

        _shadow.transform.position = transform.position + new Vector3(40, 30, 0);

        var currentAngle = _plane.transform.localEulerAngles.x;
        if (currentAngle > 180) currentAngle -= 360;

        var desiredAngle = 100 * angle;
        if (desiredAngle < -maxRotationDegrees) desiredAngle = -maxRotationDegrees;
        if (desiredAngle > maxRotationDegrees) desiredAngle = maxRotationDegrees;

        _plane.transform.Rotate(Vector3.right * corpusRotationSpeed * (desiredAngle - currentAngle));
    }

}
