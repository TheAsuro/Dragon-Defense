using UnityEngine;
using System.Collections;
using WorldObjects.Players;

namespace WorldObjects.Enemies
{
    public class AggressiveEnemy : SwordEnemy
    {
        protected override void Move()
        {
            WalkTo(Player.Current.transform.position);
            LookAt(Player.Current.transform.position);
        }
    }
}