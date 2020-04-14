using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Vector2 direction;

    public float speed;
    public float stopDistance;
    public float disapearanceTime;
    public float scalingTime;

    public void init(Vector2 direction, float speed, float stopDistance, float disapearanceTime, float scalingTime)
    {
        this.direction = direction;
        this.speed = speed;
        this.stopDistance = stopDistance;
        this.disapearanceTime = disapearanceTime;
        this.scalingTime = scalingTime;
    }
}
