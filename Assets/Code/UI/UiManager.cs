using UnityEngine;
using System.Collections;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        private static UiManager manager;
        public static UiManager Manager { get { return manager; } }

        private GameObject LevelScreen { get { return transform.FindChild("LevelScreen").gameObject; } }
        private GameObject DeathScreen { get { return transform.FindChild("DeathScreen").gameObject; } }
        private GameObject EndScreen { get { return transform.FindChild("EndScreen").gameObject; } }

        void Start()
        {
            manager = this;
        }

        public bool ShowLevelScreen
        {
            get { return LevelScreen.activeSelf; }
            set { LevelScreen.SetActive(value); }
        }

        public bool ShowDeathScreen
        {
            get { return DeathScreen.activeSelf; }
            set { DeathScreen.SetActive(value); }
        }

        public bool ShowEndScreen
        {
            get { return EndScreen.activeSelf; }
            set { EndScreen.SetActive(value); }
        }
    }
}