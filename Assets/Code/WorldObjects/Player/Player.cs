using UnityEngine;
using System.Collections;

namespace WorldObjects.Player
{
    public class Player : Creature
    {
        void Start()
        {
            PlayerData.Player = this;
        }

        protected override void Die()
        {
            print("you ded");
        }
    }

    public static class PlayerData
    {
        private static Player player;

        public static Player Player
        {
            get { return player; }
            set { player = value; }
        }
    }
}
