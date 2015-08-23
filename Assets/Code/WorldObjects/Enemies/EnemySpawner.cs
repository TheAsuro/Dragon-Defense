using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WorldObjects.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        private Dictionary<float, SpawnInfo> spawnData = new Dictionary<float,SpawnInfo>();

        void Start()
        {
            SpawnInfo test = new SpawnInfo("0|1|AggressiveEnemy1");
            spawnData.Add(test.)
        }

        void Update()
        {
            foreach (KeyValuePair<float, SpawnInfo> pair in spawnData)
            {

            }
        }

        private void Spawn(GameObject enemy)
        {
            GameObject.Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }

    public struct SpawnInfo
    {
        public SpawnInfo(string text)
        {
            string[] data = text.Split('|');
            if (data.Length < 3)
                throw new System.Exception();

            spawned = false;

            if (!float.TryParse(data[0], out time))
                throw new System.Exception();
            if (!int.TryParse(data[1], out count))
                throw new System.Exception();
            type = data[2];
        }

        public bool spawned;
        public float time;
        public int count;
        public string type;
    }
}
