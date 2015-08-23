using UnityEngine;
using System.Collections;

namespace WorldObjects.Enemies
{
    public class Enemy : Creature
    {
        [SerializeField]
        protected float moveSpeed = 0.2f;

        protected virtual void Move() { }

        protected override void Tick()
        {
            Move();
        }

        protected bool WalkTo(Vector3 target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);
            CreatureUtils.LookAtPosition(transform, target, Vector3.forward);

            if (transform.position == target)
                return true;
            else
                return false;
        }

        protected void LookAt(Vector3 target)
        {
            CreatureUtils.LookAtPosition(transform, target, Vector3.forward);
        }
    }
}