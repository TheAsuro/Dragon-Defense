using UnityEngine;
using System.Collections.Generic;
using Utils;

namespace WorldObjects
{
    public class WeaponAttack : CollisionList
    {
        public List<GameObject> CollidingCreatures
        {
            get
            {
                return CollidingObjects.FindAll((obj) =>
                {
                    if (obj != null)
                        return obj.GetComponent<Creature>() != null;
                    else
                        return false;
                });
            }
        }
    }
}
