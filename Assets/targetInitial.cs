using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetInitial : MonoBehaviour
{

    // Direction, lié à des ints, car plus simple pour les index des arrays
    private const int RIGHT = 0;
    private const int LEFT = 1;
    private const int UP = 2;
    private const int DOWN = 3;

    [Header("Paramètres de Target")]
    [Tooltip("Distance to the origin of the target when it spawns")]
    public float maxRange = 4.5f;
    [Tooltip("Distance to the origin of the target when it has to stop because it reached the center")]
    public float minRange = 0.74f;
    [Tooltip("Time the target sticks at the end of its path before being considered as missed")]
    public float disapearanceTime = 0.2f;
    [Tooltip("Time it takes for a target to scale to its final size before starting to move")]
    public float scalingTime = 1.2f;
    [Tooltip("Scaling factor of the size of the targets' sprites")]
    public float scalingFactor = 0.85f;
    [Tooltip("Distance to the center of a target at which it can already be considered 'hit'")]
    public static float acceptedDistance = 0.1f;

    // Touche pour générer soi-même des cibles
    [Header("Génération des Target")]
    public KeyCode targetUp = KeyCode.W;        // Venant du haut
    public KeyCode targetDown = KeyCode.S;      // Venant du bas
    public KeyCode targetLeft = KeyCode.A;      // Venant de gauche
    public KeyCode targetRight = KeyCode.D;     // Venant de droite
    public KeyCode targetVertical = KeyCode.E;  // Venant d'en haut et d'en bas
    public KeyCode targetHorizontal = KeyCode.Q;    // Venant de à droite et à gauche

    // Listes contenant les cibles
    [HideInInspector]
    public List<GameObject> right_stack = new List<GameObject>();      
    [HideInInspector]
    public List<GameObject> left_stack = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> up_stack = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> down_stack = new List<GameObject>();
    [HideInInspector]
    public static List<GameObject>[] stacks = new List<GameObject>[4];
    
    // Variables pour la génération aléatoire des cibles
    private float currentTime;
    private float oldTime;
    private static readonly Vector2[] acceptedDirections = { Vector2.left, Vector2.right, Vector2.down, Vector2.up };
    private int randDir;
    private int toolbarInt = 0;
    private string[] toolbarStrings = new string[] { "Lent", "Moyen", "Rapide" };
    public static float interv = 3f;

    // Start is called before the first frame update
    void Start()
    {

        TargetGenerator.maxRange = maxRange;                    // Cercle extérieur
        TargetGenerator.minRange = minRange;                    // Cercle intérieur
        TargetGenerator.disapearanceTime = disapearanceTime;    // Temps avant la disparition
        TargetGenerator.scalingTime = scalingTime;  
        TargetGenerator.scalingFactor = scalingFactor;

        // On rassemble toutes les listes de cibles dans un seul array
        stacks[RIGHT] = right_stack;
        stacks[LEFT] = left_stack;
        stacks[UP] = up_stack;
        stacks[DOWN] = down_stack;

        currentTime = 0;        // Temps initial (au lancement du jeu)
    }

    // Update is called once per frame
    void Update()
    {
        // For testing purposes, generate targets using keyboard.
        if (Input.GetKeyDown(targetUp))
        {
            GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.down, 3f);
            up_stack.Add(t);
        }
        if (Input.GetKeyDown(targetDown))
        {
            GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.up, 3f);
            down_stack.Add(t);
        }
        if (Input.GetKeyDown(targetLeft))
        {
            GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.right, 3f);
            left_stack.Add(t);
        }
        if (Input.GetKeyDown(targetRight))
        {
            GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.left, 3f);
            right_stack.Add(t);
        }

        if (Input.GetKeyDown(targetHorizontal))
        {
            var ts = TargetGenerator.GenerateDoubleTarget(Vector2.left, Vector2.right, 3f);
            right_stack.Add(ts.Item1);
            left_stack.Add(ts.Item2);
        }

        if (Input.GetKeyDown(targetVertical))
        {
            var ts = TargetGenerator.GenerateDoubleTarget(Vector2.up, Vector2.down, 3f);
            down_stack.Add(ts.Item1);
            up_stack.Add(ts.Item2);
        }
    


        // Handles the destruction of missed targets
        // Iterate through all targets currently loaded, and find the ones that should 
        // be destroyed because the user missed it for too long and destroy them.
        DeleteMissedTargets(right_stack);
        DeleteMissedTargets(left_stack);
        DeleteMissedTargets(up_stack);
        DeleteMissedTargets(down_stack);


        if (toolbarInt == 0)
            interv = 2.5f;
        else if (toolbarInt == 1)
            interv = 1.5f;
        else if (toolbarInt == 2)
            interv = 0.6f;

        currentTime = Time.time;
        if (Mathf.Abs(currentTime - oldTime) > interv)      // S'active seul toutes les 'interv' secondes
        {
            randDir = Random.Range(0, 4);                   // Direction de la cible, générée aléatoirement
            oldTime = Time.time;                            // Pour la prochaine cible
            GameObject t = TargetGenerator.GenerateSingleTarget(acceptedDirections[randDir], 3f);
            stacks[randDir].Add(t);     // Evite de faire 4 if
        }

    }

    // Loops through the list given as argument, and checks for each target if it should
    // be considered as missed and destroyed, if so removes it from the list and destroys it.
    public void DeleteMissedTargets(List<GameObject> stack)
    {
        for (int i = stack.Count - 1; i >= 0; i--)      // Parcours les 4 stacks (gauche, haut, ...)
        {
            Target t = stack[i].GetComponent(typeof(Target)) as Target;
            if (t.GetTimeBeforeDeletion() <= 0)
            {
                Destroy(stack[i]);      // Détruit l'objet
                stack.RemoveAt(i);      // Enlève l'objet de la liste (puisque chaque Update, on parcours tous les objets, il faut l'enlever)
            }
        }
    }

    // Computes if a target has been hit or not and removes it if it does.
    public static void ClickTarget(int direction)
    {
        //List<GameObject> stack
        if (stacks[direction].Count > 0)
        {
            var idx = 0; // stacks[direction].Count - 1;
            var t = stacks[direction][idx].GetComponent(typeof(Target)) as Target;

            // Si on réussit à bien cliquer
            if (t.GetDistanceLeft() < acceptedDistance)
            {
                int type = t.GetTargetType();

                var colorModule = SideController.pss[direction].colorOverLifetime;

                if (type == Target.SINGLE)      // Cible Jaune
                    colorModule.color = SideController.yellowGradient;
                else                            // Cibles Violettes
                    colorModule.color = SideController.purpleGradient;

                SideController.pss[direction].Play();       // Active les effets hexagones, pour signifier la réussite

                Destroy(stacks[direction][idx]);
                stacks[direction].RemoveAt(idx);

                GameManager.score += 1;     // Score + 1
            }
        }
    }

    // Gui, pour modifier la vitesse de génération des cibles
    void OnGUI()
    {
        toolbarInt = GUI.Toolbar(new Rect(25, 25, 250, 30), toolbarInt, toolbarStrings);
    }

}
