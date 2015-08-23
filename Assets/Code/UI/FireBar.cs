using UnityEngine;
using System.Collections;

namespace UI
{
    public class FireBar : Bar
    {
        void Update()
        {
            Value = WorldObjects.Players.Player.Current.FireCharge / WorldObjects.Players.Player.Current.MaxFireCharge;
        }
    }
}
