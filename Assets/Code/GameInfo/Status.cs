using UnityEngine;
using System.Collections.Generic;
using WorldObjects.Enemies;

namespace GameInfo
{
    public enum State
    {
        Playing,
        DeathScreen
    }

    public static class Status
    {
        private static State curState = State.Playing;
        public static State CurrentState { get { return curState; } }
        public static float GameTime { get { return Time.time - startTime; } }

        private static float startTime = float.NaN;
        private static List<GameObject> livingEnemies = new List<GameObject>();

        public static void StartPlay()
        {
            startTime = Time.time;

            foreach (EnemySpawner spawner in EnemySpawner.AllSpawners)
            {
                spawner.StartSpawning();
            }
        }

        public static void EnemySpawned(GameObject enemy)
        {
            if (!livingEnemies.Contains(enemy))
                livingEnemies.Add(enemy);

            UpdateLevelStatus();
        }

        public static void EnemiyDied(GameObject enemy)
        {
            if (livingEnemies.Contains(enemy))
                livingEnemies.Remove(enemy);

            UpdateLevelStatus();
        }

        private static void UpdateLevelStatus()
        {
            if (livingEnemies.Count == 0 && EnemySpawner.AllSpawners.TrueForAll((spawner) => { return spawner.FinishedLevel; }))
                NextLevel();
        }

        private static void NextLevel()
        {
            
        }

        public static void DeathScreen()
        {
            curState = State.DeathScreen;
            ResetLevel();
        }

        public static void ResetLevel()
        {
            curState = State.Playing;
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}