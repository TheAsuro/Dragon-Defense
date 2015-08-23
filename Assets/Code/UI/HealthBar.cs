using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UI
{
    public class HealthBar : Bar
    {
        void Update()
        {
            Value = WorldObjects.Players.Player.Current.Health / 100f;
        }
    }
}
