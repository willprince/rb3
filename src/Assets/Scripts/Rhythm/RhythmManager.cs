using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager rManager;

    public GameObject notes, prefabNote, prefabEnemyNote, song;

    public ActivatorObj actAttack, actSkills, actItems, actChange;

    private AudioSource songAudio;

    private IEnumerator menuCoroutine;

    public GameObject[] forestSongs = new GameObject[2];
    public GameObject[] mountainSongs = new GameObject[2];
    public GameObject[] desertSongs = new GameObject[1];

    public int forestTempos;
    public int mountainTempos;
    public int desertTempos;

    public bool hasStarted = false;

    public int combo, multiplier, beatLimit;

    private int distance, posToAdd, tempo, beatCounter, speedMultiplier, basePosition;

    public Text multiText, comboText, flavorText, menuText;

    // Start is called before the first frame update
    void Start()
    {
        rManager = this;
        beatCounter = 0;
        speedMultiplier = PermManager.pManager.speedMultiplier;
        distance = 1 * speedMultiplier;
        basePosition = 5 * speedMultiplier;
        notes.transform.position = new Vector3 (basePosition, 0f, 0f);
        posToAdd = 0;
        for (int x = 0; x < 18; x++)
        {
            SpawnNote();
        }
        SetUiText();
        GetSongArea();
        hasStarted = true;
    }

    private void GetSongArea()
    {
        if (PermManager.AreaFlag < 3)
        {
            ChooseSong(forestSongs, forestTempos);
        }
        else if (PermManager.AreaFlag < 5)
        {
            ChooseSong(mountainSongs, mountainTempos);
        }
        else if (PermManager.AreaFlag < 6)
        {
            ChooseSong(desertSongs, desertTempos);
        }
    }

    private void ChooseSong(GameObject[] songs, int tempo)
    {
        int songNum = Random.Range(0, songs.Length);
        this.tempo = tempo;
        song = Instantiate(songs[songNum]);
        songAudio = song.GetComponent<AudioSource>();
        songAudio.Play();
    }

    public void ResetBeatCounter()
    {
        beatCounter = 0;
    }

    public void ChangeActivators(int code, Player curPlayer)
    {
        actAttack.ChangeColors(code, curPlayer);
        actSkills.ChangeColors(code, curPlayer);
        actItems.ChangeColors(code, curPlayer);
        actChange.ChangeColors(code, curPlayer);
    }

    public float GetTempo()
    {
        return (tempo / 60) * speedMultiplier;
    }

    public void EarlyNote()
    {
        ResetCounters(0);
        CheckBeatCounter();
    }

    public void MissedNote()
    {
        ResetCounters(2);
        CheckBeatCounter();
    }

    public void HitNote(int code)
    {
        combo++;
        if ((combo % 5 == 0) && multiplier < 5)
        {
            multiplier++;
        }
        SetUI(1);
        BattleManager.bManager.PerformAction(code);
        CheckBeatCounter();
    }

    private void ResetCounters(int categorie)
    {
        multiplier = 1;
        combo = 0;
        SetUI(categorie);
    }

    private void CheckBeatCounter()
    {
        if (BattleManager.bManager.playerTurn)
        {
            beatCounter++;
            if (beatCounter == beatLimit)
            {
                BattleManager.bManager.EndPlayerTurn();
            }
        }
    }

    private void SetUI(int categorie)
    {
        switch (categorie)
        {
            case 0:
                flavorText.text = "Early!";
                break;
            case 1:
                flavorText.text = "Great!";
                break;
            case 2:
                flavorText.text = "Missed!";
                break;
        }
        SetUiText();
        SpawnNote();
    }

    public void ItemText()
    {
        menuCoroutine = ShowMenuText("ITEMS");
        StopAllCoroutines();
        StartCoroutine(menuCoroutine);
    }

    public void WasteTime()
    {
        menuCoroutine = ShowMenuText("WASTED!");
        StopAllCoroutines();
        StartCoroutine(menuCoroutine);
    }

    public void CannotRun()
    {
        menuCoroutine = ShowMenuText("CAN'T RUN!");
        StopAllCoroutines();
        StartCoroutine(menuCoroutine);
    }

    public IEnumerator ShowMenuText(string text)
    {
        menuText.text = text;
        yield return new WaitForSeconds(1);
        menuText.text = "";
    }

    private void SpawnNote()
    {
        GameObject note = Instantiate(prefabNote, notes.transform);
        note.transform.localPosition = new Vector3(posToAdd, 3f, 0f);
        posToAdd = posToAdd - distance;
    }

    public void SpawnEnemyNote()
    {
        GameObject note = Instantiate(prefabEnemyNote, notes.transform);
        note.transform.localPosition = new Vector3((posToAdd + (15 * distance)), 3f, 0f);
    }

    private void SetUiText()
    {
        comboText.text = "Combo: " + combo;
        multiText.text = "Multiplier: " + multiplier;
    }
}
