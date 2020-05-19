using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_manager : MonoBehaviour
{

    private SpriteRenderer renderer;
    GameManager gm;
    private double sante = 0;
    public bool changeBG = false;
    private float yellowVal = 0.688f;
    private float redVal = 0.332f;
    private string colorBG = "green";
    private string currentBG = "green";
    private Sprite greenBG;
    private Sprite yellowBG;
    private Sprite redBG;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        renderer = GetComponent<SpriteRenderer>();
        greenBG = Resources.Load<Sprite>("Background/BG_purple");
        yellowBG = Resources.Load<Sprite>("Background/BG_yellow");
        redBG = Resources.Load<Sprite>("Background/BG_red");
        renderer.sprite = greenBG;
    }

    // Update is called once per frame
    void Update()
    {
        sante = gm.health.NormalHealth();
        //print("Santé : " + sante);
        NewColor();

        if (changeBG)
        {
            print("Changement de BG !");
            changeBG = false;
            currentBG = colorBG;
            NewBG();
        }
    }

    public void NewColor()
    {
        if (sante > yellowVal)
        {
            colorBG = "green";
        }
        else if ((sante < yellowVal) && (sante > redVal))
        {
            colorBG = "yellow";
        }
        else if (sante < redVal)
        {
            colorBG = "red";
        }

        if (colorBG != currentBG)
        {
            changeBG = true;
        }
    }

    public void NewBG()
    {
        if (currentBG == "green")
        {
            renderer.sprite = greenBG;
        }
        else if (currentBG == "yellow")
        {
            renderer.sprite = yellowBG;
        }
        else if (currentBG == "red")
        {
            renderer.sprite = redBG;
        }
    }


}
