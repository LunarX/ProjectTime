﻿//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    // A target will go through 3 phases during its lifetime:
    // First the target will be at the very edge of the screen and won't move, it will juste scale up to its finalScale.
    // Then the target will move to center of the screen. And finally when the target has reached the center of the screen,
    // the target will stay there a few milliseconds in case the user has not perfectly clicked on it right down to the millisecond. 
    private const int SCALE_STATE = 0;
    private const int MOVE_STATE = 1;
    private const int WAITING_DEL_STATE = 2;

    private int state = SCALE_STATE;


    [Header("Settings")]
    public Vector2 direction;// = Vector2.zero;

    public float speed;// = 0.1f;
    public float stopDistance;// = 0.98f;
    public float disapearanceTime;// = 0.2f;
    public float scalingTime;
    

    private Transform rb;
    Vector3 finalScale;

    public void init(Vector2 direction, float speed, float stopDistance, float disapearanceTime, float scalingTime)
    {
        this.direction = direction;
        this.speed = speed;
        this.stopDistance = stopDistance;
        this.disapearanceTime = disapearanceTime;
        this.scalingTime = scalingTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Transform>();

        finalScale = rb.localScale;
        rb.localScale = new Vector3(0f, 0f, finalScale.z);
    }


    void Update()
    {

        switch (state)
        {
            case SCALE_STATE:
                Vector3 nextScale = rb.localScale + (finalScale / scalingTime) * Time.deltaTime;
                nextScale.z = finalScale.z;
                rb.localScale = nextScale;

                if(rb.localScale.x >= finalScale.x)
                {
                    rb.localScale = finalScale;
                    state = MOVE_STATE;
                }
                break;
            case MOVE_STATE:
                Vector2 prev = rb.position;
                if (!ReachedLimit(prev)) // If the target hasn't reached the limit yet, update its position towards it
                {
                    Vector2 nextPos = prev + direction * speed * Time.deltaTime;
                    if (ReachedLimit(nextPos)) nextPos = GetLimit(); // In case we go a little bit too far inbetween two frames, constrain the position at the limit
                    rb.position = nextPos;
                }
                else
                {
                    state = WAITING_DEL_STATE;
                    Invoke("OnMissed", disapearanceTime);
                }
                break;
            case WAITING_DEL_STATE:
                // Literally do nothing
                break;
        }
    }


    // Returns true if the position of this target is the center position according to its direction
    bool ReachedLimit(Vector2 position)
    {
        return Mathf.Abs(position.x) <= stopDistance * ((direction.x == 0) ? 0 : 1) && Mathf.Abs(position.y) <= stopDistance * ((direction.y == 0) ? 0 : 1);
    }

    // Returns the 2D position of the center position of this direction
    Vector2 GetLimit()
    {
        return -direction * stopDistance;
    }

    // When a target has been missed
    void OnMissed()
    {
        Destroy(gameObject);
    }
}
