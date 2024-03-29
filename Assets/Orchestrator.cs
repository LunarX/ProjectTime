﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestrator : MonoBehaviour
{


    // Variables pour la génération aléatoire des cibles
    private float currentTime;
    private float oldTime;
    private float levelTime;
    private float patternTime = 0f;
    
    private int randDir;
    private int toolbarInt = 0;
    private string[] toolbarStrings = new string[] { "Lent", "Moyen", "Rapide" };
    private float interv = 2.5f;
    private float intervGenerate;

    public static Dictionary<int, GameObject> dicSkel = new Dictionary<int, GameObject>();

    public static int indexx = 0;       // Index pour les squelettes générés (cf. dicSkel)
    
    private PatternGenerator PG = new PatternGenerator();
    private List<(int[], float)> pattern;
    GameManager gm;
    private bool patternBool;
    private bool targetBool;
    private bool MGSsound;

    [Header("Activer les éléments")]
    public bool Zombie;      // Présence (True ou False) des zombies
    public bool Target;      // Présence (True ou False) des targets
    
    private bool slowPossible = true;

    public static float intervPattern;
    private int i;
    private string level = "";
    private float difficulty;
    private float bpm0 = 0, bpm1 = 0, bpm2 = 0;
    private float BPMstoreTime = 0f;

    [Header("Vitesse des niveaux")]
    public float level1 = 8.0f;
    public float level2 = 5.0f;
    public float level3 = 2.0f;


    private PythonConnexion PC;
    private float meanBPM = 90;

    private float oldBulletTime = 0;
    private float bulletTime = 0;

    private float diffilPattern = 0;


    // Start is called before the first frame update
    void Start()
    {
        
        patternBool = false;
        targetBool = true;
        intervPattern = 5f;
        i = 0;

        currentTime = 0;        // Temps initial (au lancement du jeu)
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        toolbarInt = PlayerPrefs.GetInt("Difficulty");
        SetDifficulty();


        Zombie = true;
        Target = true;
        Zombie = (PlayerPrefs.GetInt("Zombie") == 1);   // Renvoye un booléan
        Target = (PlayerPrefs.GetInt("Target") == 1);   // Pareil
        levelTime = 0;
        difficulty = 1f;


        if (GameObject.Find("PythonConnexion") != null)
        {
            PC = GameObject.Find("PythonConnexion").GetComponent<PythonConnexion>();
            meanBPM = PC.equipementMesures.averageBpm;

        }
    }


    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;

        intervGenerate = interv*difficulty;

        if (Mathf.Abs(currentTime - levelTime) > 1f)
        {
            levelTime = Time.time;
            difficulty -= 0.01f;
            
        }

        if (Mathf.Abs(currentTime - oldTime) > intervGenerate)      // S'active seul toutes les 'interv' secondes
        {
            oldTime = Time.time;                            // Pour la prochaine cible
            GenerateSkeletton();
            if (Target)
                GenerateTarget();
        }

        if (Mathf.Abs(bulletTime - oldBulletTime) > 6.0)
            slowPossible = true;

        if (Mathf.Abs(currentTime - patternTime) > intervPattern)        // Pattern activé tout les 'intervalPattern' secondes
        {
            patternTime = Time.time;
            pattern = PG.GetRandomPattern();
            patternBool = true;
            targetBool = false;
            MGSsound = true;
            i = 0;
        }

        if (patternBool)
            if (Target)
                GeneratePattern();      // Génération des patterns, si le moment est venu (cf patternBool)

        if (PC != null)     // Permet d'éviter une erreur, si on joue sans le PC (Python Controller)
            PC_control();   // Mesure du BPM, et mise à jour de la difficulté

        if (PC != null)
            storeBPM();

    }

    public void SetDifficulty()
    {
        if (toolbarInt == 0)
        {
            level = "easy";
            interv = level1;
            diffilPattern = 4f;
        }
        else if (toolbarInt == 1)
        {
            level = "medium";
            interv = level2;
            diffilPattern = 3f;
        }
        else if (toolbarInt == 2)
        {
            level = "hard";
            interv = level3;
            diffilPattern = 1f;
        }
    }

    public void GenerateSkeletton()
    {
        // Génère les squelettes :
        if (Zombie)     // Limite du nombre de squelette && si Skelette autorisé
        {
            GameObject s = SkeletonGenerator.CreateSkel(indexx);
            dicSkel.Add(indexx, s);
            indexx += 1;        // ID du zombie
        }
    }

    public void GenerateTarget()
    {
        if (Target)
        {
            randDir = Random.Range(0, 6);                   // Direction de la cible, générée aléatoirement

            if (randDir < 4)
            {
                GameObject t = gm.tg.GenerateSingleTarget(randDir, 3f, 0.15f);
                gm.stacks[randDir].Add(t);     // Evite de faire 4 if
            }
            else
            {
                if (randDir == 4)
                {
                    var ts = gm.tg.GenerateDoubleTarget(GameManager.LEFT, GameManager.RIGHT, 3f, 0.15f);
                    gm.stacks[GameManager.LEFT].Add(ts.Item1);
                    gm.stacks[GameManager.RIGHT].Add(ts.Item2);
                }
                if (randDir == 5)
                {
                    var ts = gm.tg.GenerateDoubleTarget(GameManager.UP, GameManager.DOWN, 3f, 0.15f);
                    gm.stacks[GameManager.UP].Add(ts.Item1);
                    gm.stacks[GameManager.DOWN].Add(ts.Item2);
                }

            }

        }
        
    }

    public void GeneratePattern()
    {
        
        var cible = pattern[i];
        float offset = cible.Item2;
        int[] dir = cible.Item1;

        if (MGSsound)   // Au début du pattern
        {
            SoundManager.PlaySound("MGS");
            MGSsound = false;
            gm.sPattern += Mathf.Round(Time.time - gm.timeStart) + " ";
        }

        if (Mathf.Abs(Time.time - patternTime) > diffilPattern*offset*difficulty+1)
        {
            if (dir.Length == 1)
            {
                GameObject t = gm.tg.GenerateSingleTarget(dir[0], 3f, 0.15f);
                gm.stacks[dir[0]].Add(t);     // Evite de faire 4 if
            }
            else
            {
                var ts = gm.tg.GenerateDoubleTarget(dir[0], dir[1], 3f, 0.15f);
                gm.stacks[dir[0]].Add(ts.Item1);
                gm.stacks[dir[1]].Add(ts.Item2);
            }

            i++;        // On passe à la prochaine cible
        }
        if (i == pattern.Count) 
        {
            patternBool = false;
            targetBool = true;
        }

            
    }

    void PC_control()
    {

        bpm2 = bpm1;
        bpm1 = bpm0;
        bpm0 = PC.equipementMesures.bpm;

        if (bpm2 - bpm0 > 100)       // Si le BPM augmente brutalement
        {
            difficulty += 0.1f;
        }

        if ((bpm0 > meanBPM*1.4) && (slowPossible))
        {
            gm.DoSlowmotion();
            slowPossible = false;
            difficulty = 1;
        }
            

        if (Mathf.Abs(currentTime - levelTime) > 1)
        {
            if ((bpm0 < (meanBPM*1.05f)) && (bpm2 < meanBPM*1.05))     // Si c'est assez calme
            {
                difficulty -= 0.05f;
            }
            else if (((bpm0 < meanBPM*1.2f) && (bpm0 > meanBPM*1.05f)) && ((bpm2 < meanBPM * 1.2f) && (bpm2 > meanBPM * 1.05f)))      // Léger stress
            {
                difficulty -= 0.01f;
            }
        }

    }

    public float GetDifficulty()
    {
        return difficulty;
    }

    void OnGUI()
    {
        var w = 100;
        var h = 50;
        
        GUI.Box(new Rect(0, 0, w, h), level + "\n" + intervGenerate);
        if (PC != null)
        {
            if (bpm0 > 125f)
                GUI.color = Color.red;
            else if (bpm0 < 60f)
                GUI.color = Color.green;
            else
                GUI.color = Color.white;
            GUI.Box(new Rect(0, Screen.height - h, w, h), "BPM : " + bpm0);
        }
    }

    // Store the BPM into a string
    public void storeBPM()
    {
        if (Mathf.Abs(currentTime - BPMstoreTime) > 1f)
        {
            BPMstoreTime = Time.time;
            gm.sBpm += (int) Mathf.Round(bpm0) + " ";
            gm.sBpmOn += PC.equipementMesures.faceDetected == true ? 1 : 0;
            gm.sBpmTime += (int)Mathf.Round(Time.time - gm.timeStart) + " ";
            gm.sFaceOk += PC.equipementMesures.faceDetected;
        }
    }

}
