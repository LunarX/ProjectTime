using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestrator : MonoBehaviour
{



    // Variables pour la génération aléatoire des cibles
    private float currentTime;
    private float oldTime;
    //private static readonly Vector2[] acceptedDir = { Vector2.left, Vector2.down, Vector2.right, Vector2.up };
    private int randDir;
    private int toolbarInt = 0;
    private string[] toolbarStrings = new string[] { "Lent", "Moyen", "Rapide" };
    public static float interv = 2.5f;

    public static int numbSkel = 0;

    public static Dictionary<int, GameObject> dicSkel = new Dictionary<int, GameObject>();

    public static int indexx = 0;
    public int Zombie;      // Nombre max de zombie
    private PatternGenerator PG = new PatternGenerator();
    private List<(int[], float)> pattern;
    GameManager gm;
    private bool patternBool = false;
    private float patternTime = 0f;
    public static float intervPattern = 5f;
    private int i = 0;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;        // Temps initial (au lancement du jeu)
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Zombie = 0;
        toolbarInt = PlayerPrefs.GetInt("Difficulty");
        SetDifficulty();
        Zombie = PlayerPrefs.GetInt("Zombie");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;

        if (Mathf.Abs(currentTime - oldTime) > interv)      // S'active seul toutes les 'interv' secondes
        {
            oldTime = Time.time;                            // Pour la prochaine cible
            GenerateSkeletton();
            GenerateTarget();
        }

        if (Mathf.Abs(currentTime - patternTime) > intervPattern)        // Pattern activé tout les 'intervalPattern' secondes
        {
            
            patternTime = Time.time;
            //GeneratePattern();
            pattern = PG.GetRandomPattern();
            patternBool = true;
            i = 0;
        }

        if (patternBool)
        {
            GeneratePattern();
        }

        
        
    }

    public void SetDifficulty()
    {
        if (toolbarInt == 0)
            interv = 2.5f;
        else if (toolbarInt == 1)
            interv = 1.5f;
        else if (toolbarInt == 2)
            interv = 0.3f;
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
            print("Fin pattern");
        }
            
    }

}
