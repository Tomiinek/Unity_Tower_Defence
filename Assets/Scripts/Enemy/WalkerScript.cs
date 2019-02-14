using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerScript : MonoBehaviour {

    public float movementSpeed;
    public float rotationSpeed;
    public PathCreator pathCreator;

    protected float movementProgress;

    protected virtual void OnEnable()
    {
        movementProgress = 0.0f;
    }

    protected virtual void Update()
    {
        var pathLengths = pathCreator.GetEvenlySpacedLengths();

        movementProgress += movementSpeed * Time.deltaTime / pathLengths[pathLengths.Length - 1];
        var newPosition = TargetPoint(movementProgress); 

        var change = newPosition - (Vector2)transform.position;
        var angle = Vector3.Angle(change, transform.right);
        if (Vector3.Cross(change, transform.right).z > 0) angle *= -1;

        Move(newPosition, angle * Time.deltaTime * rotationSpeed);
    }


    protected virtual void Move(Vector2 position, float angle)
    {
        transform.position = position;
        transform.Rotate(0, 0, angle);
    }

    public float GetProgress()
    {
        return movementProgress;
    }

    public Vector2 TargetPoint(float u)
    {
        var pathLengths = pathCreator.GetEvenlySpacedLengths();
        var pathPoints = pathCreator.GetEvenlySpacedPoints();

        var targetLength = u * pathLengths[pathLengths.Length - 1];
        var low = 0;
        var high = pathPoints.Length;
        var index = 0;

        while (low < high)
        {
            index = low + (((high - low) / 2) | 0);
            if (pathLengths[index] < targetLength) low = index + 1;
            else high = index;
        }

        if (pathLengths[index] > targetLength) index--;
        var lengthBefore = pathLengths[index];
        if (lengthBefore == targetLength || index == pathLengths.Length - 1) return pathPoints[index];
        else return pathPoints[index] + ((targetLength - lengthBefore) / (pathLengths[index + 1] - lengthBefore) * (pathPoints[index + 1] - pathPoints[index]));
    }
}
