using UnityEngine;
using System.Collections;
using WorldObjects.Players;

namespace WorldObjects.Enemies
{
    public class FlankEnemy : DefenseEnemy
    {
        protected override void Move()
        {
            if (reachedTarget)
            {
                WalkTo(Player.Current.transform.position);
                LookAt(Player.Current.transform.position);
            }
            else
            {
                base.Move();
            }
        }
    }
}