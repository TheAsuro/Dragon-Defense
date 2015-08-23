using UnityEngine;
using System.Collections;
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

        public static void StartPlay()
        {
            startTime = Time.time;

            foreach (EnemySpawner spawner in EnemySpawner.AllSpawners)
            {
                spawner.StartSpawning();
            }
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