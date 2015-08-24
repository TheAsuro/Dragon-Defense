using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameInfo;

namespace UI
{
    public class DeathButton : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Click);
        }

        private void Click()
        {
            Status.StartPlay();
        }
    }
}