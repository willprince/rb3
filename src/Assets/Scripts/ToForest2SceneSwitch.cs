using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PermManager;

public class ToForest2SceneSwitch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene(2);

        PermManager.pManager.setAreaFlag(2);

        // Set starting position
        PermManager.pManager.setLastPosition(new Vector2(74,100));

        Debug.Log("Setting scene to: " + PermManager.AreaFlag);
    }
}
