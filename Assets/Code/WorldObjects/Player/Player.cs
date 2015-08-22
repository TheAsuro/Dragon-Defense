using UnityEngine;
using System.Collections;

namespace WorldObjects.Player
{
    public class Player : Creature
    {
        private static Player current;
        public static Player Current { get { return current; } }

        [SerializeField]
        private float biteDamage = 100f;

        [SerializeField]
        private float maxFireCharge = 100f;
        [SerializeField]
        private float fireUsePerTick = 10f;
        [SerializeField]
        private float fireRechargeAmount = 0.5f;
        [SerializeField]
        private float fireCharge = 100f;
        [SerializeField]
        private float minRequiredCharge = 30f;
        [SerializeField]
        private Vector3 fireOffset = new Vector3(0, 1, 0);
        [SerializeField]
        private GameObject firePrefab;

        private bool firePressCancelled = false;

        PlayerBite Bite { get { return transform.GetChild(0).GetComponent<PlayerBite>(); } }

        public float FireCharge { get { return fireCharge; } }
        public float MaxFireCharge { get { return maxFireCharge; } }

        void Start()
        {
            current = this;
        }

        protected override void Die()
        {
            GameInfo.Status.DeathScreen();
        }

        protected override void Tick()
        {
            base.Tick();

            UpdateBite();
            UpdateFire();
        }

        private void UpdateBite()
        {
            if (Input.GetButton("Bite"))
            {
                foreach (GameObject enemy in Bite.CollidingEnemies)
                {
                    enemy.GetComponent<Enemy>().Damage(biteDamage, DamageType.Bite);
                }
            }
        }

        private void UpdateFire()
        {
            if (Input.GetButton("Fire") && fireCharge < fireUsePerTick)
            {
                firePressCancelled = true;
            }

            if (Input.GetButton("Fire") && fireCharge > fireUsePerTick && !firePressCancelled)
            {
                fireCharge -= fireUsePerTick;
                GameObject.Instantiate(firePrefab, transform.position + (transform.rotation * fireOffset), transform.rotation);
            }
            else
            {
                fireCharge += fireRechargeAmount;
                if (fireCharge > maxFireCharge)
                    fireCharge = maxFireCharge;
            }

            if (Input.GetButtonUp("Fire"))
            {
                firePressCancelled = false;
            }
        }
    }
}
