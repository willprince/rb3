using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public static BattleManager bManager;


    public GameObject partyObj, enemiesObj;

    public GameObject[] partyPrefabs = new GameObject[2];

    public GameObject[] enemyPrefabsForest = new GameObject[2];
    public GameObject[] enemyPrefabsMountain = new GameObject[2];
    public GameObject[] enemyPrefabsDesert = new GameObject[2];
    public int numPrefabsForest;
    public int numPrefabsMountain;
    public int numPrefabsDesert;

    public List<Player> players = new List<Player>();
    public List<Enemy> enemies = new List<Enemy>();

    public Vector3 pBasePos1, pBasePos2, pAdvancePos1, pAdvancePos2;

    public Player curPlayer;
    public Enemy target, curEnemy;
    public bool playerTurn, subMenu, skillMenu;
    public int enemyCounter, enemyNum, enemyTurnIndex, playerTurnIndex, playerNum;

    // Start is called before the first frame update
    void Start()
    {
        bManager = this;
        subMenu = false;
        skillMenu = false;
        //Testing Remove after

        InstantiateEnemyArea();
        InstantiateParty();
        enemyCounter = -1;
        playerNum = 2;
    }

    private void InstantiateEnemyArea()
    {
        if (PermManager.AreaFlag < 3)
        {
            InstantiateEnemies(enemyPrefabsForest, numPrefabsForest);
        }
        else if (PermManager.AreaFlag < 5)
        {
            InstantiateEnemies(enemyPrefabsMountain, numPrefabsMountain);
        }
        else if (PermManager.AreaFlag < 6)
        {
            InstantiateEnemies(enemyPrefabsDesert, numPrefabsDesert);
        }
    }

    public void PerformAction(int code)
    {
        if (playerTurn)
        {
            switch (code)
            {
                case 0:
                    if (subMenu)
                    {
                        if (skillMenu)
                        {
                            UseSkill(0);
                            EndPlayerTurn();
                        }
                        else
                        {
                            Play();
                        }
                    }
                    else
                    {
                        if (curPlayer.stats.name == "mage")
                        {
                            target.fireHit.Play("Base Layer.Hit", 0, 0.25f);
                        }
                        else
                        {
                            target.swordHit.Play("Base Layer.Hit", 0, 0.25f);
                        }
                        curPlayer.animator.Play("layer.NormalAttack", 0, 0.25f);
                        DamageEnemy(curPlayer.CalcAttackDamage() * RhythmManager.rManager.multiplier);
                        EndPlayerTurn();
                    }
                    break;
                case 1:
                    if (subMenu)
                    {
                        if (skillMenu)
                        {
                            //UseSkill(1);

                            if (curPlayer.stats.name == "mage")
                            {
                                for (int x = 0; x < enemies.Count; x++)
                                {
                                    enemies[x].fireHit.Play("Base Layer.Hit", 0, 0.25f);
                                    enemies[x].TakeDamage(curPlayer.CalcAttackDamage());
                                }
                            }
                            else
                            {
                                for (int x = 0; x < enemies.Count; x++)
                                {
                                    enemies[x].swordHit.Play("Base Layer.Hit", 0, 0.25f);
                                    enemies[x].TakeDamage(curPlayer.CalcAttackDamage());
                                }
                            }

                            EndPlayerTurn();
                        }
                        else
                        {
                            Items();
                        }
                    }
                    else
                    {
                        OpenSkillMenu();
                    }
                    break;
                case 2:
                    if (subMenu)
                    {
                        if (skillMenu)
                        {
                            if (curPlayer.stats.name == "mage")
                            {
                                target.zapHit.Play("Base Layer.Hit", 0, 0.25f);
                                DamageEnemy(150);
                            }
                            else
                            {
                                UseSkill(2);
                            }
                            
                            EndPlayerTurn();
                        }
                        else
                        {
                            Run();
                        }
                    }
                    else
                    {
                        OpenMenuMenu();
                    }
                    break;
                case 3:
                    if (subMenu)
                    {
                        ReturnToMonkey();
                    }
                    else
                    {
                        ChangeTarget();
                    }
                    break;
            }
        }
    }

    public void ReturnToMonkey()
    {
        subMenu = false;
        skillMenu = false;
        RhythmManager.rManager.ChangeActivators(0, curPlayer);
    }

    public void OpenSkillMenu()
    {
        subMenu = true;
        skillMenu = true;
        RhythmManager.rManager.ChangeActivators(1, curPlayer);
    }

    public void OpenMenuMenu()
    {
        subMenu = true;
        RhythmManager.rManager.ChangeActivators(2, curPlayer);
    }

    public void Play()
    {
        RhythmManager.rManager.WasteTime();
    }

    public void Items()
    {
        RhythmManager.rManager.ItemText();
    }

    public void Run()
    {
        float percentage = (curPlayer.stats.agility / 20) * 100;
        int randomNum = Random.Range(0, 100);
        if (randomNum < percentage)
        {
            EndCombat();
        }
        else
        {
            RhythmManager.rManager.CannotRun();
            EndPlayerTurn();
        }
    }

    public void UseSkill(int code)
    {
        curPlayer.UseSkill(code);
        ReturnToMonkey();
    }

    public void DamageEnemy(float damage)
    {
        target.TakeDamage((damage * (float)(1 + curPlayer.stats.damageEmplify)) * RhythmManager.rManager.multiplier);
    }

    public void StartPlayerTurn()
    {
        playerTurn = true;
        playerTurnIndex = 0;
        SetPlayerTurn(players[playerTurnIndex]);
    }

    public void SetPlayerTurn(Player who)
    {
        curPlayer = who;
        enemyCounter = -1;
        ChangeTarget();
        curPlayer.MoveForward();
    }

    public void EndPlayerTurn()
    {
        RhythmManager.rManager.ResetBeatCounter();
        curPlayer.BackToDefault();
        ReturnToMonkey();
        playerTurnIndex++;
        if (playerTurnIndex < playerNum)
        {
            SetPlayerTurn(players[playerTurnIndex]);
        }
        else
        {
            PermManager.pManager.CheckBuffed();
            PermManager.pManager.CheckBuffedBerserk();
            StartEnemyTurn();
        }
    }

    public void ChangeTarget()
    {
        enemyCounter++;
        if (enemyCounter > 0)
        {
            target.RemoveTarget();
        }
        target = enemies[(enemyCounter % enemyNum)];
        target.SetTarget();
    }

    public void StartEnemyTurn()
    {
        playerTurn = false;
        enemyTurnIndex = 0;
        SetEnemyTurn(enemies[enemyTurnIndex]);
    }

    public void SetEnemyTurn(Enemy who)
    {
        curEnemy = who;
        target.RemoveTarget();
        RhythmManager.rManager.SpawnEnemyNote();
    }

    public void DoEnemyAction()
    {
        int i = Random.Range(0, players.Count);
        Player enemyTarget = players[i];
        curEnemy.animator.Play("Base Layer.NormalAttack", 0, 0.25f);
        enemyTarget.TakeDamage(curEnemy.GetAttack());
        enemyTarget.animator.Play("layer.Hit", 0, 0.25f);
        enemyTurnIndex++;
        if (enemyTurnIndex < enemyNum)
        {
            SetEnemyTurn(enemies[enemyTurnIndex]);
        }
        else
        {
            StartPlayerTurn();
        }
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
        if (players.Count == 1)
        {
            StartPlayerTurn();
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        enemy.RemoveTarget();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        curPlayer.ReceiveExp(target.stats.exp);
        enemyNum--;
        if (enemyNum == 0)
        {
            EndCombat();
        }
        else
        {
            ChangeTarget();
            enemies.Remove(enemy);
        }
    }

    public void RemovePlayer(Player player)
    {
        if ((playerNum - 1) == 0)
        {
            EndCombat();
        }
        else
        {
            players.Remove(player);
            playerNum--;
        }
    }

    private void EndCombat()
    {
        if (playerNum > 0)
        {
            players.RemoveRange(0, playerNum);
        }
        if (enemyNum > 0)
        {
            enemies.RemoveRange(0, enemyNum);
        }
        PermManager.pManager.ExitCombat();
    }

    private void InstantiateParty()
    {

        GameObject gObj = Instantiate(partyPrefabs[0], partyObj.transform);
        Player player = gObj.GetComponent<Player>();
        Animator animator = gObj.GetComponent<Animator>();
        Vector3 defaultPos = new Vector3(0f, 0f, 0f);
        Vector3 forwardPos = new Vector3(0f, 0f, 0f);
        PlayerStat stats = new PlayerStat(0, 0, 0, 0, 0, 0, 0, 0, "", "");

        defaultPos = new Vector3(9.3f, -1.6f, 0f);
        forwardPos = new Vector3(7.3f, -1.6f, 0f);
        stats = PermManager.partyMem1;

        Player player1 = player.InstantiatePlayer(stats, defaultPos, forwardPos, animator);

        gObj = Instantiate(partyPrefabs[1], partyObj.transform);
        player = gObj.GetComponent<Player>();
        animator = gObj.GetComponent<Animator>();
        defaultPos = new Vector3(0f, 0f, 0f);
        forwardPos = new Vector3(0f, 0f, 0f);
        stats = new PlayerStat(0, 0, 0, 0, 0, 0, 0, 0, "", "");

        defaultPos = new Vector3(9.4f, -3.9f, 0f);
        forwardPos = new Vector3(7.4f, -3.9f, 0f);
        stats = PermManager.partyMem2;

        Player player2 = player.InstantiatePlayer(stats, defaultPos, forwardPos, animator);

        player1.setPartyMember(player2);
        player2.setPartyMember(player1);


        //for (int x = 0; x < 2; x++)
        //{
        //    GameObject gObj = Instantiate(partyPrefabs[x], partyObj.transform);
        //    Player player = gObj.GetComponent<Player>();
        //    Animator animator = gObj.GetComponent<Animator>();
        //    Vector3 defaultPos = new Vector3(0f, 0f, 0f);
        //    Vector3 forwardPos = new Vector3(0f, 0f, 0f);
        //    PlayerStat stats = new PlayerStat(0, 0, 0, 0, 0, 0, 0, 0, "", "");
        //    switch (x)
        //    {
        //        case 0:
        //            defaultPos = new Vector3(9.3f, -1.6f, 0f);
        //            forwardPos = new Vector3(7.3f, -1.6f, 0f);
        //            stats = PermManager.partyMem1;
        //            break;
        //        case 1:
        //            defaultPos = new Vector3(9.4f, -3.9f, 0f);
        //            forwardPos = new Vector3(7.4f, -3.9f, 0f);
        //            stats = PermManager.partyMem2;
        //            break;
        //    }
        //        player.InstantiatePlayer(stats, defaultPos, forwardPos, animator);
        //}
    }

    private void InstantiateEnemies(GameObject[] enemyPrefabs, int enemyPreNum)
    {
        enemyNum = Random.Range(1, 4);
        for (int x = 0; x < enemyNum; x++)
        {
            int ranNum = Random.Range(0, enemyPreNum);
            Vector3 position = new Vector3(0f, 0f, 0f);
            switch (x)
            {
                case 0:
                    position = new Vector3(-6.9f, -2.9f, 0f);
                    break;
                case 1:
                    position = new Vector3(-9.3f, -1.2f, 0f);
                    break;
                case 2:
                    position = new Vector3(-9.2f, -4.8f, 0f);
                    break;
            }
            Instantiate(enemyPrefabs[ranNum], position, Quaternion.identity, enemiesObj.transform);
        }
    }
}
