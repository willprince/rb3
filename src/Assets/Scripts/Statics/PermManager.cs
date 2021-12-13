using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PermManager : MonoBehaviour
{
    public static PlayerStat partyMem1;
    public static PlayerStat partyMem2;
    private bool buffed1, buffed2;
    private int buffCounter1, buffCounter2;
    private bool buffedBerserk1, buffedBerserk2;
    private int buffBerserkCounter1, buffBerserkCounter2;
    public static PermManager pManager;
    public bool leveledUp;
    public int speedMultiplier;
    public static int AreaFlag;
    private bool CombatFlag;

    public static Animator buffMem1Animator;
    public static Animator buffMem2Animator;

    public static Animator buffBerserkMem1Animator;
    public static Animator buffBerserkMem2Animator;


    public static Vector2 lastPosition = new Vector2(0,0); 
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        CreateDude();
        CreateDudette();
        speedMultiplier = 4;
        pManager = this;
    }

    public void SetBuffTimer(PlayerStat affectedPlayer)
    {
        if (affectedPlayer == partyMem1)
        {
            buffed1 = true;
            buffCounter1 = 0;
            Debug.Log("P1 buffed");
        }
        else if (affectedPlayer == partyMem2)
        {
            buffed2 = true;
            buffCounter2 = 0;
            Debug.Log("P2 buffed");
        }
    }

    public void SetBuffBerserkTimer(PlayerStat affectedPlayer)
    {
        if (affectedPlayer == partyMem1)
        {
            buffedBerserk1 = true;
            buffBerserkCounter1 = 0;
            Debug.Log("P1 buffed");
        }
    }

    public void CheckBuffed()
    {
        if (buffed1)
        {
            if (buffCounter1 >= 3)
            {
                Debug.Log("Buff ending p1");
                EndBuff(partyMem1);
            }
            else
            {
                buffCounter1++;
            }
        }
        if (buffed2)
        {
            if (buffCounter2 >= 3)
            {
                Debug.Log("Buff ending p2");
                EndBuff(partyMem2);
            }
            else
            {
                buffCounter2++;
            }
        }
    }

    public void CheckBuffedBerserk()
    {
        if (buffedBerserk1)
        {
            if (buffBerserkCounter1 >= 3)
            {
                Debug.Log("Buff ending p1");
                EndBuffBerserk(partyMem1);
            }
            else
            {
                buffBerserkCounter1++;
            }
        }
    }

    private void EndBuff(PlayerStat stat)
    {
        stat.damageReduction = 0;
        if (buffMem1Animator)
        {

            buffMem1Animator.Play("Base Layer.Nothing", 0, 0.25f);
        }
        if (buffMem2Animator)
        {

            buffMem2Animator.Play("Base Layer.Nothing", 0, 0.25f);
        }
        
        Debug.Log("Damage reduction back to " + stat.damageReduction +  " percent");
    }

    private void EndBuffBerserk(PlayerStat stat)
    {
        stat.damageEmplify = 0;
        if (buffBerserkMem1Animator)
        {
            buffBerserkMem1Animator.Play("Base Layer.Nothing", 0, 0.25f);
        }
        Debug.Log("Damage emplify back to " + stat.damageEmplify + " percent");
    }

    private void CreateDude()
    {
        PlayerStat dude = new PlayerStat(25, 9, 4, 6, 10, 450, 90, 1, "melee1", "warrior");
        partyMem1 = dude;
        
    }

    private void CreateDudette()
    {
        PlayerStat dudette = new PlayerStat(30, 7, 3, 9, 6, 500, 0, 1, "fire1", "mage");
        partyMem2 = dudette;
    }

    public void ExitCombat()
    {
        CombatFlag = false;
        if (buffed1)
        {
            EndBuff(partyMem1);
        }
        if (buffed2)
        {
            EndBuff(partyMem2);
        }
        ChangeScene();
        SceneManager.UnloadSceneAsync("BattleScene");
    }

    public void EnterCombat()
    {
        CombatFlag = true;
        ChangeScene();
    }

    public void LoadNextScene()
    {
        AreaFlag--;
        ChangeScene();
    }

    public void LoadPrevScene()
    {
        AreaFlag++;
        ChangeScene();
    }

    private void ChangeScene()
    {

        if (CombatFlag)
        {
            SceneManager.LoadSceneAsync("BattleScene");
            return;
        }
        switch (AreaFlag)
        {
            case 1:
                SceneManager.LoadSceneAsync("World_Forest1");
                break;
            case 2:
                SceneManager.LoadSceneAsync("World_Forest2");
                break;
            case 3:
                SceneManager.LoadSceneAsync("World_Mountain1");
                break;
            case 4:
                SceneManager.LoadSceneAsync("World_Mountain2");
                break;
            case 5:
                SceneManager.LoadSceneAsync("World_Sand1");
                break;
        }
    }

    public void setLastPosition(Vector2 position){

        lastPosition = position;
    }

    public void setAreaFlag(int sceneNumber){

        AreaFlag = sceneNumber;
    }
}
