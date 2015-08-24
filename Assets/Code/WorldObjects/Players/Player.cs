using UnityEngine;
using System.Collections;
using WorldObjects.Enemies;

namespace WorldObjects.Players
{
    public class Player : Creature
    {
        private static Player current;
        public static Player Current { get { return current; } }

        [SerializeField]
        private float biteDamage = 100f;
        [SerializeField]
        private float biteCooldown = 0.33f;

        [SerializeField]
        private float maxFireCharge = 100f;
        [SerializeField]
        private float fireUsePerTick = 10f;
        [SerializeField]
        private float fireRechargeAmount = 0.5f;
        [SerializeField]
        private float fireCharge = 100f;
        [SerializeField]
        private Vector3 fireOffset = new Vector3(0, 1, 0);
        [SerializeField]
        private GameObject firePrefab;

        [SerializeField]
        private AudioClip fireSound;
        [SerializeField]
        private AudioClip biteSound;

        private bool firePressCancelled = false;

        private float biteCooldownStart;
        private bool biteOnCooldown = false;
        public bool BiteOnCooldown { get { return biteOnCooldown; } }

        private CreatureSound sound;
        private PlayerBite BiteObj { get { return transform.GetChild(0).GetComponent<PlayerBite>(); } }

        public float FireCharge { get { return fireCharge; } }
        public float MaxFireCharge { get { return maxFireCharge; } }

        private float defaultFireCharge;
        private float defaultHealth;

        void Start()
        {
            current = this;
            sound = new CreatureSound(GetComponent<AudioSource>());

            defaultFireCharge = fireCharge;
            defaultHealth = health;

            GameInfo.Status.StartPlay();
        }

        public override void Die()
        {
            GameInfo.Status.DeathScreen();
        }

        protected override void Tick()
        {
            base.Tick();
        }

        void Update()
        {
            UpdateBite();
            UpdateFire();
        }

        public void Reset()
        {
            fireCharge = defaultFireCharge;
            health = defaultHealth;
        }

        private void UpdateBite()
        {
            if (Input.GetButtonDown("Bite"))
            {
                biteOnCooldown = true;
                biteCooldownStart = Time.time;
                Bite();
            }

            if (Time.time >= biteCooldownStart + biteCooldown)
            {
                biteOnCooldown = false;
            }

            if (biteOnCooldown)
                GetComponent<DragonControls>().Frozen = true;
            else
                GetComponent<DragonControls>().Frozen = false;
        }

        private void Bite()
        {
            sound.PlaySound(biteSound);
            foreach (GameObject enemy in BiteObj.CollidingEnemies)
            {
                enemy.GetComponent<Enemy>().Damage(biteDamage, DamageType.Bite);
            }
        }

        private void UpdateFire()
        {
            if (Input.GetButtonDown("Fire"))
            {
                sound.PlaySound(fireSound);
            }

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
