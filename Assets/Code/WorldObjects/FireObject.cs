using UnityEngine;
using System.Collections;

namespace WorldObjects
{
    public class FireObject : DamageObject
    {
        private float creationTime;
        private float lifeTime = 3f;

        void Start()
        {
            creationTime = Time.time;
        }

        void Update()
        {
            if (Time.time >= creationTime + lifeTime)
            {
                Die();
            }
        }

        private void Die()
        {
            GameObject.Destroy(gameObject);
        }
    }
}