﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public const int RIGHT = 0;
    public const int LEFT = 1;
    public const int UP = 2;
    public const int DOWN = 3;

    [Header("Playing Key Settings")]
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;

    [Header("Settings")]
    [Tooltip("Distance to the origin of the target when it spawns")]
    public float maxRange = 4.5f;
    [Tooltip("Distance to the origin of the target when it has to stop because it reached the center")]
    public float minRange = 0.74f;
    [Tooltip("Scaling factor of the size of the targets' sprites")]
    public float scalingFactor = 0.85f;
    public VFXManager vfx;

    private float centerRadius = 0.1f;
    private float borderRadius = 0.2f;

    //[Tooltip("Time the target sticks at the end of its path before being considered as missed")]
    //public float disapearanceTime = 0.2f;
    //[Tooltip("Time it takes for a target to scale to its final size before starting to move")]
    //public float scalingTime = 1.2f;

    [Header("Donnée du jeu")]
    public static int score = 0;


    [Header("Testing Key Settings")]
    public KeyCode t_up = KeyCode.W;
    public KeyCode t_down = KeyCode.S;
    public KeyCode t_left = KeyCode.A;
    public KeyCode t_right = KeyCode.D;
    public KeyCode t_double_vertical = KeyCode.E;
    public KeyCode t_double_horizontal = KeyCode.Q;


    private List<GameObject> right_stack = new List<GameObject>();
    private List<GameObject> left_stack = new List<GameObject>();
    private List<GameObject> up_stack = new List<GameObject>();
    private List<GameObject> down_stack = new List<GameObject>();
    private List<GameObject>[] stacks = new List<GameObject>[4];

    private TargetGenerator tg;// = new TargetGenerator(maxRange, minRange, scalingFactor);
    

    // Start is called before the first frame update
    void Start()
    {
        stacks[RIGHT] = right_stack;
        stacks[LEFT] = left_stack;
        stacks[UP] = up_stack;
        stacks[DOWN] = down_stack;

        tg = new TargetGenerator(maxRange, minRange, scalingFactor);
}

    // Update is called once per frame
    void Update()
    {
        CheckTargets();
        DebugTargetGeneration();
    }

    void DebugTargetGeneration()
    {
        // For testing purposes, generate targets using keyboard.
        if (Input.GetKeyDown(t_up))
        {
            GameObject t = tg.GenerateSingleTarget(DOWN, 3f, 0.2f);
            up_stack.Add(t);
        }
        if (Input.GetKeyDown(t_down))
        {
            GameObject t = tg.GenerateSingleTarget(UP, 3f, 0.2f);
            down_stack.Add(t);
        }
        if (Input.GetKeyDown(t_left))
        {
            GameObject t = tg.GenerateSingleTarget(RIGHT, 3f, 0.2f);
            left_stack.Add(t);
        }
        if (Input.GetKeyDown(t_right))
        {
            GameObject t = tg.GenerateSingleTarget(LEFT, 3f, 0.2f);
            right_stack.Add(t);
        }

        if (Input.GetKeyDown(t_double_horizontal))
        {
            var ts = tg.GenerateDoubleTarget(LEFT, RIGHT, 3f, 0.2f);
            right_stack.Add(ts.Item1);
            left_stack.Add(ts.Item2);
        }

        if (Input.GetKeyDown(t_double_vertical))
        {
            var ts = tg.GenerateDoubleTarget(UP, DOWN, 3f, 0.2f);
            down_stack.Add(ts.Item1);
            up_stack.Add(ts.Item2);
        }
    }

    void CheckTargets()
    {
        // Handles the click hit of targets
        if (Input.GetKeyDown(right))
        {
            ClickTarget(RIGHT);
        }
        if (Input.GetKeyDown(left))
        {
            ClickTarget(LEFT);
        }
        if (Input.GetKeyDown(up))
        {
            ClickTarget(UP);
        }
        if (Input.GetKeyDown(down))
        {
            ClickTarget(DOWN);
        }

        // Handles the destruction of missed targets
        // For each stack check if the oldest target should be destroyed because the user missed
        // it for too long and destroy them.
        DeleteMissedTargets(RIGHT);
        DeleteMissedTargets(LEFT);
        DeleteMissedTargets(UP);
        DeleteMissedTargets(DOWN);

    }

    // Loops through the list given as argument, and checks for each target if it should
    // be considered as missed and destroyed, if so removes it from the list and destroys it.
    void DeleteMissedTargets(int direction)
    {
        if (stacks[direction].Count > 0)
        {
            int i = 0;
            Target t = stacks[direction][i].GetComponent(typeof(Target)) as Target;
            if (t.GetTimeBeforeDeletion() <= 0)
            {
                Destroy(stacks[direction][i]);
                stacks[direction].RemoveAt(i);
            }
        }
    }

    // Computes if a target has been hit or not and removes it if it does.
    void ClickTarget(int direction)
    {
        //List<GameObject> stack
        if (stacks[direction].Count > 0)
        {
            var idx = 0; // stacks[direction].Count - 1;
            var t = stacks[direction][idx].GetComponent(typeof(Target)) as Target;
            var d = t.GetDistanceLeft();
            if (d < borderRadius)
            {
                vfx.PlayHaxagones(direction, t.GetTargetType());

                Destroy(stacks[direction][idx]);
                stacks[direction].RemoveAt(idx);

                if(d < centerRadius)
                {
                    //TODO Call ScoreManager with target hit in the center
                }
                else
                {
                    //TODO Call ScoreManager with target hit in the border
                }
            }
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(550, 400, 100, 50), "Score : " + score);
    }
}
