using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WorldObjects
{
    public class EnemySpawner : MonoBehaviour
    {
        private static List<EnemySpawner> allSpawners = new List<EnemySpawner>();
        public static List<EnemySpawner> AllSpawners { get { return new List<EnemySpawner>(allSpawners); } }

        // Lists so unity can serialize
        [SerializeField]
        private List<GameObject> spawnEnemies = new List<GameObject>();
        [SerializeField]
        private List<int> spawnEnemyCount = new List<int>();
        [SerializeField]
        private List<float> spawnEnemyTimes = new List<float>();
        [SerializeField]
        private List<bool> didSpawnEnemies = new List<bool>();

        private float startTime;

        public int Count { get { return spawnEnemies.Count; } }

        void Start()
        {
            allSpawners.Add(this);
        }

        void OnDestroy()
        {
            allSpawners.Remove(this);
        }

        public void StartSpawning()
        {
            startTime = Time.time;
        }

        public void AddEntry(GameObject enemy, int count, float time)
        {
            spawnEnemies.Add(enemy);
            spawnEnemyCount.Add(count);
            spawnEnemyTimes.Add(time);
            didSpawnEnemies.Add(false);
        }

        public void RemoveEntryAt(int index)
        {
            spawnEnemies.RemoveAt(index);
            spawnEnemyCount.RemoveAt(index);
            spawnEnemyTimes.RemoveAt(index);
            didSpawnEnemies.RemoveAt(index);
        }

        public GameObject GetEnemyAt(int index)
        {
            return spawnEnemies[index];
        }

        public void SetEnemyAt(int index, GameObject enemy)
        {
            spawnEnemies[index] = enemy;
        }

        public int GetCountAt(int index)
        {
            return spawnEnemyCount[index];
        }

        public void SetCountAt(int index, int count)
        {
            spawnEnemyCount[index] = count;
        }

        public float GetTimeAt(int index)
        {
            return spawnEnemyTimes[index];
        }

        public void SetTimeAt(int index, float time)
        {
            spawnEnemyTimes[index] = time;
        }

        void Update()
        {
            float spawnTime = Time.time - startTime;

            for (int i = 0; i < spawnEnemies.Count; i++)
            {
                if (!didSpawnEnemies[i] && spawnTime >= spawnEnemyTimes[i])
                {
                    didSpawnEnemies[i] = true;
                    for (int spawnCount = 0; spawnCount < spawnEnemyCount[i]; spawnCount++)
                    {
                        Spawn(spawnEnemies[i]);
                    }
                }
            }
        }

        private void Spawn(GameObject enemy)
        {
            GameObject.Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }

    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EnemySpawner spawner = (EnemySpawner)target;

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Type | Count | Time");
            for (int i = 0; i < spawner.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                spawner.SetEnemyAt(i, (GameObject)EditorGUILayout.ObjectField(spawner.GetEnemyAt(i), typeof(GameObject), false));
                spawner.SetCountAt(i, EditorGUILayout.IntField(spawner.GetCountAt(i)));
                spawner.SetTimeAt(i, EditorGUILayout.FloatField(spawner.GetTimeAt(i)));
                if (GUILayout.Button("-"))
                {
                    spawner.RemoveEntryAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }
            if (GUILayout.Button("+"))
            {
                spawner.AddEntry(null, 1, 0f);
            }
            EditorGUILayout.EndVertical();
        }
    }
}
