using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score;       // Score du joueur
    private int comboLength;    // Nombre de cible atteinte d'affilée
    private const int scoreCenterTarget = 10; // Euh... Je sais plus... (# de points si cible atteinte au centre ??)
    private const int scoreBorderTarget = 5; // Je sais plus non plus...
    private const int scoreCenterSkel = 10; // Euh... Je sais plus... (# de points si cible atteinte au centre ??)
    private const int scoreBorderSkel = 5; // Je sais plus non plus...

    void Start()
    {
        score = 0;
        comboLength = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Rien de spécial à faire...
    }

    public static void TargetHitted(string TargetType, string success)
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
            else
                print("ERREUR : Circle");
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
            else
                print("ERREUR : Squelette");
        }
    }

    // Affiche le score (se met à jour automatiquement)
    void OnGUI()
    {
        GUI.Box(new Rect(700, 400, 100, 50), "Score : " + score);
    }


}
