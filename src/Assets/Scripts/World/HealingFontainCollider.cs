using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PermManager;
using static CharactersPortraitUpdate;

public class HealingFontainCollider : MonoBehaviour
{
    public Animator healAnimator;

    void OnTriggerEnter2D(Collider2D col)
    {
        PermManager.partyMem1.healToMaxHealth();
        PermManager.partyMem2.healToMaxHealth();
        CharactersPortraitUpdate.SetPortrait();
        healAnimator.Play("Base Layer.Run", 0, 0.25f);
    }
}
