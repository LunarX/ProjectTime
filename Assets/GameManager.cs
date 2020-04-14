using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
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
    public float acceptedDistance = 0.1f;

    [Header("Playing Key Settings")]
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;

    [Header("Testing Key Settings")]
    public KeyCode t_up = KeyCode.W;
    public KeyCode t_down = KeyCode.S;
    public KeyCode t_left = KeyCode.A;
    public KeyCode t_right = KeyCode.D;


    private List<GameObject> right_stack = new List<GameObject>();
    private List<GameObject> left_stack = new List<GameObject>();
    private List<GameObject> up_stack = new List<GameObject>();
    private List<GameObject> down_stack = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        TargetGenerator.maxRange = maxRange;
        TargetGenerator.minRange = minRange;
        TargetGenerator.disapearanceTime = disapearanceTime;
        TargetGenerator.scalingTime = scalingTime;
        TargetGenerator.scalingFactor = scalingFactor;
    }

    // Update is called once per frame
    void Update()
    {
        // Handles the click hit of targets
        if (Input.GetKeyDown(right))
        {
            ClickTarget(right_stack);
        }
        if (Input.GetKeyDown(left))
        {
            ClickTarget(left_stack);
        }
        if (Input.GetKeyDown(up))
        {
            ClickTarget(up_stack);
        }
        if (Input.GetKeyDown(down))
        {
            ClickTarget(down_stack);
        }

        // Handles the destruction of missed targets
        // Iterate through all targets currently loaded, and find the ones that should 
        // be destroyed because the user missed it for too long and destroy them.
        DeleteMissedTargets(right_stack);
        DeleteMissedTargets(left_stack);
        DeleteMissedTargets(up_stack);
        DeleteMissedTargets(down_stack);


        // For testing purposes, generate targets using keyboard.
        if (Input.GetKeyDown(t_up))
        {
            //print("Generating particle from TOP");
            GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.down);
            up_stack.Add(t);
        }
        if (Input.GetKeyDown(t_down))
        {
            //print("Generating particle from BOT");
            GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.up);
            down_stack.Add(t);
        }
        if (Input.GetKeyDown(t_left))
        {
            //print("Generating particle from LEFT");
            GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.right);
            left_stack.Add(t);
        }
        if (Input.GetKeyDown(t_right))
        {
            //print("Generating particle from RIGHT");
            GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.left);
            right_stack.Add(t);
        }
    }

    // Loops through the list given as argument, and checks for each target if it should
    // be considered as missed and destroyed, if so removes it from the list and destroys it.
    void DeleteMissedTargets(List<GameObject> stack)
    {
        for (int i = stack.Count - 1; i >= 0; i--)
        {
            Target t = stack[i].GetComponent(typeof(Target)) as Target;
            if (t.GetTimeBeforeDeletion() <= 0)
            {
                Destroy(stack[i]);
                stack.RemoveAt(i);
            }
        }
    }

    // Computes if a target has been hit or not and removes it if it does.
    void ClickTarget(List<GameObject> stack)
    {
        if (stack.Count > 0)
        {
            var idx = stack.Count - 1;
            var t = stack[idx].GetComponent(typeof(Target)) as Target;
            if (t.GetDistanceLeft() < acceptedDistance)
            {
                Destroy(stack[idx]);
                stack.RemoveAt(idx);
            }
        }
    }
}
