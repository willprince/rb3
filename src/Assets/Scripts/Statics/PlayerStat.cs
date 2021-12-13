using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    public string name { get; set; }
    public float baseDamage { get; set; }
    public float strength { get; set; }
    public float inteligence { get; set; }
    public float agility { get; set; }
    public float defence { get; set; }
    public float maxHealth { get; set; }
    public float health { get; set; }
    public int exp { get; set; }
    private int levelUpExp { get; set; }
    public short level { get; set; }
    public List<Skill> skills;
    public int skillNum;
    public string normalAttack;
    public double damageReduction;
    public double damageEmplify;

    public PlayerStat(int baseDamage, int strength, int inteligence, int agility, int defence, int health, int exp, short level, string normalAttack, string name)
    {
        this.baseDamage = baseDamage;
        this.strength = strength;
        this.inteligence = inteligence;
        this.agility = agility;
        this.defence = defence;
        this.maxHealth = health;
        this.health = health;
        this.exp = exp;
        this.level = level;
        skills = new List<Skill>();
        this.normalAttack = normalAttack;
        this.name = name;
        this.damageReduction = 0;
        this.damageEmplify = 0;
        skillNum = 0;
        CalcLevelUpExp();
    }

    public void ResetSkillPlayer(Player player, Player partyMember)
    {
        foreach (Skill skill in skills)
        {
            skill.player = player;
            skill.partyMember = partyMember;
        }
    }

    public void AddSkill(Player player, string skillName, Player partyMember)
    {
        if (skillNum + 1 > 3)
        {
            skills.RemoveAt(2);
        }
        else
        {
            skillNum++;
        }
        skills.Add(new Skill(player, skillName, partyMember));
    }

    public void RemoveSkillAt(int index)
    {
        skills.RemoveAt(index);
        skillNum--;
    }

    //public void SetSkillAt(int index, Player player, string skillName)
    //{
    //    skills[index] = new Skill(player, skillName);
    //    skillNum++;
    //}

    public bool CheckLevelUp()
    {
        if (exp > levelUpExp)
        {
            return true;
        }
        return false;
    }

    public void LevelUp()
    {
        if (level < 99)
        {
            level++;
            CalcLevelUpExp();
            BringUpStats();
        }
    }

    public void healToMaxHealth()
    {
        Debug.Log("Setting health");
        this.health = this.maxHealth;
        Debug.Log(this.health);
    }

    private void BringUpStats()
    {
        int num = Random.Range(1, 3);
        strength += num;
        num = Random.Range(1, 3);
        inteligence += num;
        num = Random.Range(1, 3);
        agility += num;
        num = Random.Range(1, 3);
        defence += num;
        num = Random.Range(1, 3);
        maxHealth += (num * 10);
    }

    private void CalcLevelUpExp()
    {
        levelUpExp += levelUpExp + (level * 100);
    }
}
