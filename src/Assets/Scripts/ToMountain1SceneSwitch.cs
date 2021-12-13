using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PermManager;

public class ToMountain1SceneSwitch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene(3);
        PermManager.AreaFlag = 3;

        // Set starting position
        PermManager.pManager.setLastPosition(new Vector2(494,59));
        
        Debug.Log("Setting scene to: " + PermManager.AreaFlag);
    }
}
