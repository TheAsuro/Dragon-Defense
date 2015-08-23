using UnityEngine;
using System.Collections;
using WorldObjects.Players;

namespace WorldObjects.Enemies
{
    public class SwordEnemy : Enemy
    {
        [SerializeField]
        protected float attackDamage = 10f;
        [SerializeField]
        protected float attackChargeTime = 1f;

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

        protected override void Tick()
        {
            base.Tick();

            if (Weapon.CollidingCreatures.Contains(Player.Current.gameObject))
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
