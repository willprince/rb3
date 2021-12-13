using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActivatorObj : MonoBehaviour
{
    private SpriteRenderer sr;
    public Color curColor, curPressed, defaultColor, pressedColor, skillColor, skillPressed, menuColor, menuPressed;
    public Sprite curSprite, defaultSprite, skillSprite, mageSkill, warriorSkill;
    public KeyCode defaultKey, altKey;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {

        sr.sprite = curSprite;

        if (Input.GetKeyDown(defaultKey) || Input.GetKeyDown(altKey))
        {
            sr.color = curPressed;
        }

        if (Input.GetKeyUp(defaultKey) || Input.GetKeyUp(altKey))
        {
            sr.color = curColor;
        }
    }

    public void ChangeColors(int code, Player curPlayer)
    {
        switch (code)
        {
            case 0:
                curColor = defaultColor;
                curPressed = pressedColor;
                curSprite = defaultSprite;
                break;
            case 1:
                curColor = skillColor;
                curPressed = skillPressed;

                if (curPlayer.stats.name == "mage")
                {
                    curSprite = mageSkill;
                }
                else
                {
                    curSprite = warriorSkill;
                }
                break;
            case 2:
                curColor = menuColor;
                curPressed = menuPressed;
                curSprite = skillSprite;
                break;
        }
        sr.color = curColor;
    }
}
