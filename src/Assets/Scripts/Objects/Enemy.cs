using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public HealthBar healthBar;
    public GameObject target;
    public EnemyStat stats;
    public Animator animator;
    public Animator fireHit;
    public Animator swordHit;
    public Animator zapHit;
    // Start is called before the first frame update
    void Start()
    {
        BattleManager.bManager.AddEnemy(this);
        healthBar.SetMaxHealth(stats.maxHealth);
        healthBar.SetHealth(stats.health);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }


    public float GetAttack()
    {
        float statUsed = 1f;
        switch (stats.strategie)
        {
            case 0:
                statUsed = stats.strength;
                break;
            case 1:
                statUsed = stats.agility;
                break;
            case 2:
                statUsed = stats.inteligence;
                break;
        }
        return (Mathf.Ceil((stats.baseDamage * statUsed) / 2));
    }

    public void SetTarget()
    {
        target.SetActive(true);
    }

    public void RemoveTarget()
    {
        target.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        stats.health -= damage;
        if (stats.health <= 0)
        {
            Die();
        }
        else
        {
            healthBar.SetHealth(stats.health);
        }
    }

    public void Die()
    {
        BattleManager.bManager.RemoveEnemy(this);
        gameObject.SetActive(false);
    }
}
