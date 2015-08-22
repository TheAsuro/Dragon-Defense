using UnityEngine;
using System.Collections;

namespace UI
{
    public class Bar : MonoBehaviour
    {
        private RectTransform RT { get { return GetComponent<RectTransform>(); } }

        public float Value
        {
            get { return RT.anchorMax.x; }
            set { RT.anchorMax = new Vector2(value, RT.anchorMax.y); }
        }
    }
}
