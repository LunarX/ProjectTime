using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestrator : MonoBehaviour
{



    // Variables pour la génération aléatoire des cibles
    private float currentTime;
    private float oldTime;
    private float levelTime;
    //private static readonly Vector2[] acceptedDir = { Vector2.left, Vector2.down, Vector2.right, Vector2.up };
    private int randDir;
    private int toolbarInt = 0;
    private string[] toolbarStrings = new string[] { "Lent", "Moyen", "Rapide" };
    public static float interv = 2.5f;
    public static float intervGenerate;

    public static int numbSkel = 0;

    public static Dictionary<int, GameObject> dicSkel = new Dictionary<int, GameObject>();

    public static int indexx = 0;
    public int Zombie;      // Nombre max de zombie
    private PatternGenerator PG = new PatternGenerator();
    private List<(int[], float)> pattern;
    GameManager gm;
    private bool patternBool = false;
    private bool targetBool = true;
    private float patternTime = 0f;
    public static float intervPattern = 5f;
    private int i = 0;
    private string level = "";
    private float difficulty = 1f;
    private float bpm0 = 0, bpm1 = 0, bpm2 = 0;

    public PythonConnexion PC;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;        // Temps initial (au lancement du jeu)
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Zombie = 0;
        toolbarInt = PlayerPrefs.GetInt("Difficulty");
        SetDifficulty();
        Zombie = PlayerPrefs.GetInt("Zombie");
        levelTime = 0;

        if(GameObject.Find("PythonConnexion") != null)
            PC = GameObject.Find("PythonConnexion").GetComponent<PythonConnexion>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;

        intervGenerate = interv * difficulty;

        if (Mathf.Abs(currentTime - levelTime) > 1f)
        {
            levelTime = Time.time;
            difficulty -= 0.01f;
        }

        if (Mathf.Abs(currentTime - oldTime) > intervGenerate)      // S'active seul toutes les 'interv' secondes
        {
            oldTime = Time.time;                            // Pour la prochaine cible
            GenerateSkeletton();
            if (targetBool)
                GenerateTarget();
        }

        if (Mathf.Abs(currentTime - patternTime) > intervPattern)        // Pattern activé tout les 'intervalPattern' secondes
        {
            patternTime = Time.time;
            pattern = PG.GetRandomPattern();
            patternBool = true;
            targetBool = false;
            i = 0;
        }

        if (patternBool)
        {
            GeneratePattern();
        }

        if (PC != null)
        {
            PC_control();
        }




    }

    public void SetDifficulty()
    {
        if (toolbarInt == 0)
        {
            level = "easy";
            interv = 2.5f;
        }
        else if (toolbarInt == 1)
        {
            level = "medium";
            interv = 1.5f;
        }
        else if (toolbarInt == 2)
        {
            level = "hard";
            interv = 0.5f;
        }
    }

    public void GenerateSkeletton()
    {
        // Génère les squelettes :
        if ((numbSkel < 10) && (Zombie == 1))      // Limite du nombre de squelette && si Skelette autorisé
        {
            GameObject s = SkeletonGenerator.CreateSkel(indexx);
            dicSkel.Add(indexx, s);
            numbSkel += 1;      // Nombre de skelette en jeu actuellement
            indexx += 1;        // ID du zombie
        }
    }

    public void GenerateTarget()
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

    public void GeneratePattern()
    {
        
        var cible = pattern[i];
        float offset = cible.Item2;
        int[] dir = cible.Item1;
        if (Mathf.Abs(Time.time - patternTime) > offset)
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
            print("Fin pattern");
        }
            
    }

    void PC_control()
    {

        bpm2 = bpm1;
        bpm1 = bpm0;
        bpm0 = PC.equipementMesures.bpm;

        if (bpm2 - bpm0 > 70)
        {
            print("Big ecart !");
            difficulty += 0.1f;
        }

        if (Mathf.Abs(currentTime - levelTime) > 1)
        {
            if ((bpm0 < 60) && (bpm2 < 60))
            {
                difficulty -= 0.05f;
            }
            else if (((bpm0 < 80) && (bpm0 > 60)) && ((bpm2 < 80) && (bpm2 > 60)))
            {
                difficulty -= 0.01f;
            }
        }

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

}
