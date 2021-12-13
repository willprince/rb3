using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinusHandler : MonoBehaviour
{
    public Text speedText;

    public void ClickMinus()
    {
        if (PermManager.pManager.speedMultiplier > 1)
        {
            PermManager.pManager.speedMultiplier -= 1;
            speedText.text = PermManager.pManager.speedMultiplier.ToString();
        }
    }
}
