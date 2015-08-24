﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameInfo;

namespace UI
{
    public class LevelButton : ButtonScript
    {
        protected override void Click()
        {
            Status.NextLevel();
        }
    }
}