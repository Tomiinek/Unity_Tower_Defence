using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class EnemyManager : MonoBehaviour {

    public PlayerHealth playerHealth;
    public GameManager manager;

    public PathCreator[] soldierPaths;
    public PathCreator[] vehiclekPaths;
    public PathCreator[] planePaths;

    public WaveProperties[] waves;
    public bool WavesCompleted { private set; get; }
    public bool SubWavesCompleted { private set; get; }

    public float startTimeOffset;
    private int _waveId;
    private int _subwaveId;
    private int _subwaveProgress;

    private GameObject _subwaveEnemy;
    private PathCreator _subwavePath;

    void Start()
    {
        _waveId = 0;
        _subwaveId = 0;
        _subwaveProgress = 0;
        Invoke("LaunchWave", startTimeOffset);
        WavesCompleted = false;
    }

    void LaunchWave()
    {
        if (_waveId >= waves.Length)
        {
            CancelInvoke();
            WavesCompleted = true;
            return;
        }

        ++_waveId;
        _subwaveId = 0;      
        LaunchSubwave();
    }

    void LaunchSubwave()
    {
        ++_subwaveId;
        _subwaveProgress = 0;

        _subwaveEnemy = waves[_waveId - 1].subwave[_subwaveId - 1].Enemy;
        if (waves[_waveId - 1].subwave[_subwaveId - 1].Enemy.GetComponent<Walker>() != null)
        {
            _subwavePath = soldierPaths[Random.Range(0, soldierPaths.Length)];
        }
        else if (waves[_waveId - 1].subwave[_subwaveId - 1].Enemy.GetComponent<Vehicle>() != null)
        {
            _subwavePath = vehiclekPaths[Random.Range(0, vehiclekPaths.Length)];
        }
        else if (waves[_waveId - 1].subwave[_subwaveId - 1].Enemy.GetComponent<Aircraft>() != null)
        {
            _subwavePath = planePaths[Random.Range(0, planePaths.Length)];
        }

        if (_subwaveId < waves[_waveId - 1].subwave.Length)
        {
            Invoke("LaunchSubwave", waves[_waveId - 1].subwave[_subwaveId - 1].subwaveTime);
        } 
        else
        {
            Invoke("LaunchWave", waves[_waveId - 1].waveTime);
        }

        LaunchEnemy();
    }

    void LaunchEnemy()
    {
        if (playerHealth.isDead) return;

        ++_subwaveProgress;

        manager.AddEnemy();
        System.Action<WalkerScript> enemySetup = (c) => c.pathCreator = _subwavePath;
        ObjectPooler.InstantiatePooled(_subwaveEnemy, _subwavePath.GetStartPoint(), _subwavePath.GetStartRotation(), enemySetup);

        if (_subwaveProgress < waves[_waveId - 1].subwave[_subwaveId - 1].size)
        {
            Invoke("LaunchEnemy", waves[_waveId - 1].subwave[_subwaveId - 1].enemyTime);
        }      
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.gameObject.GetComponent<EnemyScript>();
        playerHealth.TakeDamage(enemy.strength);
        enemy.Destroy(true);
    }
}
