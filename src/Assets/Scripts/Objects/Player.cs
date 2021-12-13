using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthBar healthBar;

    public Player partyMember;

    public PlayerStat stats;

    public Canvas levelUp;

    public Animator animator;

    public ProjectilBehavior Fire1;

    public Transform LaunchOffset;

    public Animator healAnimator;

    public AudioSource healsource2;

    public Animator shieldAnimator;

    public Animator berserkAnimator;

    private Vector3 defaultPos, forwardPos;
    // Start is called before the first frame update
    void Start()
    {
        BattleManager.bManager.AddPlayer(this);
    }

    public void setPartyMember(Player partyMember)
    {
        this.partyMember = partyMember;
        if (stats.skillNum == 0)
        {
            GiveSkills();
        }
        else
        {
            stats.ResetSkillPlayer(this, partyMember);
        }
    }

    public Player InstantiatePlayer(PlayerStat stats, Vector3 defaultPos, Vector3 forwardPos, Animator animator)
    {
        this.stats = stats;
        this.animator = animator;
        levelUp.gameObject.SetActive(false);
        SetHealth();
        SetPoses(defaultPos, forwardPos);
        

        return this;
    }

    public void GiveSkills()
    {
        stats.AddSkill(this, "heal", partyMember);
        stats.AddSkill(this, "buff", partyMember);
        stats.AddSkill(this, "ouchie", partyMember);
    }

    public void SetHealth()
    {
        healthBar.SetMaxHealth(stats.maxHealth);
        healthBar.SetHealth(stats.health);
    }

    public void SetPoses(Vector3 defaultPos, Vector3 forwardPos)
    {
        this.defaultPos = defaultPos;
        BackToDefault();
        this.forwardPos = forwardPos;
    }

    public float CalcAttackDamage()
    {
        return (Mathf.Ceil((stats.baseDamage * stats.strength) / 2));
    }

    public void MoveForward()
    {
        transform.localPosition = forwardPos; 
    }

    public void BackToDefault()
    {
        transform.localPosition = defaultPos;
    }

    public void UseSkill(int skill)
    {
        if ((skill + 1) <= stats.skillNum)
        {
            stats.skills[skill].action();
        }
    }

    public void ReceiveExp(int exp)
    {
        stats.exp += exp;
        if (stats.CheckLevelUp())
        {
            StartCoroutine("LevelUpUI");
            stats.LevelUp();
            stats.health = stats.maxHealth;
            SetHealth();
        }
    }

    private IEnumerator LevelUpUI()
    {
        levelUp.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        levelUp.gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        float percent = (stats.defence / 100) * RhythmManager.rManager.multiplier;
        int realDamage = Mathf.CeilToInt(damage - (damage * percent));
        stats.health -= (int)((float)realDamage * (1 - stats.damageReduction));
        if (stats.health <= 0)
        {
            stats.health = 1;
            Die();
        }
        else
        {
            healthBar.SetHealth(stats.health);
        }
    }

    public void Die()
    {
        BattleManager.bManager.RemovePlayer(this);
        gameObject.SetActive(false);
    }

    public void Revive()
    {
        BattleManager.bManager.AddPlayer(this);
        gameObject.SetActive(true);
    }

    public void NormalAttack()
    {
        if(this.stats.name == "mage")
        {
            Debug.Log("Mage normal attack");
            Instantiate(Fire1, LaunchOffset.position, transform.rotation);
        }
    }
}
