using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Distance to the origin of the target when it spawns")]
    public float maxRange = 5f;

    [Tooltip("Distance to the origin of the target when it has to stop because it reached the center")]
    public float minRange = 0.98f;


    [Tooltip("Time the target sticks at the end of its path before being considered as missed")]
    public float disappearanceTime = 0.2f;

    [Tooltip("Scaling factor of the size of the targets' sprites")]
    public float scalingFactor = 1f;

    public KeyCode k_up = KeyCode.W;
    public KeyCode k_down = KeyCode.S;
    public KeyCode k_left = KeyCode.A;
    public KeyCode k_right = KeyCode.D;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(k_up))
        {
            //print("Generating particle from TOP");
            GenerateTarget(Vector2.down);
        }
        else if(Input.GetKeyDown(k_down))
        {
            //print("Generating particle from BOT");
            GenerateTarget(Vector2.up);
        }
        else if (Input.GetKeyDown(k_left))
        {
            //print("Generating particle from LEFT");
            GenerateTarget(Vector2.right);
        }
        else if (Input.GetKeyDown(k_right))
        {
            //print("Generating particle from RIGHT");
            GenerateTarget(Vector2.left);
        }
    }


    private void GenerateTarget(Vector2 direction)
    {
        Sprite sprite = Resources.Load<Sprite>("Targets/Circle");

        GameObject go = new GameObject("Target Circle");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;

        float startingX = -direction.x * maxRange;
        float startingY = -direction.y * maxRange;

        go.transform.position = new Vector3(startingX, startingY, 0f);
        go.transform.localScale = new Vector3(0.3f, 0.3f, 1f) * scalingFactor;

        TargetBehavior tb = go.AddComponent<TargetBehavior>();

        tb.direction = direction;
        tb.speed = 3f;
        tb.stopDistance = minRange;
        tb.disapearance = disappearanceTime;
    }
}
