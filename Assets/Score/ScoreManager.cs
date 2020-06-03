using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager 
{
    public int score;       // Score du joueur
    //private int comboLength;    // Nombre de cible atteinte d'affilée
    private const int scoreCenterTarget = 10;
    private const int scoreBorderTarget = 5;
    private const int scoreCenterSkel = 10; 
    private const int scoreBorderSkel = 5;
    public float combos = 1;
    AudioClip[] clips = new AudioClip[3];

    void Start()
    {
        score = 0;
        //comboLength = 0;

        clips[0] = Resources.Load<AudioClip>("Sound/Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        // Rien de spécial à faire...
    }

    public void TargetHitted(string TargetType, string success)
    {
        if (TargetType == "Circle")        // Une cible (les cercles) est touchée
        {
            if (success == "center")        // On pourra enlever les { et } inutiles plus tard
            {
                score += (int) Mathf.Round(scoreCenterTarget *combos);
            }
            else if (success == "border")
            {
                score += (int) Mathf.Round(scoreBorderTarget * combos);
            }
            combos += 0.2f;
        }
        else if (TargetType == "skeletton")   // Un squelette (dans les diagonales) est touchée
        {
            if (success == "center")
            {
                score += (int)Mathf.Round(scoreCenterSkel * combos);
            }
            else if (success == "border")
            {
                score += (int)Mathf.Round(scoreBorderSkel * combos);
            }
            //print("ERREUR : Squelette");
            combos += 0.2f;
        }
    }

    public int GetScore()
    {
        return score;
    }


}
