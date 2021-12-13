using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyStat
{
    public Boss()
    {
        baseDamage = 50;
        strength = 20;
        inteligence = 8;
        agility = 12;
        health = 1400;
        maxHealth = 1400;
        strategie = 0;
        exp = 600;
    }
}
