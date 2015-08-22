using UnityEngine;
using System.Collections;

namespace WorldObjects.Player
{
    public class DragonControls : MonoBehaviour
    {
        [SerializeField]
        private float maxMoveSpeed = 0.4f;
        [SerializeField]
        private float biteDamage = 100f;

        [SerializeField]
        private Vector3 fireOffset = new Vector3(0, 1, 0);
        [SerializeField]
        private GameObject firePrefab;

        GameObject Player { get { return gameObject; } }
        Vector2 InputDirection { get { return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); } }
        PlayerBite Bite { get { return transform.GetChild(0).GetComponent<PlayerBite>(); } }

        void FixedUpdate()
        {
            // Move with wasd
            if (InputDirection != Vector2.zero)
            {
                Player.transform.Translate(new Vector3(InputDirection.x * maxMoveSpeed, InputDirection.y * maxMoveSpeed, 0f), Space.World);
            }

            // Look at mouse
            CreatureUtils.LookAtMouse(transform);

            // Attack
            if (Input.GetButton("Bite"))
            {
                foreach (GameObject enemy in Bite.CollidingEnemies)
                {
                    enemy.GetComponent<Enemy>().Damage(biteDamage, DamageType.Bite);
                }
            }

            if (Input.GetButton("Fire"))
            {
                SpawnFire();
            }
        }

        private void SpawnFire()
        {
            GameObject.Instantiate(firePrefab, Player.transform.position + (Player.transform.rotation * fireOffset), Player.transform.rotation);
        }
    }
}
