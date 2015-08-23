using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UI
{
    public class BiteStatus : MonoBehaviour
    {
        private Image Img { get { return GetComponent<Image>(); } }
        private Color imgColor;

        void Start()
        {
            imgColor = Img.color;
        }

        void Update()
        {
            if (WorldObjects.Players.Player.Current.BiteOnCooldown)
                Img.color = Color.black;
            else
                Img.color = imgColor;
        }
    }
}