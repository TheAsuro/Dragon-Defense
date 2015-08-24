using UnityEngine;
using System.Collections.Generic;
using WorldObjects.Enemies;
using WorldObjects.Players;
using UI;

namespace GameInfo
{
    public static class Status
    {
        public static float GameTime { get { return Time.time - startTime; } }
        public static int Level { get { return level; } }

        private static float startTime = float.NaN;
        private static int level = 0;
        private static List<GameObject> livingEnemies = new List<GameObject>();

        public static void StartPlay()
        {
            ResumeGame();

            startTime = Time.time;
            Player.Current.Reset();

            livingEnemies.ForEach((enemy) => enemy.GetComponent<Enemy>().Die());

            foreach (EnemySpawner spawner in EnemySpawner.AllSpawners)
            {
                spawner.Reset();
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
                LevelScreen();
        }

        private static void LevelScreen()
        {
            PauseGame();
            UiManager.Manager.ShowLevelScreen = true;
        }

        public static void DeathScreen()
        {
            PauseGame();
            UiManager.Manager.ShowDeathScreen = true;
        }

        public static void NextLevel()
        {
            level++;
            StartPlay();
        }

        private static void PauseGame()
        {
            Time.timeScale = 0f;
        }

        private static void ResumeGame()
        {
            Time.timeScale = 1f;
            UiManager.Manager.ShowLevelScreen = false;
            UiManager.Manager.ShowDeathScreen = false;
        }
    }
}