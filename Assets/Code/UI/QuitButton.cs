using UnityEngine;
using System.Collections;

namespace UI
{
    public class QuitButton : ButtonScript
    {
        protected override void Click()
        {
            Application.Quit();
        }
    }
}