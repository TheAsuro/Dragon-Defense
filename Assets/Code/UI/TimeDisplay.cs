using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameInfo;

public class TimeDisplay : MonoBehaviour
{
    private Text Txt { get { return GetComponent<Text>(); } }
	
	void Update ()
    {
        Txt.text = System.Math.Round(Status.GameTime, 2).ToString();
	}
}
