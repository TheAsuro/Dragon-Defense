using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UI
{
    public class ButtonScript : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Click);
        }

        protected virtual void Click()
        {

        }
    }
}