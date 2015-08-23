using UnityEngine;
using System.Collections;

namespace WorldObjects
{
    public class AggressiveEnemy : Enemy
    {
        [SerializeField]
        private float attackDamage = 10f;
        [SerializeField]
        private float attackChargeTime = 1f;

        [SerializeField]
        private AudioClip attackSound;

        private CreatureSound sound;
        private WeaponAttack Weapon { get { return transform.GetChild(0).GetChild(0).GetComponent<WeaponAttack>(); } }

        private float attackChargeStart;
        private bool chargingAttack = false;

        void Start()
        {
            sound = new CreatureSound(GetComponent<AudioSource>());
        }

        protected override void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.Player.Current.transform.position, 0.1f);
            CreatureUtils.LookAtPosition(transform, Player.Player.Current.transform.position, Vector3.forward);
        }

        protected override void Tick()
        {
            base.Tick();

            if (Weapon.CollidingCreatures.Contains(Player.Player.Current.gameObject))
            {
                if (!chargingAttack)
                {
                    chargingAttack = true;
                    attackChargeStart = Time.time;
                }
            }
            else
            {
                chargingAttack = false;
            }

            if (Time.time >= attackChargeStart + attackChargeTime && chargingAttack)
            {
                Attack();
                chargingAttack = false;
            }
        }

        protected virtual void Attack()
        {
            sound.PlaySound(attackSound);
            foreach (GameObject creature in Weapon.CollidingCreatures)
            {
                creature.GetComponent<Creature>().Damage(attackDamage, DamageType.Sword);
            }
        }
    }
}