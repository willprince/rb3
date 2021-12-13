using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PermManager;

public class ToMountain2SceneSwitch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene(4);
        PermManager.AreaFlag = 4;

        // Set starting position
        PermManager.pManager.setLastPosition(new Vector2(475,-64));

        Debug.Log("Setting scene to: " + PermManager.AreaFlag);
    }
}
