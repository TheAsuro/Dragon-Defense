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

        public void Damage(float value, DamageType type)
        {
            if (!invulnerable && !tookDamageThisTick && !damageResists.Contains(type))
            {
                health -= value;
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

    public static class CreatureUtils
    {
        public static void LookAtMouse(Transform transform)
        {
            Plane gamePlane = new Plane(Vector3.forward, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist = 0f;
            if (gamePlane.Raycast(ray, out dist))
            {
                Vector3 hitPoint = ray.GetPoint(dist);
                LookAtPosition(transform, hitPoint, gamePlane.normal);
            }
        }

        public static void LookAtPosition(Transform transform, Vector3 target, Vector3 planeNormal)
        {
            Vector3 vectorToHit = target - transform.position;

            float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(vectorToHit.normalized, Vector3.up))
                * Mathf.Sign(Vector3.Dot(planeNormal, Vector3.Cross(vectorToHit, Vector3.up))) * -1;

            if (!float.IsNaN(angle))
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        }
    }

    public class CreatureSound
    {
        private float startVolume;
        private AudioSource audio;

        public CreatureSound(AudioSource source)
        {
            audio = source;
            startVolume = source.volume;
        }

        public void PlaySound(AudioClip clip)
        {
            audio.volume = startVolume;
            audio.PlayOneShot(clip);
        }
    }
}
