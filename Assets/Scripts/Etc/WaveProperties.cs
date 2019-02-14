using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "Wave.asset", menuName = "Level/Wave")]
public class WaveProperties : ScriptableObject
{
    [System.Serializable]
    public class Subwave
    {
        public int size;
        public GameObject Enemy;
        public float enemyTime;
        public float subwaveTime;
    }
  
    public Subwave[] subwave;
    public float waveTime;
}
