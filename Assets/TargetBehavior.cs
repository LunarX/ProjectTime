//using System.Collections;
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

    // Contains characteristic data of the target
    private Target target;


    private Transform rb;
    private Vector3 finalScale;

    public void init(Target target)
    {
        this.target = target;
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
                Vector3 nextScale = rb.localScale + (finalScale / target.scalingTime) * Time.deltaTime;
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
                    Vector2 nextPos = prev + target.direction * target.speed * Time.deltaTime;
                    if (ReachedLimit(nextPos)) nextPos = GetLimit(); // In case we go a little bit too far inbetween two frames, constrain the position at the limit
                    rb.position = nextPos;
                }
                else
                {
                    state = WAITING_DEL_STATE;
                    Invoke("OnMissed", target.disapearanceTime);
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
        return Mathf.Abs(position.x) <= target.stopDistance * ((target.direction.x == 0) ? 0 : 1) && Mathf.Abs(position.y) <= target.stopDistance * ((target.direction.y == 0) ? 0 : 1);
    }

    // Returns the 2D position of the center position of this direction
    Vector2 GetLimit()
    {
        return -target.direction * target.stopDistance;
    }

    // When a target has been missed
    void OnMissed()
    {
        Destroy(gameObject);
    }
}
