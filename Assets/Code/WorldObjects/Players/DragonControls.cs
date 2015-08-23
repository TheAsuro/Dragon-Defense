using UnityEngine;
using System.Collections;

namespace WorldObjects.Players
{
    public class DragonControls : MonoBehaviour
    {
        [SerializeField]
        private float maxMoveSpeed = 0.4f;

        private bool frozen = false;
        public bool Frozen
        {
            get { return frozen; }
            set { frozen = value; }
        }

        Vector2 InputDirection { get { return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); } }

        void FixedUpdate()
        {
            // Move with wasd
            if (InputDirection != Vector2.zero && !frozen)
            {
                Player.Current.transform.Translate(new Vector3(InputDirection.x * maxMoveSpeed, InputDirection.y * maxMoveSpeed, 0f), Space.World);
            }

            // Look at mouse
            CreatureUtils.LookAtMouse(transform);
        }
    }
}
