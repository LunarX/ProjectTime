using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //private const int RIGHT = 0;
    //private const int LEFT = 1;
    //private const int UP = 2;
    //private const int DOWN = 3;

    //[Header("Settings")]
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
    //public float acceptedDistance = 0.1f;


    //[Header("Hit Particles")]
    //public ParticleSystem psRight;
    //public ParticleSystem psLeft;
    //public ParticleSystem psUp;
    //public ParticleSystem psDown;
    //private ParticleSystem[] pss = new ParticleSystem[4];
    //private Gradient yellowGradient;
    //private Gradient purpleGradient;

    //[Header("Playing Key Settings")]
    //public KeyCode right = KeyCode.RightArrow;
    //public KeyCode left = KeyCode.LeftArrow;
    //public KeyCode up = KeyCode.UpArrow;
    //public KeyCode down = KeyCode.DownArrow;

    //[Header("Testing Key Settings")]
    //public KeyCode t_up = KeyCode.W;
    //public KeyCode t_down = KeyCode.S;
    //public KeyCode t_left = KeyCode.A;
    //public KeyCode t_right = KeyCode.D;
    //public KeyCode t_double_vertical = KeyCode.E;
    //public KeyCode t_double_horizontal = KeyCode.Q;

    [Header("Donnée du jeu")]
    public static int score = 0;


    //private List<GameObject> right_stack = new List<GameObject>();
    //private List<GameObject> left_stack = new List<GameObject>();
    //private List<GameObject> up_stack = new List<GameObject>();
    //private List<GameObject> down_stack = new List<GameObject>();
    //private List<GameObject>[] stacks = new List<GameObject>[4];

    // Start is called before the first frame update
    void Start()
    {
        //TargetGenerator.maxRange = maxRange;
        //TargetGenerator.minRange = minRange;
        //TargetGenerator.disapearanceTime = disapearanceTime;
        //TargetGenerator.scalingTime = scalingTime;
        //TargetGenerator.scalingFactor = scalingFactor;

        //pss[RIGHT] = psRight;
        //pss[LEFT] = psLeft;
        //pss[UP] = psUp;
        //pss[DOWN] = psDown;

        //foreach (ParticleSystem ps in pss) {
        //    ps.Stop();
        //}

        //stacks[RIGHT] = right_stack;
        //stacks[LEFT] = left_stack;
        //stacks[UP] = up_stack;
        //stacks[DOWN] = down_stack;

        //var myYellow = new Color(1f, 0.95f, 0f);
        //var myPurple = new Color(0.7961f, 0.2980f, 0.9490f);

        //yellowGradient = new Gradient();
        //yellowGradient.SetKeys(
        //    new GradientColorKey[] { new GradientColorKey(myYellow, 0.0f), new GradientColorKey(myYellow, 1.0f) },
        //    new GradientAlphaKey[] { new GradientAlphaKey(0.5f, 0.0f), new GradientAlphaKey(0.25f, 1.0f) }
        //);

        //purpleGradient = new Gradient();
        //purpleGradient.SetKeys(
        //    new GradientColorKey[] { new GradientColorKey(myPurple, 0.0f), new GradientColorKey(myPurple, 1.0f) },
        //    new GradientAlphaKey[] { new GradientAlphaKey(0.5f, 0.0f), new GradientAlphaKey(0.25f, 1.0f) }
        //);
    }

    // Update is called once per frame
    void Update()
    {
        // Handles the click hit of targets
        //if (Input.GetKeyDown(right))
        //{
        //    ClickTarget(RIGHT);
        //}
        //if (Input.GetKeyDown(left))
        //{
        //    ClickTarget(LEFT);
        //}
        //if (Input.GetKeyDown(up))
        //{
        //    ClickTarget(UP);
        //}
        //if (Input.GetKeyDown(down))
        //{
        //    ClickTarget(DOWN);
        //}

        // Handles the destruction of missed targets
        // Iterate through all targets currently loaded, and find the ones that should 
        // be destroyed because the user missed it for too long and destroy them.
        //DeleteMissedTargets(right_stack);
        //DeleteMissedTargets(left_stack);
        //DeleteMissedTargets(up_stack);
        //DeleteMissedTargets(down_stack);


        // For testing purposes, generate targets using keyboard.
        //if (Input.GetKeyDown(t_up))
        //{
        //    GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.down, 3f);
        //    up_stack.Add(t);
        //}
        //if (Input.GetKeyDown(t_down))
        //{
        //    GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.up, 3f);
        //    down_stack.Add(t);
        //}
        //if (Input.GetKeyDown(t_left))
        //{
        //    GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.right, 3f);
        //    left_stack.Add(t);
        //}
        //if (Input.GetKeyDown(t_right))
        //{
        //    GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.left, 3f);
        //    right_stack.Add(t);
        //}

        //if(Input.GetKeyDown(t_double_horizontal))
        //{
        //    var ts = TargetGenerator.GenerateDoubleTarget(Vector2.left, Vector2.right, 3f);
        //    right_stack.Add(ts.Item1);
        //    left_stack.Add(ts.Item2);
        //}

        //if (Input.GetKeyDown(t_double_vertical))
        //{
        //    var ts = TargetGenerator.GenerateDoubleTarget(Vector2.up, Vector2.down, 3f);
        //    down_stack.Add(ts.Item1);
        //    up_stack.Add(ts.Item2);
        //}
    }

    // Loops through the list given as argument, and checks for each target if it should
    // be considered as missed and destroyed, if so removes it from the list and destroys it.
    //void DeleteMissedTargets(List<GameObject> stack)
    //{
    //    for (int i = stack.Count - 1; i >= 0; i--)
    //    {
    //        Target t = stack[i].GetComponent(typeof(Target)) as Target;
    //        if (t.GetTimeBeforeDeletion() <= 0)
    //        {
    //            Destroy(stack[i]);
    //            stack.RemoveAt(i);
    //        }
    //    }
    //}

    // Computes if a target has been hit or not and removes it if it does.
    //void ClickTarget(int direction)
    //{
    //    //List<GameObject> stack
    //    if (stacks[direction].Count > 0)
    //    {
    //        var idx = 0; // stacks[direction].Count - 1;
    //        var t = stacks[direction][idx].GetComponent(typeof(Target)) as Target;
    //        if (t.GetDistanceLeft() < acceptedDistance)
    //        {
    //            int type = t.GetTargetType();

    //            var colorModule = pss[direction].colorOverLifetime;
    //            if(type == Target.SINGLE) { colorModule.color = yellowGradient; }
    //            else { colorModule.color = purpleGradient; }
    //            pss[direction].Play();

    //            Destroy(stacks[direction][idx]);
    //            stacks[direction].RemoveAt(idx);
    //        }
    //    }
    //}

    void OnGUI()
    {
        GUI.Box(new Rect(550, 400, 100, 50), "Score : " + score);
    }
}
