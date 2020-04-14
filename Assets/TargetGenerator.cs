using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class TargetGenerator
{
    [Header("Settings")]
    [Tooltip("Distance to the origin of the target when it spawns")]
    public static float maxRange = 4.5f;

    [Tooltip("Distance to the origin of the target when it has to stop because it reached the center")]
    public static float minRange = 0.98f;


    [Tooltip("Time the target sticks at the end of its path before being considered as missed")]
    public static float disapearanceTime = 0.2f;

    [Tooltip("Time it takes for a target to scale to its final size before starting to move")]
    public static float scalingTime = 1f;

    [Tooltip("Scaling factor of the size of the targets' sprites")]
    public static float scalingFactor = 1.2f;



    private static readonly Vector2[] acceptedDirections = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
    


    public static GameObject GenerateSingleTarget(Vector2 direction, float speed)
    {
        if (!Array.Exists(acceptedDirections, element => element == direction))
        {
            throw new ArgumentException("The given direction isn't accepted");
        }

        return CreateTarget("Targets/Circle", Target.SINGLE, direction, speed);
    }

    public static Tuple<GameObject, GameObject> GenerateDoubleTarget(Vector2 direction1, Vector2 direction2, float speed)
    {
        if(direction1 == direction2)
        {
            throw new ArgumentException("Cannot generate double target on the same side");
        }
        
        if (!Array.Exists(acceptedDirections, element => element == direction1) || !Array.Exists(acceptedDirections, element => element == direction2))
        {
            throw new ArgumentException("At least one of the two given direction isn't accepted");
        }

        return Tuple.Create(CreateTarget("Targets/Circle2", Target.DOUBLE, direction1, speed), CreateTarget("Targets/Circle2", Target.DOUBLE, direction2, speed));
    }

    private static GameObject CreateTarget(String spritePath, int type, Vector2 direction, float speed)
    {
        GameObject go = new GameObject("Target Circle");

        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>(spritePath);
        renderer.sprite = sprite;

        Target target = go.AddComponent<Target>();
        target.init(direction, speed, minRange, disapearanceTime, scalingTime, type);

        float startingX = -direction.x * maxRange;
        float startingY = -direction.y * maxRange;
        go.transform.position = new Vector3(startingX, startingY, 0f);
        go.transform.localScale = new Vector3(0.3f, 0.3f, 1f) * scalingFactor;

        return go;
    }
}
