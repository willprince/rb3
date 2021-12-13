using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedTextSetter : MonoBehaviour
{
    public Text speedText;
    // Start is called before the first frame update
    void Start()
    {
        speedText.text = PermManager.pManager.speedMultiplier.ToString();
    }
}
