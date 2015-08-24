using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameInfo;

namespace UI
{
    public class DeathButton : ButtonScript
    {
        protected override void Click()
        {
            Status.StartPlay();
        }
    }
}