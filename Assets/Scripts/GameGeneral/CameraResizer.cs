using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResizer : MonoBehaviour {

    public float cameraSizeOverHeightTimesWidth;

    private float _width;
    private float _height;

    private Camera _camera;

    void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
        Resize();
    }

    void Update()
    {
        if (Screen.height != _height || _width != Screen.width)
        {
            Resize();
        }
    }

    private void Resize()
    {
        _width = Screen.width;
        _height = Screen.height;
        _camera.orthographicSize = cameraSizeOverHeightTimesWidth / Screen.width * Screen.height;
    }
}
