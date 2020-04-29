using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager 
{
    private int score;       // Score du joueur
    //private int comboLength;    // Nombre de cible atteinte d'affilée
    private const int scoreCenterTarget = 10; // Euh... Je sais plus... (# de points si cible atteinte au centre ??)
    private const int scoreBorderTarget = 5; // Je sais plus non plus...
    private const int scoreCenterSkel = 10; // Euh... Je sais plus... (# de points si cible atteinte au centre ??)
    private const int scoreBorderSkel = 5; // Je sais plus non plus...

    void Start()
    {
        score = 0;
        //comboLength = 0;
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
                score += scoreCenterTarget;
            }
            else if (success == "border")
            {
                score += scoreBorderTarget;
            }
                //print("ERREUR : Circle");
        }
        else if (TargetType == "skeletton")   // Un squelette (dans les diagonales) est touchée
        {
            if (success == "center")
            {
                score += scoreCenterSkel;
            }
            else if (success == "border")
            {
                score += scoreBorderSkel;
            }
                //print("ERREUR : Squelette");
        }
    }

    public int GetScore()
    {
        return score;
    }


}
