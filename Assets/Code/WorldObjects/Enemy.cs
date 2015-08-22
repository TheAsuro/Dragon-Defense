using UnityEngine;
using System.Collections;

namespace WorldObjects
{
    public class Enemy : Creature
    {
        protected virtual void Move() { }

        protected override void Tick()
        {
            Move();
        }
    }
}