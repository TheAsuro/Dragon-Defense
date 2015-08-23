using UnityEngine;
using System.Collections;
using WorldObjects.Players;

namespace WorldObjects.Enemies
{
    public class DefenseEnemy : SwordEnemy
    {
        [SerializeField]
        protected Vector3 targetPosition = new Vector3();

        protected bool reachedTarget = false;

        protected override void Move()
        {
            if (WalkTo(targetPosition))
                reachedTarget = true;
            LookAt(Player.Current.transform.position);
        }
    }
}