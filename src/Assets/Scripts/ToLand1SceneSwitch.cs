using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PermManager;

public class ToLand1SceneSwitch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene(5);
        PermManager.AreaFlag = 5;

        // Set starting position
        PermManager.pManager.setLastPosition(new Vector2(475,-188));

        Debug.Log("Setting scene to: " + PermManager.AreaFlag);
    }
}
