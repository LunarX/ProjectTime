using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetInitial : MonoBehaviour
{

    //// Direction, lié à des ints, car plus simple pour les index des arrays
    //private const int RIGHT = 0;
    //private const int LEFT = 1;
    //private const int UP = 2;
    //private const int DOWN = 3;

    //[Header("Paramètres de Target")]
    //[Tooltip("Distance to the origin of the target when it spawns")]
    //public float maxRange = 4.5f;
    //[Tooltip("Distance to the origin of the target when it has to stop because it reached the center")]
    //public float minRange = 0.74f;
    //[Tooltip("Time the target sticks at the end of its path before being considered as missed")]
    //public float disapearanceTime = 0.2f;
    //[Tooltip("Time it takes for a target to scale to its final size before starting to move")]
    //public float scalingTime = 1.2f;
    //[Tooltip("Scaling factor of the size of the targets' sprites")]
    //public float scalingFactor = 0.85f;
    //[Tooltip("Distance to the center of a target at which it can already be considered 'hit'")]
    //public static float acceptedDistance = 0.1f;


    //// Listes contenant les cibles
    //[HideInInspector]
    //public List<GameObject> right_stack = new List<GameObject>();      
    //[HideInInspector]
    //public List<GameObject> left_stack = new List<GameObject>();
    //[HideInInspector]
    //public List<GameObject> up_stack = new List<GameObject>();
    //[HideInInspector]
    //public List<GameObject> down_stack = new List<GameObject>();
    //[HideInInspector]
    //public static List<GameObject>[] stacks = new List<GameObject>[4];

    //// Variables pour la génération aléatoire des cibles
    //private float currentTime;
    //private float oldTime;
    //private static readonly Vector2[] acceptedDirections = { Vector2.left, Vector2.right, Vector2.down, Vector2.up };
    //private int randDir;
    //private int toolbarInt = 0;
    //private string[] toolbarStrings = new string[] { "Lent", "Moyen", "Rapide" };
    public static float interv = 3f;

    //// Start is called before the first frame update
    //void Start()
    //{

    //    TargetGenerator.maxRange = maxRange;                    // Cercle extérieur
    //    TargetGenerator.minRange = minRange;                    // Cercle intérieur
    //    TargetGenerator.disapearanceTime = disapearanceTime;    // Temps avant la disparition
    //    TargetGenerator.scalingTime = scalingTime;  
    //    TargetGenerator.scalingFactor = scalingFactor;

    //    // On rassemble toutes les listes de cibles dans un seul array
    //    stacks[RIGHT] = right_stack;
    //    stacks[LEFT] = left_stack;
    //    stacks[UP] = up_stack;
    //    stacks[DOWN] = down_stack;

    //    currentTime = 0;        // Temps initial (au lancement du jeu)
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //    if (toolbarInt == 0)
    //        interv = 2.5f;
    //    else if (toolbarInt == 1)
    //        interv = 1.5f;
    //    else if (toolbarInt == 2)
    //        interv = 0.6f;

    //    currentTime = Time.time;
    //    if (Mathf.Abs(currentTime - oldTime) > interv)      // S'active seul toutes les 'interv' secondes
    //    {
    //        randDir = Random.Range(0, 4);                   // Direction de la cible, générée aléatoirement
    //        oldTime = Time.time;                            // Pour la prochaine cible
    //        GameObject t = TargetGenerator.GenerateSingleTarget(acceptedDirections[randDir], 3f);
    //        stacks[randDir].Add(t);     // Evite de faire 4 if
    //    }

    //}

    //// Gui, pour modifier la vitesse de génération des cibles
    //void OnGUI()
    //{
    //    toolbarInt = GUI.Toolbar(new Rect(25, 25, 250, 30), toolbarInt, toolbarStrings);
    //}

}
