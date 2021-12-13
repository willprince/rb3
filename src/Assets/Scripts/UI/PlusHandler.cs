using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlusHandler : MonoBehaviour
{
    public Text speedText;

    public void ClickPlus()
    {
        if (PermManager.pManager.speedMultiplier < 5)
        {
            PermManager.pManager.speedMultiplier += 1;
            speedText.text = PermManager.pManager.speedMultiplier.ToString();
        }
    }
}
