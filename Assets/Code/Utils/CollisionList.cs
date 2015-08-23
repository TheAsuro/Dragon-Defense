using UnityEngine;
using System.Collections.Generic;

namespace Utils
{
    public class CollisionList : MonoBehaviour
    {
        private List<GameObject> collidingObjects = new List<GameObject>();
        public List<GameObject> CollidingObjects { get { return new List<GameObject>(collidingObjects); } }

        void OnTriggerEnter(Collider coll)
        {
            if (!collidingObjects.Contains(coll.gameObject))
                collidingObjects.Add(coll.gameObject);
        }

        void OnTriggerExit(Collider coll)
        {
            if (collidingObjects.Contains(coll.gameObject))
                collidingObjects.Remove(coll.gameObject);
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            if (!collidingObjects.Contains(coll.gameObject))
                collidingObjects.Add(coll.gameObject);
        }

        void OnTriggerExit2D(Collider2D coll)
        {
            if (collidingObjects.Contains(coll.gameObject))
                collidingObjects.Remove(coll.gameObject);
        }
    }
}
