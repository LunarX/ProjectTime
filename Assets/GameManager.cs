using System.Collections;
using System.Collections.Generic;
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
    public float scalingTime = 1f;

    [Tooltip("Scaling factor of the size of the targets' sprites")]
    public float scalingFactor = 1.2f;


    [Header("Testing Key Settings")]
    public KeyCode k_up = KeyCode.W;
    public KeyCode k_down = KeyCode.S;
    public KeyCode k_left = KeyCode.A;
    public KeyCode k_right = KeyCode.D;


    private List<GameObject> right_stack = new List<GameObject>();

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
        if (Input.GetKeyDown(k_up))
        {
            //print("Generating particle from TOP");
            GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.down);
        }
        else if (Input.GetKeyDown(k_down))
        {
            //print("Generating particle from BOT");
            GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.up);
        }
        else if (Input.GetKeyDown(k_left))
        {
            //print("Generating particle from LEFT");
            GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.right);
        }
        else if (Input.GetKeyDown(k_right))
        {
            //print("Generating particle from RIGHT");
            GameObject t = TargetGenerator.GenerateSingleTarget(Vector2.left);
            right_stack.Add(t);
        }

        // Illegal target direction
        else if (Input.GetKeyDown("f"))
        {
            TargetGenerator.GenerateSingleTarget(new Vector2(-1f, -1f));
        }
    }

    void GM_OnMissed()
    {
        print("Received OnMissed Event");
    }
}
