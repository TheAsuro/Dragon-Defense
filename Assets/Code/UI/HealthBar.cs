using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    private RectTransform RT { get { return GetComponent<RectTransform>(); } }

    void Update()
    {
        RT.anchorMax = new Vector2(WorldObjects.Player.Player.Current.Health / 100f, RT.anchorMax.y);
    }
}
