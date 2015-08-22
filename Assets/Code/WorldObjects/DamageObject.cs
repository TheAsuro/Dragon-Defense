using UnityEngine;
using System.Collections;

namespace WorldObjects
{
    public enum DamageType
    {
        Fire
    }

    public class DamageObject : MonoBehaviour
    {
        [SerializeField]
        private float damagePerTick = 0.1f;
        [SerializeField]
        private DamageType damageType;

        public float DamagePerTick { get { return -damagePerTick; } }
        public DamageType DamageType { get { return damageType; } }
    }
}