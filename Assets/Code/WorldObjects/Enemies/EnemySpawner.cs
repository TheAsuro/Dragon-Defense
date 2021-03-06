﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using GameInfo;

namespace WorldObjects.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        private static List<EnemySpawner> allSpawners = new List<EnemySpawner>();
        public static List<EnemySpawner> AllSpawners { get { return new List<EnemySpawner>(allSpawners); } }

        private static int maxLevelNumber = 0;
        public static int MaxLevelNumber { get { return maxLevelNumber; } }

        [SerializeField]
        private List<GameObject> enemyObjects = new List<GameObject>();

        [SerializeField]
        private TextAsset spawnFile;

        private List<Dictionary<float, SpawnInfo>> spawnData = new List<Dictionary<float, SpawnInfo>>();
        private bool spawning;

        void Start()
        {
            allSpawners.Add(this);
            ParseSpawnFile();
        }

        public void Reset()
        {
            spawnData.ForEach((data) =>
            {
                foreach (SpawnInfo info in data.Values)
                    info.spawned = false;
            });
        }

        public void StartSpawning()
        {
            spawning = true;
        }

        public void StopSpawning()
        {
            spawning = false;
        }

        private void ParseSpawnFile()
        {
            Dictionary<float, SpawnInfo> levelData = new Dictionary<float, SpawnInfo>();

            foreach (string line in spawnFile.text.Split('\n'))
            {
                string tLine = line.Trim();

                // Ignore comments
                if (tLine.StartsWith("#") || tLine.Equals(""))
                    continue;

                // New level
                if (tLine.StartsWith("-"))
                {
                    if (levelData.Count > 0)
                        spawnData.Add(levelData);

                    levelData = new Dictionary<float, SpawnInfo>();
                }
                else
                {
                    // Line inside a level
                    SpawnInfo info = new SpawnInfo(tLine);
                    levelData.Add(info.time, info);
                }
            }

            if (levelData.Count > 0)
                spawnData.Add(levelData);

            if (maxLevelNumber < spawnData.Count)
                maxLevelNumber = spawnData.Count;
        }

        void Update()
        {
            if (spawning)
            {
                foreach (KeyValuePair<float, SpawnInfo> pair in spawnData[Status.Level])
                {
                    if (Status.GameTime >= pair.Key && !pair.Value.spawned)
                    {
                        pair.Value.SetSpawned(true);
                        for (int i = 0; i < pair.Value.count; i++)
                        {
                            Spawn(GetPrefabByName(pair.Value.type));
                        }
                    }
                }
            }
        }

        private GameObject GetPrefabByName(string name)
        {
            foreach (GameObject obj in enemyObjects)
            {
                if (obj.name.Equals(name))
                    return obj;
            }
            throw new System.Exception("Prefab not found!");
        }

        private void Spawn(GameObject enemy)
        {
            GameInfo.Status.EnemySpawned((GameObject)GameObject.Instantiate(enemy, transform.position, Quaternion.identity));
        }

        public bool FinishedLevel
        {
            get
            {
                float lastSpawnTime = 0f;

                foreach (float spawnTime in spawnData[Status.Level].Keys)
                    if (spawnTime > lastSpawnTime)
                        lastSpawnTime = spawnTime;

                return Status.GameTime >= lastSpawnTime;
            }
        }
    }

    public class SpawnInfo
    {
        public SpawnInfo(string text)
        {
            string[] data = text.Split('|');
            if (data.Length < 3)
                throw new System.InvalidOperationException("Invalid format!");

            spawned = false;

            if (!float.TryParse(data[0], out time))
                throw new System.InvalidCastException("Couldn't parse time!");
            if (!int.TryParse(data[1], out count))
                throw new System.InvalidCastException("Couldn't parse count!");
            type = data[2];
        }

        public bool spawned;
        public void SetSpawned(bool value) { spawned = value; }
        public float time;
        public int count;
        public string type;
    }
}
