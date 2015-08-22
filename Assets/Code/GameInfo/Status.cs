using UnityEngine;
using System.Collections;

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