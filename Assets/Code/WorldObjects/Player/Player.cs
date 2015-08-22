using UnityEngine;
using System.Collections;

namespace WorldObjects.Player
{
    public class Player : Creature
    {
        private static Player current;
        public static Player Current { get { return current; } }

        void Start()
        {
            current = this;
        }

        protected override void Die()
        {
            GameInfo.Status.DeathScreen();
        }
    }
}
