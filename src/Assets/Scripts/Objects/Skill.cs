using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Skill
{
    public Action action;

    public Player player, partyMember;


    public Skill(Player player, string actionCode, Player partyMember)
    {
        this.player = player;
        this.partyMember = partyMember;

        if (player.stats.name == "mage")
        {
            switch (actionCode)
            {

                case "heal":
                    action = Heal;
                    break;
                case "buff":
                    action = Buff;
                    break;
                case "ouchie":
                    action = Ouchie;
                    break;
            }
        }
        else
        {
            switch (actionCode)
            {

                case "heal":
                    action = Shield;
                    break;
                case "buff":
                    action = Buff;
                    break;
                case "ouchie":
                    action = Buff;
                    break;
            }
        }
    }

    public void Heal()
    {
        player.healAnimator.Play("Base Layer.Run", 0, 0.25f);
        player.animator.Play("layer.NormalAttack", 0, 0.25f);
        partyMember.healAnimator.Play("Base Layer.Run", 0, 0.25f);
        player.healsource2.Play();

        if ((player.stats.health += 50f) > player.stats.maxHealth)
        {
            player.stats.health = player.stats.maxHealth;
            partyMember.stats.health = player.stats.maxHealth;
        }
        else
        {
            player.stats.health += 50f;
            partyMember.stats.health += 50f;
        }
        player.healthBar.SetHealth(player.stats.health);
        partyMember.healthBar.SetHealth(player.stats.health);
    }

    public void Buff()
    {
        player.stats.damageEmplify = 0.30;
        Debug.Log("Buff damage emplify");
        PermManager.pManager.SetBuffBerserkTimer(player.stats);
        player.berserkAnimator.Play("Base Layer.Run", 0, 0.25f);
        PermManager.buffBerserkMem1Animator = player.berserkAnimator;
    }

    public void Ouchie()
    {
        BattleManager.bManager.DamageEnemy(200f);
    }   

    public void Shield()
    {
        Debug.Log("Shielding team");
        player.shieldAnimator.Play("Base Layer.Run", 0, 0.25f);
        partyMember.shieldAnimator.Play("Base Layer.Run", 0, 0.25f);

        player.stats.damageReduction = 0.25;
        partyMember.stats.damageReduction = 0.25;

        PermManager.pManager.SetBuffTimer(player.stats);
        PermManager.buffMem1Animator = player.shieldAnimator;
        PermManager.pManager.SetBuffTimer(partyMember.stats);
        PermManager.buffMem2Animator = partyMember.shieldAnimator;
    }
}
