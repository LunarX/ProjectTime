using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetGenerator
{
    [Header("Settings")]
    [Tooltip("Distance to the origin of the target when it spawns")]
    private static float maxRange;
    [Tooltip("Distance to the origin of the target when it has to stop because it reached the center")]
    private static float minRange;
    [Tooltip("Scaling factor of the size of the targets' sprites")]
    private static float scalingFactor;


    //[Tooltip("Time the target sticks at the end of its path before being considered as missed")]
    //public static float disapearanceTime = 0.2f;

    //[Tooltip("Time it takes for a target to scale to its final size before starting to move")]
    //public static float scalingTime;





    private static readonly Vector2[] acceptedDirections = { Vector2.left, Vector2.down, Vector2.right, Vector2.up };


    public TargetGenerator(float maxRange_, float minRange_, float scalingFactor_)
    {
        maxRange = maxRange_;
        minRange = minRange_;
        scalingFactor = scalingFactor_;
    }

    public GameObject GenerateSingleTarget(int direction, float speed, float disapearanceTime)
    {
        if (direction < 0 && direction >= acceptedDirections.Length)
        {
            throw new ArgumentException("The given direction isn't accepted");
        }

        return CreateTarget("Targets/NeonYellow", Target.SINGLE, acceptedDirections[direction], speed, disapearanceTime);
    }

    // PROPOSITION :
    // Direction : 0 = right, 1 = left, ... (cf. acceptedDirections). Evite les if's
    //public GameObject GenerateSingleTarget(Vector2 direction, float speed, float disapearanceTime)
    //{
    //    if (acceptedDirections.Contains(direction))
    //        return CreateTarget("Targets/Circle", Target.SINGLE, direction, speed, disapearanceTime);
    //    else
    //        throw new ArgumentException("The given direction isn't accepted");
    //}

    public Tuple<GameObject, GameObject> GenerateDoubleTarget(int direction1, int direction2, float speed, float disapearanceTime)
    {
        if(direction1 == direction2)
        {
            throw new ArgumentException("Cannot generate double target on the same side");
        }

        if (direction1 < 0 && direction1 >= acceptedDirections.Length || direction2 < 0 && direction2 >= acceptedDirections.Length)
        {
            throw new ArgumentException("At least one of the two given direction isn't accepted");
        }

        return Tuple.Create(CreateTarget("Targets/NeonPurple", Target.DOUBLE, acceptedDirections[direction1], speed, disapearanceTime), CreateTarget("Targets/NeonPurple", Target.DOUBLE, acceptedDirections[direction2], speed, disapearanceTime));
    }

    private static GameObject CreateTarget(String spritePath, int type, Vector2 direction, float speed, float disapearanceTime)
    {
        GameObject go = new GameObject("Target Circle");

        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>(spritePath);
        renderer.sprite = sprite;

        Target target = go.AddComponent<Target>();
        target.init(direction, speed, minRange, disapearanceTime, type);

        float startingX = -direction.x * maxRange;
        float startingY = -direction.y * maxRange;
        go.transform.position = new Vector3(startingX, startingY, 0f);
        go.transform.localScale = new Vector3(0.3f, 0.3f, 1f) * scalingFactor;

        // Plus utile... (mais au cas où)
        // Rajouté par Quentin (permet la détection de collision)
        //Rigidbody2D rb = go.AddComponent<Rigidbody2D>() as Rigidbody2D;
        //rb.bodyType = RigidbodyType2D.Kinematic;
        //rb.simulated = true;

        //CircleCollider2D cc = go.AddComponent<CircleCollider2D>() as CircleCollider2D;
        //cc.isTrigger = true;
        // 

        return go;
    }



}
