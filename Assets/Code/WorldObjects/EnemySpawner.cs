using UnityEngine;
using System.Collections.Generic;

namespace WorldObjects
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> enemyPrefabs = new List<GameObject>();

        [SerializeField]
        private List<float> enemySpawnRates = new List<float>();

        [SerializeField]
        private float spawnRate = 1f;

        private float lastSpawn = -1f;

        void Start()
        {
            RandomSpawn();
        }

        void Update()
        {
            if (Time.time - lastSpawn >= spawnRate)
            {
                RandomSpawn();
                lastSpawn = Time.time;
            }
        }

        private void RandomSpawn()
        {
            float rnd = Random.Range(0f, 1f);
            for (int i = 0; i < enemySpawnRates.Count; i++)
            {
                rnd -= enemySpawnRates[i];
                if (rnd <= 0f)
                    Spawn(enemyPrefabs[i]);
            }
        }

        private void Spawn(GameObject enemy)
        {
            GameObject.Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
}
