using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PermManager;

public class CharactersPortraitUpdate : MonoBehaviour
{

    void Start()
    {
        SetPortrait();
        UpdateHpText();
    }

    public static void SetPortrait()
    {
        Text WarriorHP = GameObject.Find("warriorHp").GetComponent<Text>();
        WarriorHP.text = PermManager.partyMem1.health + "/" + PermManager.partyMem1.maxHealth;

        Text MageHp = GameObject.Find("mageHp").GetComponent<Text>();
        MageHp.text = PermManager.partyMem2.health + "/" + PermManager.partyMem2.maxHealth;
    }

    private void Update()
    {
        if (PermManager.pManager.leveledUp)
        {
            PermManager.pManager.leveledUp = false;
            UpdateHpText();
        }
    }

    public void UpdateHpText()
    {
        Text WarriorHP = GameObject.Find("warriorHp").GetComponent<Text>();
        WarriorHP.text = PermManager.partyMem1.health + "/" + PermManager.partyMem1.maxHealth;

        Debug.Log(PermManager.partyMem2.health);
        Text MageHp = GameObject.Find("mageHp").GetComponent<Text>();
        MageHp.text = PermManager.partyMem2.health + "/" + PermManager.partyMem2.maxHealth;
    }
}