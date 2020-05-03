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


        if (Mathf.Abs(currentTime - oldTime) > interv)      // S'active seul toutes les 'interv' secondes
        {
            oldTime = Time.time;                            // Pour la prochaine cible
            GenerateSkeletton();
            GenerateTarget();
            patternBool = true;
            pattern = PG.GetRandomPattern();
        }

        //if (patternBool)
        //    GeneratePattern();

        currentTime = Time.time;
        
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
        //if (Mathf.Abs(Time.time - oldTime) > pattern[1].directions[1])
        //{
        //    print("Chapeau !");
        //}
    }

}
