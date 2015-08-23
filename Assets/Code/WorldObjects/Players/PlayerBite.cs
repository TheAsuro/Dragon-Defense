using UnityEngine;
using System.Collections.Generic;
using Utils;
using WorldObjects.Enemies;

namespace WorldObjects.Players
{
    public class PlayerBite : CollisionList
    {
        public List<GameObject> CollidingEnemies
        {
            get
            {
                return CollidingObjects.FindAll((obj) =>
                {
                    if (obj != null)
                        return obj.GetComponent<Enemy>() != null;
                    else
                        return false;
                });
            }
        }
    }
}