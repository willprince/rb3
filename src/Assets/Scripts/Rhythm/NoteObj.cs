using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NoteObj : MonoBehaviour
{

    public bool shouldBePressed, isEarly;
    public KeyCode
        aDefaultKey, aAltKey,
        sDefaultKey, sAltKey,
        iDefaultKey, iAltKey,
        cDefaultKey, cAltKey;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        if (shouldBePressed)
        {
            if (Input.GetKeyDown(aDefaultKey) || Input.GetKeyDown(aAltKey))
            {
                PressedOnTime(0);
            }
            if (Input.GetKeyDown(sDefaultKey) || Input.GetKeyDown(sAltKey))
            {
                PressedOnTime(1);
            }
            if (Input.GetKeyDown(iDefaultKey) || Input.GetKeyDown(iAltKey))
            {
                PressedOnTime(2);
            }
            if (Input.GetKeyDown(cDefaultKey) || Input.GetKeyDown(cAltKey))
            {
                PressedOnTime(3);
            }
        }
        if (isEarly)
        {
            if (Input.GetKeyDown(aDefaultKey) || Input.GetKeyDown(aAltKey) ||
            Input.GetKeyDown(sDefaultKey) || Input.GetKeyDown(sAltKey) ||
            Input.GetKeyDown(iDefaultKey) || Input.GetKeyDown(iAltKey) ||
            Input.GetKeyDown(cDefaultKey) || Input.GetKeyDown(cAltKey))
            {
                Destroy(gameObject);
                RhythmManager.rManager.EarlyNote();
            }
        }
    }

    private void PressedOnTime(int code)
    {
        shouldBePressed = false;
        Destroy(gameObject);
        RhythmManager.rManager.HitNote(code);
    }

    private void OnTriggerEnter2D(Collider2D activator)
    {
        if (activator.tag == "Early")
        {
            isEarly = true;
        }
        if (activator.tag == "Activator")
        {
            isEarly = false;
            shouldBePressed = true;
        }
        if (shouldBePressed && activator.tag == "Missed")
        {
            shouldBePressed = false;
            Destroy(gameObject);
            RhythmManager.rManager.MissedNote();
        }
    }
}
