using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour {

    [HideInInspector]
    public Path path;

	public Color anchorCol = Color.red;
    public Color controlCol = Color.white;
    public Color segmentCol = Color.green;
    public Color selectedSegmentCol = Color.yellow;
    public float anchorDiameter = .1f;
    public float controlDiameter = .075f;
    public bool displayControlPoints = true;

    public float spacing = 0.25f;

    private Vector2[] pathPoints = null;
    private float[] pathLengths = null;

    public void CreatePath()
    {
        path = new Path(transform.position);
    } 

    void Reset()
    {
        CreatePath();
    }

    public Vector2 GetStartPoint()
    {
        if (pathPoints == null) PathInit();
        return pathPoints[0];
    }

    public Quaternion GetStartRotation()
    {
        if (pathPoints == null) PathInit();
        var dir = pathPoints[1] - pathPoints[0];
        return Quaternion.LookRotation(transform.forward, new Vector2(-dir.y, dir.x));
    }

    public float[] GetEvenlySpacedLengths()
    {
        if (pathLengths == null) PathInit();
        return pathLengths;
    }

    public Vector2[] GetEvenlySpacedPoints()
    {
        if (pathPoints == null) PathInit();
        return pathPoints;
    }

    private void PathInit()
    {
        pathPoints = path.CalculateEvenlySpacedPoints(spacing);
        pathLengths = new float[pathPoints.Length];

        for (int i = 0; i < pathPoints.Length; i++)
        {
            if (i == 0) pathLengths[0] = 0;
            else pathLengths[i] = pathLengths[i - 1] + (pathPoints[i - 1] - pathPoints[i]).magnitude;
        }
    }
}
