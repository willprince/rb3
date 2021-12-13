using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PermManager;

public class toCombatScene : MonoBehaviour
{

    private bool insideTile = true;


    void OnTriggerEnter2D(Collider2D col)
    {
        // Randomly check if we enter in a figth
        if(UnityEngine.Random.Range(0, 60) == 1){

            Debug.Log("Entering figth");

            PermManager.pManager.EnterCombat();
        }
        
    }

}
