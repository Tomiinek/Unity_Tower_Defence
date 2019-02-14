using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Loading : MonoBehaviour {

    public float timeOffset;
    public TextMeshProUGUI text;

    private string _sceneName;
    private float _dotTime = 0.3f;
    private int _currentDots = 0;

    public void StartGame(string levelName)
    {
        Invoke("Run", timeOffset);

        _currentDots = 0;
        text.text = "";
        InvokeRepeating("UpdateDot", _dotTime, _dotTime);

        _sceneName = levelName;
    }

    private void UpdateDot()
    {
        _currentDots = (_currentDots + 1) % 4;
        string toDisplay = "";
        for (int i = 0; i < _currentDots; i++) toDisplay += ".";
        text.text = toDisplay;
    }

    private void Run()
    {
        CancelInvoke();
        SceneManager.LoadScene(_sceneName);
    }
   
}
