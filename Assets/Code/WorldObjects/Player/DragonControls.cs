using UnityEngine;
using System.Collections;

namespace WorldObjects.Player
{
    public class DragonControls : MonoBehaviour
    {
        [SerializeField]
        private float maxMoveSpeed = 0.4f;

        [SerializeField]
        private Vector3 fireOffset = new Vector3(0, 1, 0);
        [SerializeField]
        private GameObject firePrefab;

        GameObject Player { get { return gameObject; } }
        Vector2 InputDirection { get { return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); } }

        void FixedUpdate()
        {
            // Move with wasd
            if (InputDirection != Vector2.zero)
            {
                Player.transform.Translate(new Vector3(InputDirection.x * maxMoveSpeed, InputDirection.y * maxMoveSpeed, 0f), Space.World);
            }

            // Look at mouse
            Plane gamePlane = new Plane(Vector3.forward, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist = 0f;
            if (gamePlane.Raycast(ray, out dist))
            {
                Vector3 hitPoint = ray.GetPoint(dist);
                Vector3 vectorToHit = hitPoint - transform.position;

                float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(vectorToHit.normalized, Vector3.up))
                    * Mathf.Sign(Vector3.Dot(gamePlane.normal, Vector3.Cross(vectorToHit, Vector3.up))) * -1;

                if (!float.IsNaN(angle))
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle);
            }

            // Fire
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
