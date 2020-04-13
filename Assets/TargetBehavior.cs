//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    [Header("Settings")]
    public Vector2 direction;// = Vector2.zero;

    public float speed;// = 0.1f;
    public float stopDistance;// = 0.98f;
    public float disapearance;// = 0.2f;

    private bool waitingForDeletion = false;

    private Transform rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Transform>();
    }


    void Update()
    {
        Vector2 prev = rb.position;
        if (!ReachedLimit(prev)) // If the target hasn't reached the limit yet, update its position towards it
        {
            Vector2 nextPos = prev + direction * speed * Time.deltaTime;

            if (ReachedLimit(nextPos)) nextPos = GetLimit(); // In case we go a little bit too far inbetween two frames, constrain the position at the limit

            rb.position = nextPos;
        }
        else
        {
            if(!waitingForDeletion)
            {
                waitingForDeletion = true;
                // If the target has reached its final position, consider that target as missed after 'disaprearance' seconds
                Invoke("OnMissed", disapearance);
            }
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
