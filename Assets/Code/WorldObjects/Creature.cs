using UnityEngine;
using System.Collections.Generic;

namespace WorldObjects
{
    public class Creature : MonoBehaviour
    {
        [SerializeField]
        protected float health = 100f;
        [SerializeField]
        protected bool invulnerable = false;
        [SerializeField]
        protected List<DamageType> damageResists = new List<DamageType>();

        private bool tookDamageThisTick = false;

        public float Health { get { return health; } }
        public bool Invulnerable
        {
            get { return invulnerable; }
            set { invulnerable = value; }
        }

        protected void Damage(float value, DamageType type)
        {
            if (!invulnerable && !tookDamageThisTick && !damageResists.Contains(type))
            {
                health += value;
                if (health <= 0)
                    Die();

                tookDamageThisTick = true;
            }
        }

        void FixedUpdate()
        {
            tookDamageThisTick = false;
            Tick();
        }

        protected virtual void Tick() { }

        protected virtual void Die()
        {
            GameObject.Destroy(gameObject);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            DamageObject dmgObj = other.GetComponent<DamageObject>();
            if (dmgObj)
                Damage(dmgObj.DamagePerTick, dmgObj.DamageType);
        }
    }
}
