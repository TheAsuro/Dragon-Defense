using UnityEngine;
using System.Collections.Generic;

namespace WorldObjects
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private List<Enemy> enemyTypes = new List<Enemy>();

        [SerializeField]
        private List<float> enemySpawnRates = new List<float>();

        void Start()
        {
            RandomSpawn();
        }

        private void RandomSpawn()
        {
            float rnd = Random.Range(0f, 1f);
            print(rnd);
            for (int i = 0; i < enemySpawnRates.Count; i++)
            {
                rnd -= enemySpawnRates[i];
                if (rnd <= 0f)
                    Spawn(enemyTypes[i]);
            }
        }

        private void Spawn(Enemy enemy)
        {
            GameObject.Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
}
