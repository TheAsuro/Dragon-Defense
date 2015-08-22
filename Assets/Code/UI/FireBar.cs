using UnityEngine;
using System.Collections;

namespace UI
{
    public class FireBar : Bar
    {
        void Update()
        {
            Value = WorldObjects.Player.Player.Current.FireCharge / WorldObjects.Player.Player.Current.MaxFireCharge;
        }
    }
}
