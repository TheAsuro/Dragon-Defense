using UnityEngine;
using System.Collections;

namespace WorldObjects
{
    public class AggressiveEnemy : Enemy
    {
        protected override void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.PlayerData.Player.transform.position, 0.1f);
        }
    }
}